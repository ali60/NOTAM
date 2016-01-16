using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Documents;

namespace NOTAM.ViewModel
{
    public class IntlNotamDetailViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        NotamDetail _notam;
        readonly NotamDetailService _notamDetailService;
        readonly IntlNotamService _notamService;

        RelayCommand _saveCommand;
        RelayCommand _sendCommand;
        RelayCommand _printCommand;
        RelayCommand _openFileCommand;
        RelayCommand _sendDefaultCommand;
        RelayCommand _removeAftnCommand;
        RelayCommand _addAftnCommand;
        RelayCommand _addManualAftnCommand;
        RelayCommand _delManualAftnCommand;
        private String _notamNum;
        private String openPar = "(";
        private String closePar = ")";
        private String slash = "/";
        private String nullChar = "\0";
        WorkspaceViewModel NotamWorkspace;

        #endregion // Fields
        #region Constructor

        public IntlNotamDetailViewModel(NotamDetail notam, NotamDetailService notamDetailService, IntlNotamService notamService, ObservableCollection<WorkspaceViewModel> parent, WorkspaceViewModel ntmWorkspace)
        {
            parentWorkSpaces = parent;
            NotamWorkspace = ntmWorkspace;
            if (notam == null)
                throw new ArgumentNullException("notam");

            if (notamService == null)
                throw new ArgumentNullException("notamService");
            if (notamDetailService == null)
                throw new ArgumentNullException("notamDetailService");

            _notam = notam;
            _notamDetailService = notamDetailService;
            _notamService = notamService;


            this.validators = this.GetType()
               .GetProperties()
               .Where(p => this.GetValidations(p).Length != 0)
               .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }

        #endregion // Constructor

        #region Display Properties



        private String _DisplayName;
        public override string DisplayName
        {
            get
            {
                _DisplayName = _notam.Notam != null ? _notam.Notam.Number : Entity.NotamDetailViewModel_DisplayName;
                return _DisplayName + " Send Info";
            }
        }

        #endregion
        #region  Properties
        //public string NotamNum
        //{
        //    get
        //    {
        //        _notamNum = _notam.Notam!=null ? _notam.Notam.Number : Entity.NotamDetailViewModel_DisplayName;
        //        return _notamNum;
        //    }
        //    set { _notamNum = value; }

        //}


