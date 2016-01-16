using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using NOTAM.Behavior;
using NOTAM.Properties;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using Word = Microsoft.Office.Interop.Word;

namespace NOTAM.ViewModel
{
    public class NotamSummaryViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {
        private ICommand _runCommand;
        private ICommand _checklistCommand;

        readonly NotamService _notamService;
        readonly IntlNotamService _intlNotamService;
        readonly AerodomService _aerodomService;
        readonly NotamCodeService _notamcodeService;
        #region Constructor
        public NotamSummaryViewModel(NotamService notamService, IntlNotamService intlnotamService, AerodomService aerodomService, NotamCodeService notamcodeService, ObservableCollection<WorkspaceViewModel> parent)
            : this(notamService, aerodomService, notamcodeService)
        {
            parentWorkSpaces = parent;
            _intlNotamService = intlnotamService;

        }
        public NotamSummaryViewModel(NotamService notamService, AerodomService aerodomService,NotamCodeService notamcodeService)
        {
            if (notamService == null)
                throw new ArgumentNullException("notamService");

            if (aerodomService == null)
                throw new ArgumentNullException("aerodomService");

            _notamService = notamService;
            _aerodomService = aerodomService;
            _notamcodeService = notamcodeService;

            this.validators = this.GetType()
              .GetProperties()
              .Where(p => this.GetValidations(p).Length != 0)
              .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }

        #endregion

        #region Origin Properties
        [RegularExpression(@"^[\d]{10}$", ErrorMessage = "Value should be 10 digits")]
        public String FromDate
        {
            get; set;
        }

        [RegularExpression(@"^[\d]{10}$", ErrorMessage = "Value should be 10 digits")]
        public String ToDate
        {
            get;
            set;
        }

        public String NType
        {
            get;
            set;
        }
      

        #endregion // Summary Properties

        #region Presentation Properties

        private bool _bInternational=false;
        public bool bInternational
        {
            get { return _bInternational; }
            set {
                _bInternational = value; 
            }
        }

        public override string DisplayName
        {
            get { return Entity.NotamSummary_DisplayName; }
        }

        /// <summary>
        /// Gets/sets whether this customer is selected in the UI.
        /// </summary>
    
        /// <summary>
        /// Returns a command that saves the customer.
        /// </summary>
        public ICommand RunCommand
        {
            get
            {
                if (_runCommand == null)
                {
                    _runCommand = new RelayCommand(
                        param => this.RunSummary(),
                        param => this.CanRun
                        );
                }
                return _runCommand;
            }
        }
        public ICommand CheckListCommand
        {
            get
            {
                if (_checklistCommand == null)
                {
                    _checklistCommand = new RelayCommand(
                        param => this.CheckListSummary(),
                        param => this.CanRun
                        );
                }
                return _checklistCommand;
            }
        }

       

        bool CanRun
        {
            get
            {
                return this.ValidateType() && string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount;
            }
        }

        #endregion // Presentation Properties


        void RunSummary()
        {
            NotamFilter filter = new NotamFilter();
            
            filter.TypeFilter = NType;
            filter.FromDateFilter = FromDate;
            filter.ToDateFilter = ToDate;
            _notamService.Archive();
            List<Notam> notamList = _notamService.GetFilterNotams(filter);
            List<string> aeroList = notamList.Select(x => x.FirAero).ToList<string>();
            Dictionary<string, string> aeroDic = _aerodomService.GetAddressList(aeroList);
            object fileName = System.Configuration.ConfigurationManager.AppSettings.Get("DocTemplatePath").ToString();
            Word.Application word = new Word.Application();
            Word.Document doc = new Word.Document();
            object missing = System.Type.Missing;
            try
            {
                doc = word.Documents.Open(ref fileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                doc.Activate();
                string newFileName = fileName.ToString();
                newFileName = newFileName.Replace(".docx", "1.docx");
                doc.SaveAs(newFileName);
                Process.Start(newFileName.ToString());
                int icol = 2, irow = 1;
                var queryYear = notamList.GroupBy(item => item.Year).Select(group =>
                                        new
                                        {
                                            Year = group.Key,
                                            nums = group.OrderBy(x => x.Number)
                                        }).OrderBy(group => group.nums.First().Number);
                Word.Table newTable = doc.Application.ActiveDocument.Tables[1];
                //                newTable.Rows.Add(newTable.Rows[1]);
                foreach (var yearItem in queryYear)
                {
                    newTable.Cell(irow, 1).Range.Text = "20" + yearItem.Year + ":";
                    foreach (var nt in yearItem.nums)
                    {
                        if (icol > 14)
                        {
                            icol = 2;
                            irow++;
                            newTable.Rows.Add(ref missing);

                        }
                        if (nt.Number!=null)
                            newTable.Cell(irow, icol).Range.Text = nt.Number.ToString();
                        doc.Save();
                        icol++;
                    }
                    irow++;
                    icol = 2;
                    newTable.Rows.Add(ref missing);
                    doc.Save();
                }
                

                //add0519
                var queryAero = notamList.Where(x => x.FirAero!=null).GroupBy(item => item.FirAero).Select(group =>
                        new
                        {
                            Aero = group.Key,
                            Addr = aeroDic.ContainsKey(group.Key) ? aeroDic[group.Key]:"",
                            nums = group.OrderBy(x => x.Number)
                        }).OrderBy(group => group.Addr);
                Word.Table secTable = doc.Application.ActiveDocument.Tables[2];
                irow = 1;
                //                newTable.Rows.Add(newTable.Rows[1]);
                
                foreach (var aeroItem in queryAero)
                {
                    secTable.Rows.Add(ref missing);
                    irow++;
                    secTable.Cell(irow, 3).Range.Font.Size = 11;
                    secTable.Cell(irow, 3).Range.Font.Bold = 1;
                    secTable.Cell(irow, 3).Range.Font.Italic = 1;
                    string strAeroCity = aeroItem.Addr;
                    secTable.Cell(irow, 3).Range.Text = strAeroCity + "." + aeroItem.Aero;
                    doc.Save();

                    foreach (var nt in aeroItem.nums)
                    {
                        secTable.Rows.Add(ref missing);
                        irow++;
                        secTable.Cell(irow, 1).Range.Font.Bold = 0;
                        secTable.Cell(irow, 1).Range.Font.Italic = 0;
                        secTable.Cell(irow, 1).Range.Font.Size = 10;
                        if (nt.Number!=null)
                            secTable.Cell(irow, 1).Range.Text = nt.Type+ nt.Number.ToString();
                        secTable.Cell(irow, 2).Range.Font.Bold = 0;
                        secTable.Cell(irow, 2).Range.Font.Italic = 0;
                        secTable.Cell(irow, 2).Range.Font.Size = 10;
                        if (nt.FromDate!=null)
                            secTable.Cell(irow, 2).Range.Text = nt.FromDate.Substring(0, 6);
                        string str = nt.FromDate + "/" + nt.ToDate + "/" + nt.PermEst + "\n";
                        string iteme = nt.EFreeText;
                        secTable.Cell(irow, 3).Range.Font.Bold = 0;
                        secTable.Cell(irow, 3).Range.Font.Italic = 0;
                        secTable.Cell(irow, 3).Range.Font.Size = 9;
                        secTable.Cell(irow, 3).Range.Text = str + iteme;
                        doc.Save();

                    }
                }


                doc.Save();
                MessageBox.Show("Summary created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }
        void CheckListSummary()
        {
            NotamFilter filter = new NotamFilter();
            filter.TypeFilter = NType;
            filter.FromDateFilter = FromDate;
            filter.ToDateFilter = ToDate;
            _notamService.Archive();
            List<Notam> notamList = _notamService.GetFilterNotams(filter);
            try
            {
                var queryYear = notamList.GroupBy(item => item.Year).Select(group =>
                                        new
                                        {
                                            Year = group.Key,
                                            nums = group.OrderBy(x => x.Number)
                                        }).OrderBy(group => group.Year);
                //                newTable.Rows.Add(newTable.Rows[1]);
                int icol=1;
                string strItemE = "CHECKLIST:";
                foreach (var yearItem in queryYear)
                {
                    strItemE += "\r\nYEAR 20" + yearItem.Year + ": ";
                    foreach (var nt in yearItem.nums)
                    {
                        strItemE += nt.Number + " ";
                        icol++;
                        if (icol > 11)
                        {
                            icol = 1;
                            strItemE += "\r\n";

                        }
                    }
                    icol = 1;
                }
                strItemE += "\r\nLATEST PUBLICATIONS: ";
                NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewRNotam();
                newNotam.NotamCode = _notamcodeService.GetNotamCodes().Where(o => (o.Subject + o.Condition).Equals("KKKK")).FirstOrDefault();
                List<Notam> AllNotams = _notamService.GetAllNotams();
                Notam ntm = AllNotams.Where(m => (m.NotamCode == newNotam.NotamCode && m.Type == NType 
                                                    && m.Year == DateTime.Now.Year.ToString().Substring(2) )).OrderByDescending(x => x.Number).FirstOrDefault();
                if (ntm == null)
                    ntm = AllNotams.Where(m => (m.NotamCode == newNotam.NotamCode && m.Type == NType )).OrderByDescending(x => x.Number).FirstOrDefault();
                newNotam.Type = "A";
                if (ntm != null)
                {
                    newNotam.RefId = ntm.Id;
                    newNotam.RefNum = ntm.Number;
                    newNotam.RefYear = ntm.Year;
                    newNotam.RefType = ntm.Type;
                    newNotam.Type = ntm.Type;
                    newNotam.FirAero = ntm.FirAero;
                }
                newNotam.EFreeText = strItemE;
                NotamViewModel workspace = new NotamViewModel(newNotam, _notamService, parentWorkSpaces);
                parentWorkSpaces.Add(workspace);
                
                this.SetActiveWorkspace(workspace);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }




        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<NotamSummaryViewModel, object>> propertyGetters;
        private readonly Dictionary<string, ValidationAttribute[]> validators;

        #region DataError

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// 
        /// 

        public string this[string propertyName]
        {
            get
            {
                if (this.propertyGetters.ContainsKey(propertyName))
                {
                    var propertyValue = this.propertyGetters[propertyName](this);
                    var errorMessages = this.validators[propertyName]
                        .Where(v => !v.IsValid(propertyValue))
                        .Select(v => v.ErrorMessage).ToArray();

                    return string.Join(Environment.NewLine, errorMessages);
                }

                return string.Empty;
            }
        }



        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error
        {
            get
            {
                var errors = from validator in this.validators
                             from attribute in validator.Value
                             where !attribute.IsValid(this.propertyGetters[validator.Key](this))
                             select attribute.ErrorMessage;

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        /// <summary>
        /// Gets the number of properties which have a validation attribute and are currently valid
        /// </summary>
        public int ValidPropertiesCount
        {
            get
            {
                var query = from validator in this.validators
                            where validator.Value.All(attribute => attribute.IsValid(this.propertyGetters[validator.Key](this)))
                            select validator;

                var count = query.Count(); //- this.validationExceptionCount;
                return count;
            }
        }

        /// <summary>
        /// Gets the number of properties which have a validation attribute
        /// </summary>
        public int TotalPropertiesWithValidationCount
        {
            get
            {
                return this.validators.Count();
            }
        }



        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        private Func<NotamSummaryViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<NotamSummaryViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion



        private bool ValidateType()
        {
            return !string.IsNullOrEmpty(NType);            

            //if (this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
            //   this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
            //    return null;

            //return Strings.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion // IDataErrorInfo Members

        private int validationExceptionCount;

        public void ValidationExceptionsChanged(int count)
        {
            this.validationExceptionCount = count;
            this.OnPropertyChanged("ValidPropertiesCount");
        }
    }
}