        public string DescriptionRemark
        {
            get { return _notam.DescriptionRemark; }
            set
            {
                if (value == _notam.DescriptionRemark)
                    return;
                if (!CanSet)
                    return;

                _notam.DescriptionRemark = value;

                base.OnPropertyChanged("DescriptionRemark");
            }
        }
        public string DescriptionAFTN
        {
            get { return _notam.DescriptionAFTN; }
            set
            {
                if (value == _notam.DescriptionAFTN)
                    return;
                if (!CanSet)
                    return;

                _notam.DescriptionAFTN = value;

                base.OnPropertyChanged("DescriptionAFTN");
            }
        }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "length must be")]
        public string FilePath
        {
            get { return _notam.FileUrl; }
            set
            {
                if (value == _notam.FileUrl)
                    return;

                if (!CanSet)
                    return;
                _notam.FileUrl = value;

                base.OnPropertyChanged("FilePath");
            }
        }


        [StringLength(3, MinimumLength = 3, ErrorMessage = "length must be 3 Chars")]
        public string User
        {
            get { return _notam.User; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.User)
                    return;

                _notam.User = value;

                base.OnPropertyChanged("User");
            }
        }
        private bool _IsAftnMsg = false;
        public bool IsAftnMsg
        {
            get
            {
                if (_notam.Source == 3 || _notam.Source == 2)
                    _IsAftnMsg = true;
                return _IsAftnMsg;
            }
            set
            {
                if (!CanSet)
                    return;
                _IsAftnMsg = value;
                _notam.Source = 0;
                if (_IsRemark)
                    _notam.Source = 1;
                if (_IsAftnMsg)
                    _notam.Source = 2;
                if (_IsAftnMsg && _IsRemark)
                    _notam.Source = 3;

                base.OnPropertyChanged("IsAftnMsg");
                base.OnPropertyChanged("IsRemark");
            }
        }

        private bool _IsRemark = false;
        public bool IsRemark
        {
            get
            {
                if (_notam.Source == 3 || _notam.Source == 1)
                    _IsRemark = true;
                return _IsRemark;
            }
            set
            {
                if (!CanSet)
                    return;
                _IsRemark = value;
                _notam.Source = 0;
                if (_IsRemark)
                    _notam.Source = 1;
                if (_IsAftnMsg)
                    _notam.Source = 2;
                if (_IsAftnMsg && _IsRemark)
                    _notam.Source = 3;


                base.OnPropertyChanged("IsRemark");
                base.OnPropertyChanged("IsAftnMsg");
            }
        }

        private string _notamText;

        public string NotamText
        {
            get
            {
                if (string.IsNullOrEmpty(_notamText))
                {
                    _notamText = GeneratePreviewNotamText();
                }
                return _notamText;
            }
            set
            {

                _notamText = value;
            }
        }

        private List<Aftn> _aftnOptions;
        /// <summary>
        /// Returns a list of Aftns for selection
        /// </summary>
        public List<Aftn> AftnOptions
        {
            get
            {
                if (_aftnOptions == null)
                {
                    _aftnOptions = _notamDetailService.GetAllAftns();
                }
                return _aftnOptions;
            }
        }


        private ObservableCollection<Aftn> _selectedAftns;

        /// <summary>
        /// Returns a list of Aftns that are selected
        /// </summary>
        public ObservableCollection<Aftn> SelectedAftns
        {
            get { return _selectedAftns; }
            set
            {
                _selectedAftns = value;
                base.OnPropertyChanged("SelectedAftns");

            }
        }


        private ObservableCollection<String> _manualAftns;

        /// <summary>
        /// Returns a list of manual Aftns that are selected
        /// </summary>
        public ObservableCollection<String> ManualAftns
        {
            get { return _manualAftns; }
            set
            {
                _manualAftns = value;
                base.OnPropertyChanged("ManualAftns");

            }
        }

        private List<string> _removedManualAftns;

        /// <summary>
        /// current selected Aftn
        /// </summary>
        public List<string> RemovedManualAftns
        {
            get { return _removedManualAftns; }
            set
            {
                _removedManualAftns = value;
                base.OnPropertyChanged("RemovedManualAftns");
            }
        }
        private String _manualAftn;

        /// <summary>
        /// Returns a list of Aftns that are selected
        /// </summary>
        public String ManualAftn
        {
            get { return _manualAftn; }
            set
            {
                _manualAftn = value;
                base.OnPropertyChanged("ManualAftn");

            }
        }


        private Aftn _selectedAftn;

        /// <summary>
        /// current selected Aftn
        /// </summary>
        public Aftn SelectedAftn
        {
            get { return _selectedAftn; }
            set
            {
                _selectedAftn = value;
                base.OnPropertyChanged("SelectedAftn");
            }
        }



        private IList<Aftn> _removedAftns;

        /// <summary>
        /// current selected Aftn
        /// </summary>
        public IList<Aftn> RemovedAftns
        {
            get { return _removedAftns; }
            set
            {
                _removedAftns = value;
                base.OnPropertyChanged("RemovedAftns");
            }
        }
        // <summary>
        /// Returns a command that saves the Notam.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => CanSave && this.IsNewNotam
                        );
                }
                return _saveCommand;
            }
        }
        bool CanSave
        {
            get
            {
                return true;
            }
        }
        bool CanSendDefault
        {
            get
            {
                return !string.IsNullOrEmpty(GenerateDefaultAFTNHeader());
            }
        }
        bool CanSet
        {
            get
            {
                return string.IsNullOrEmpty(_notam.Notam.Number) || _FreeStyle;
            }
        }
        bool _FreeStyle = false;
        public bool FreeStyle
        {
            set { _FreeStyle = value; }
            get
            {
                return _FreeStyle;
            }
        }

        // <summary>
        /// Returns a command that saves the Notam.
        /// </summary>
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(
                        param => this.Send(),
                        param => this.CanSave && !this.IsNewNotam
                        );
                }
                return _sendCommand;
            }
        }

        public ICommand PrintCommand
        {
            get
            {
                if (_printCommand == null)
                {
                    _printCommand = new RelayCommand(
                        param => this.Print(),
                        param => true
                        );
                }
                return _printCommand;
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                if (_openFileCommand == null)
                {
                    _openFileCommand = new RelayCommand(
                        param => this.OpenFile(),
                        param => true
                        );
                }
                return _openFileCommand;
            }
        }
        public ICommand SendDefaultCommand
        {
            get
            {
                if (_sendDefaultCommand == null)
                {
                    _sendDefaultCommand = new RelayCommand(
                        param => this.SendDefault(),
                        param => this.CanSave && !this.IsNewNotam && CanSendDefault
                        );
                }
                return _sendDefaultCommand;
            }
        }

        public ICommand AddAftnCommand
        {
            get
            {
                if (_addAftnCommand == null)
                {
                    _addAftnCommand = new RelayCommand(
                        param => this.AddAftn(),
                        param => true
                        );
                }
                return _addAftnCommand;
            }
        }

        public ICommand RemoveManualAftnCommand
        {
            get
            {
                if (_delManualAftnCommand == null)
                {
                    _delManualAftnCommand = new RelayCommand(
                        param => this.RemoveManualAftn(),
                        param => true
                        );
                }
                return _delManualAftnCommand;
            }
        }
        public ICommand RemoveAftnCommand
        {
            get
            {
                if (_removeAftnCommand == null)
                {
                    _removeAftnCommand = new RelayCommand(
                        param => this.RemoveAftn(),
                        param => true
                        );
                }
                return _removeAftnCommand;
            }
        }
        public ICommand AddManualAftnCommand
        {
            get
            {
                if (_addManualAftnCommand == null)
                {
                    _addManualAftnCommand = new RelayCommand(
                        param => this.AddManualAftn(),
                        param => true
                        );
                }
                return _addManualAftnCommand;
            }
        }






        #endregion

        #region Public Methods
        private bool _bOnceAlert = true;
        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {

        }


        public void Send()
        {
            if (SendNotam() == true)
            {
                parentWorkSpaces.Remove(NotamWorkspace);
                parentWorkSpaces.Remove(this);
            }

        }
        public void SendDefault()
        {
            if (SendNotam(true) == true)
            {
                parentWorkSpaces.Remove(NotamWorkspace);
                parentWorkSpaces.Remove(this);
            }

        }

        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            int printCount = SettingViewModel.GetPrintCount();
            bool IsLaser = SettingViewModel.IfLaser();
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                FlowDocument flowDocument = new FlowDocument();
                Paragraph myParagraph = new Paragraph();
                for (int i = 0; i < printCount; i++)
                {
                    foreach (string line in NotamText.Split('\n'))
                    {
                        myParagraph.Margin = new Thickness(0);
                        myParagraph.FontSize = 10;
                        //myParagraph.FontFamily
                        string s = line.Replace("\r", "\r\n");
                        myParagraph.Inlines.Add(new Run(s));
                        flowDocument.Blocks.Add(myParagraph);
                    }
                    if (IsLaser)
                    {
                        myParagraph.Inlines.Add("\r\n------------------------------------------------");
                        flowDocument.Blocks.Add(myParagraph);
                        myParagraph.Inlines.Add("\r\n\r\n");
                        flowDocument.Blocks.Add(myParagraph);
                    }

                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                printDialog.PrintDocument(paginator, "Print");
            }
        }

        public void AddAftn()
        {
            if (SelectedAftn != null && (SelectedAftns == null || !SelectedAftns.Contains(SelectedAftn)))
            {
                if (SelectedAftns == null)
                    SelectedAftns = new ObservableCollection<Aftn>();
                SelectedAftns.Add(SelectedAftn);
            }
        }

        public void RemoveAftn()
        {
            if (RemovedAftns != null && RemovedAftns.Count > 0 && SelectedAftns != null)
                foreach (var item in RemovedAftns)
                {
                    if (SelectedAftns.Contains(item))
                        SelectedAftns.Remove(item);
                }
        }
        public void AddManualAftn()
        {
            if (!String.IsNullOrEmpty(ManualAftn) && (ManualAftns == null || !ManualAftns.Contains(ManualAftn)))
            {
                if (ManualAftn.IndexOf(" ") >= 0)
                {
                    MessageBox.Show("Invalid AFTN Address");
                    return;
                }
                if (ManualAftns == null)
                    ManualAftns = new ObservableCollection<String>();
                ManualAftns.Add(ManualAftn);
            }
        }

        public void RemoveManualAftn()
        {
            if (RemovedManualAftns != null && RemovedManualAftns.Count > 0 && RemovedManualAftns != null)
                foreach (var item in RemovedManualAftns)
                {
                    if (ManualAftns.Contains(item))
                        ManualAftns.Remove(item);
                }
            //             if (ManualAftns != null)
            //                 ManualAftns.Clear();
        }


        public void OpenFile()
        {

            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".jpeg" };
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;
            }

        }

        #endregion // Public Methods

        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<IntlNotamDetailViewModel, object>> propertyGetters;
        private readonly Dictionary<string, ValidationAttribute[]> validators;

        #region DataError

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
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

        private Func<IntlNotamDetailViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<IntlNotamDetailViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion


        string IDataErrorInfo.Error
        {
            get { return (_notam as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                // error= ValidateProperty(columnName);

                if (propertyName == "CustomerType")
                {
                    // The IsCompany property of the Customer class 
                    // is Boolean, so it has no concept of being in
                    // an "unselected" state.  The CustomerViewModel
                    // class handles this mapping and validation.
                    error = this.ValidateType();
                }
                else
                {
                    error = (_notam as IDataErrorInfo)[propertyName];
                }

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateType()
        {
            return null;

            //if (this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
            //   this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
            //    return null;

            //return Strings.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion // IDataErrorInfo Members

        #region Private Methods

        bool IsNewNotam
        {
            get { return !_notamDetailService.ContainsNotam(_notam) && string.IsNullOrEmpty(_notam.Notam.Number); }
        }


        private string GenerateDefaultAFTNHeader()
        {
            if (_aftnOptions == null)
                return "";
            Aftn aftn = _aftnOptions.Where(a => a.Series.Equals(_notam.Notam.Type)).FirstOrDefault();
            if (aftn == null)
            {
                return "";
            }
            return aftn.AftnList;
        }
        private string GenerateAFTNHeader()
        {
            var aftnBuilder = new StringBuilder();
            int i = 0;
            if (SelectedAftns != null)
            {
                foreach (var item in SelectedAftns)
                {
                    //if(!firstDesRow)
                    string[] strList = item.AftnList.Split(',');
                    foreach (string str in strList)
                    {
                        aftnBuilder.Append(str);
                        if (i == 7)
                        {
                            aftnBuilder.Append("\n");
                            i = 0;
                        }
                        else
                            aftnBuilder.Append(" ");

                        i++;
                    }
                }
            }

            if (ManualAftns != null)
            {
                foreach (var item in ManualAftns)
                {
                    //if(!firstDesRow)

                    aftnBuilder.Append(item);
                    if (i == 7)
                    {
                        aftnBuilder.Append(Environment.NewLine);
                        i = 0;
                    }
                    else
                        aftnBuilder.Append(" ");

                    i++;
                }
            }
            return aftnBuilder.ToString().TrimEnd(' ');
        }
        private int NoOfMessagesBySender()
        {
            int iCount = 0;
            if (SelectedAftns != null)
            {
                foreach (var item in SelectedAftns)
                {
                    //if(!firstDesRow)
                    string[] strList = item.AftnList.Split(',');
                    iCount += strList.Count();
                }
            }

            if (ManualAftns != null)
                iCount += ManualAftns.Count();
            return iCount % 21 == 0 ? iCount / 21 : (iCount / 21) + 1;
        }

        private string GeneratePreviewNotamText(string strAFTNeader = null)
        {

            var ntmBuilder = new StringBuilder();

            #region First Line
            if (!string.IsNullOrEmpty(strAFTNeader))
                for (int i = 0; i < 120; i++)
                    ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG ");
            if (!string.IsNullOrEmpty(strAFTNeader))
            {
                ntmBuilder.Append(strAFTNeader.Replace("\r\n", ""));
                ntmBuilder.Append(Environment.NewLine);
            }
            else
                ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region Second Line

            ntmBuilder.Append(_notam.Notam.SendTime.Substring(_notam.Notam.SendTime.Length - 6, 6) + " " +
                              _notam.Notam.Origin.Code + Environment.NewLine);
            #endregion

            #region 3rd line
            string notamNum = _notam.Notam.Number ?? "****";
            if (_notam.Notam.NotamType == "N")
                ntmBuilder.Append(openPar + _notam.Notam.Type + notamNum + slash + _notam.Notam.Year + " " +
                                  "NOTAM" + _notam.Notam.NotamType + Environment.NewLine);
            else
                ntmBuilder.Append(openPar + _notam.Notam.Type + notamNum + slash + _notam.Notam.Year + " " +
                                  "NOTAM" + _notam.Notam.NotamType + " " + _notam.Notam.RefType + _notam.Notam.RefNum + "/" + _notam.Notam.RefYear + Environment.NewLine);

            #endregion

            #region Q line
            ntmBuilder.Append("Q" + closePar + _notam.Notam.FIR.Code);

            if (_notam.Notam.NotamCode != null)
            {
                ntmBuilder.Append(slash + "Q" + _notam.Notam.NotamCode.ToString());
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Traffic))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Traffic);
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Purpose))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Purpose);
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Scope))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Scope);
            }
            if (!string.IsNullOrEmpty(_notam.Notam.LowerLimit))
                ntmBuilder.Append(slash + _notam.Notam.LowerLimit);
            if (!string.IsNullOrEmpty(_notam.Notam.HigherLimit))
                ntmBuilder.Append(slash + _notam.Notam.HigherLimit);
            ntmBuilder.Append(slash);
            if (!string.IsNullOrEmpty(_notam.Notam.Latitude))
                ntmBuilder.Append(_notam.Notam.Latitude);
            if (!string.IsNullOrEmpty(_notam.Notam.Longtitude))
                ntmBuilder.Append(slash + _notam.Notam.Longtitude);
            if (!string.IsNullOrEmpty(_notam.Notam.Radius))
                ntmBuilder.Append(slash + _notam.Notam.Radius);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region A B C line

            ntmBuilder.Append("A" + closePar + _notam.Notam.FirAero);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA2))
                ntmBuilder.Append(slash + _notam.Notam.FirA2);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA3))
                ntmBuilder.Append(slash + _notam.Notam.FirA3);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA4))
                ntmBuilder.Append(slash + _notam.Notam.FirA4);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA5))
                ntmBuilder.Append(slash + _notam.Notam.FirA5);

            ntmBuilder.Append(" B" + closePar + _notam.Notam.FromDate);

            if (!string.IsNullOrEmpty(_notam.Notam.ToDate))
                ntmBuilder.Append(" C" + closePar + _notam.Notam.ToDate);

            if (!string.IsNullOrEmpty(_notam.Notam.PermEst))
                ntmBuilder.Append(" " + _notam.Notam.PermEst);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region D E line

            if (!string.IsNullOrEmpty(_notam.Notam.DFreeText))
                ntmBuilder.Append("D" + closePar + _notam.Notam.DFreeText + Environment.NewLine);


            if (!string.IsNullOrEmpty(_notam.Notam.EFreeText))
                ntmBuilder.Append("E" + closePar + _notam.Notam.EFreeText);
            #endregion

            #region F G line

            if (!string.IsNullOrEmpty(_notam.Notam.FFreeText))
                ntmBuilder.Append(Environment.NewLine + "F" + closePar + _notam.Notam.FFreeText);


            if (!string.IsNullOrEmpty(_notam.Notam.GFreeText))
                ntmBuilder.Append(" G" + closePar + _notam.Notam.GFreeText + Environment.NewLine);
            #endregion

            ntmBuilder.Append(" " + closePar);

            return ntmBuilder.ToString();
        }

        private Tuple<string, int> GenerateSendingNotamText(int iPart, int iLastIndex, string strAFTNList)
        {

            var ntmBuilder = new StringBuilder();

            #region First Line
            for (int i = 0; i < 120; i++)
                ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG");
            ntmBuilder.Append(strAFTNList);
            //ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region Second Line

            ntmBuilder.Append(_notam.Notam.SendTime.Substring(_notam.Notam.SendTime.Length - 6, 6) + " " +
                              _notam.Notam.Origin.Code + Environment.NewLine);
            #endregion

            #region Part Line

            ntmBuilder.Append(string.Format("Part {0:00}", iPart + 1) + " OF <ALLPARTS>" + Environment.NewLine);
            #endregion

            #region 3rd line
            if (_notam.Notam.NotamType == "N")
                ntmBuilder.Append(openPar + _notam.Notam.Type + _notam.Notam.Number + slash + _notam.Notam.Year + " " +
                                  "NOTAM" + _notam.Notam.NotamType + Environment.NewLine);
            else
                ntmBuilder.Append(openPar + _notam.Notam.Type + _notam.Notam.Number + slash + _notam.Notam.Year + " " +
                                  "NOTAM" + _notam.Notam.NotamType + " " + _notam.Notam.RefType + _notam.Notam.RefNum + "/" + _notam.Notam.RefYear + Environment.NewLine);
            #endregion

            #region Q line
            ntmBuilder.Append("Q" + closePar + _notam.Notam.FIR.Code);

            if (_notam.Notam.NotamCode != null)
            {
                ntmBuilder.Append(slash + "Q" + _notam.Notam.NotamCode.ToString());
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Traffic))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Traffic);
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Purpose))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Purpose);
                if (!string.IsNullOrEmpty(_notam.Notam.NotamCode.Scope))
                    ntmBuilder.Append(slash + _notam.Notam.NotamCode.Scope);
            }
            if (!string.IsNullOrEmpty(_notam.Notam.LowerLimit))
                ntmBuilder.Append(slash + _notam.Notam.LowerLimit);
            if (!string.IsNullOrEmpty(_notam.Notam.HigherLimit))
                ntmBuilder.Append(slash + _notam.Notam.HigherLimit);
            if (!string.IsNullOrEmpty(_notam.Notam.Latitude))
                ntmBuilder.Append(slash + _notam.Notam.Latitude);
            if (!string.IsNullOrEmpty(_notam.Notam.Longtitude))
                ntmBuilder.Append(slash + _notam.Notam.Longtitude);
            if (!string.IsNullOrEmpty(_notam.Notam.Radius))
                ntmBuilder.Append(slash + _notam.Notam.Radius);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region A B C line

            ntmBuilder.Append("A" + closePar + _notam.Notam.FirAero);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA2))
                ntmBuilder.Append(slash + _notam.Notam.FirA2);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA3))
                ntmBuilder.Append(slash + _notam.Notam.FirA3);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA4))
                ntmBuilder.Append(slash + _notam.Notam.FirA4);
            if (!string.IsNullOrEmpty(_notam.Notam.FirA5))
                ntmBuilder.Append(slash + _notam.Notam.FirA5);

            ntmBuilder.Append(" B" + closePar + _notam.Notam.FromDate);

            if (!string.IsNullOrEmpty(_notam.Notam.ToDate))
                ntmBuilder.Append(" C" + closePar + _notam.Notam.ToDate);

            if (!string.IsNullOrEmpty(_notam.Notam.PermEst))
                ntmBuilder.Append(" " + _notam.Notam.PermEst);
            ntmBuilder.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(_notam.Notam.DFreeText))
                ntmBuilder.Append("D" + closePar + _notam.Notam.DFreeText + Environment.NewLine);

            #endregion

            #region E line

            int iIndex = iLastIndex;
            string[] strEList = _notam.Notam.EFreeText.Split('\n');
            while (ntmBuilder.Length < 1500)
            {
                ntmBuilder.Append(strEList[iIndex]);
                iIndex++;
                if (iIndex == strEList.Count())
                {
                    iIndex = -1;
                    break;
                }
            }
            //            ntmBuilder.Append(Environment.NewLine);

            #endregion

            #region F G line
            if (!string.IsNullOrEmpty(_notam.Notam.FFreeText))
                ntmBuilder.Append(Environment.NewLine + "F" + closePar + _notam.Notam.FFreeText);


            if (!string.IsNullOrEmpty(_notam.Notam.GFreeText))
                ntmBuilder.Append(" G" + closePar + _notam.Notam.GFreeText);
            #endregion
            ntmBuilder.Append(Environment.NewLine + string.Format("//END PART {0:00}", iPart + 1) + "// )");

            return Tuple.Create(ntmBuilder.ToString(), iIndex);
        }
        private bool SendNotam(bool bDefault = false)
        {
            if (bDefault)
            {
                string strDefault = GenerateDefaultAFTNHeader();
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Caution", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return false;
                SendNotam2AFTN(strDefault);
            }
            else
            {
                int iCount = NoOfMessagesBySender();
                if (iCount == 0)
                {
                    MessageBox.Show("No AFTN selected");
                    return false;
                }
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Caution", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return false;
                if (iCount > 1)
                    MessageBox.Show("NOTAM WILL BE SENT IN " + iCount.ToString() + " MESSAGES");
                string[] strAllAFTN = GenerateAFTNHeader().Split('\n');
                int iRemaining = strAllAFTN.Count();


                for (int i = 0; i < iCount; i++)
                {
                    if (iCount > 1)
                    {
                        MessageBox.Show("SENDING NOTAM " + (i + 1).ToString() + " OF " + iCount.ToString() + " NOTAMS");
                        System.Threading.Thread.Sleep(1000);
                    }
                    string[] tempArray = strAllAFTN.Skip(i * 3).Take((iRemaining > 3) ? 3 : iRemaining).ToArray();
                    string strAFTN = "";
                    iRemaining -= tempArray.Count();
                    foreach (string s in tempArray)
                    {
                        strAFTN += (s + Environment.NewLine);
                    }
                    strAFTN = strAFTN.TrimEnd('\n').TrimEnd('\r');
                    SendNotam2AFTN(strAFTN);
                }
            }
            MessageBox.Show("Notam Message Sent Successfully");
            return true;
        }
        private void SendNotam2AFTN(string AFTNHeader)
        {
            string str = GeneratePreviewNotamText();
            if (str.Length > 1568)
            {
                List<string> strSendNotams = new List<string>();
                int index = 0, LastIndex = 0;
                do
                {
                    Tuple<string, int> tuple;
                    tuple = GenerateSendingNotamText(index++, LastIndex, AFTNHeader);
                    LastIndex = tuple.Item2;
                    strSendNotams.Add(tuple.Item1);
                } while (LastIndex >= 0);
                //no of messages
                int iNoOfMessages = strSendNotams.Count;
                int i = 0;
                foreach (string strPart in strSendNotams)
                {

                    string path = System.Configuration.ConfigurationManager.AppSettings.Get("NotamPath").ToString();
                    string time;
                    time = DateTime.Now.ToString("HHmmss") + string.Format("{0:00}", i++) + ".00L";
                    path += time;
                    File.WriteAllText(path, strPart.Replace("<ALLPARTS>", string.Format("{0:00}", iNoOfMessages)));
                    string newName = path.Replace(".00L", ".00G");
                    File.Move(path, newName);

                }
            }
            else
            {
                string path = System.Configuration.ConfigurationManager.AppSettings.Get("NotamPath").ToString();
                string strSend = GeneratePreviewNotamText(AFTNHeader);
                var time = DateTime.Now.ToString("HHmmss") + "00.00L";
                path += time;
                File.WriteAllText(path, strSend);
                string newName = path.Replace(".00L", ".00G");
                File.Move(path, newName);
            }
        }

        #endregion



    }

}
