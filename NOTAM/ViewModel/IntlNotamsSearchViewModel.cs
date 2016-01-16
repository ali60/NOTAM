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
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using System.Text;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Controls;

namespace NOTAM.ViewModel
{
    public class IntlNotamsSearchViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {
        private ICommand _runCommand;
        private ICommand _printCommand;
        private ICommand _saveCommand;
        private ICommand _blancCommand;
        private ICommand _checklistCommand;
        private ICommand _displayCommand;
        private ICommand _gapAnalysisCommand;

        readonly IntlNotamService _notamService;
        readonly NotamService _notamServiceLocal;
        readonly AerodomService _aerodomService;
        readonly FIRService _firService;
        readonly NotamCodeService _notamcodeService;
        #region Constructor
        public IntlNotamsSearchViewModel(IntlNotamService notamService,NotamService notamServiceLocal, AerodomService aerodomService, NotamCodeService notamcodeService, ObservableCollection<WorkspaceViewModel> parent)
            : this(notamService, aerodomService, notamcodeService)
        {
            parentWorkSpaces = parent;
            _notamServiceLocal = notamServiceLocal;

        }
        public IntlNotamsSearchViewModel(IntlNotamService notamService, AerodomService aerodomService, NotamCodeService notamcodeService)
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
        [RegularExpression(@"^[\d]{4}$", ErrorMessage = "Value should be 4 digits")]
        public String From
        {
            get;
            set;
        }

        [RegularExpression(@"^[\d]{4}$", ErrorMessage = "Value should be 4 digits")]
        public String To
        {
            get;
            set;
        }
        public String FromDate
        {
            get;
            set;
        }
        public String ToDate
        {
            get;
            set;
        }

        public String Type
        {
            get;
            set;
        }

        public String OriginItem
        {
            get;
            set;
        }
        public String Year
        {
            get;
            set;
        }
        public String QCode
        {
            get;
            set;
        }
        private FIR _FIR;
        public FIR FIR
        {
            get
            {
                return _FIR;
            }
            set
            {
                _AerodomOptions = null;
                _FIR = value;
                OnPropertyChanged("FIR");
                OnPropertyChanged("AerodomOptions");

            }
        }

        
        private string _progressVal="30" ;
        public string ProgressVal
        {
            get { return _progressVal; }
            set
            {
                _progressVal = value;
                OnPropertyChanged("ProgressVal");
            }
        }
        private List<FIR> _FirOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<FIR> FirOptions
        {
            get
            {
                if (_FirOptions == null)
                {

                    _FirOptions = _notamService.GetAllFIRs();
//                     this.FIR = _FirOptions.Where(x => x.Code.Equals("OIIX")).FirstOrDefault();
//                     _AerodomOptions = null;
                    OnPropertyChanged("AerodomOptions");
                }
                return _FirOptions;
            }
        }
        public Aerodom Aerodom
        {
            get;
            set;
        }

        private List<Aerodom> _AerodomOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<Aerodom> AerodomOptions
        {
            get
            {
                if (_AerodomOptions == null)
                {
                    if (this.FIR != null)
                        _AerodomOptions = _aerodomService.GetAerodoms().Where(n => n.FIR == this.FIR).ToList();
                    else
                        _AerodomOptions = _aerodomService.GetAerodoms();

                }
                return _AerodomOptions;
            }
        }

        private string _ReportResult;
        public string ReportResult
        {
            get { return _ReportResult; }
            set
            {
                if (value == _ReportResult)
                    return;

                _ReportResult = value;
                base.OnPropertyChanged("ReportResult");
            }

        }
        private string _ImportedListText;
        public string ImportedListText
        {
            get { return _ImportedListText; }
            set
            {
                if (value == _ImportedListText)
                    return;

                _ImportedListText = value;
                base.OnPropertyChanged("ImportedListText");
            }

        }
        #endregion // Summary Properties

        #region Presentation Properties


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
                        param => this.RunReport(),
                        param => this.CanRun
                        );
                }
                return _runCommand;
            }
        }

        public ICommand BlancCommand
        {
            get
            {
                if (_blancCommand == null)
                {
                    _blancCommand = new RelayCommand(
                        param => this.GenerateBlancReport(),
                        param => this.CanRun
                        );
                }
                return _blancCommand;
            }
        }
        
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.SaveToFile(),
                        param => this.CanPrintSave
                        );
                }
                return _saveCommand;
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
                        param => this.CanPrintSave
                        );
                }
                return _printCommand;
            }
        }
        public ICommand ValidNotamsCommand
        {
            get
            {
                if (_checklistCommand == null)
                {
                    _checklistCommand = new RelayCommand(
                        param => this.DisplayValidNotams(),
                        param => this.CanReport
                        );
                }
                return _checklistCommand;
            }
        }

        public ICommand DisplayArchivesCommand
        {
            get
            {
                if (_displayCommand== null)
                {
                    _displayCommand = new RelayCommand(
                        param => this.DisplayArchiveNotams(),
                        param => this.CanReport
                        );
                }
                return _displayCommand;
            }
        }
        public bool bCanGap
        {
            get { return CanReport && !string.IsNullOrEmpty(_ImportedListText); }
        }
        public ICommand GAPCommand
        {
            get
            {
                if (_gapAnalysisCommand == null)
                {
                    _gapAnalysisCommand = new RelayCommand(
                        param => this.GenerateGAPAnalysisReport(),
                        param => bCanGap
                        );
                }
                return _gapAnalysisCommand;
            }
        }


        bool CanRun
        {
            get
            {
                return this.ValidateType() && string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount;
            }
        }
        bool CanReport
        {
            get
            {
                return _FIR!=null && CanRun;
            }
        }

        bool CanPrintSave
        {
            get
            {
                return !string.IsNullOrEmpty(ReportResult);
            }
        }

        #endregion // Presentation Properties

        private String openPar = "(";
        private String closePar = ")";
        private String slash = "/";

        string GenerateNotamText(IntlNotam notam)
        {
            var ntmBuilder = new StringBuilder();

            #region First Line
            if (notam.Origin != null && !string.IsNullOrEmpty(notam.SendTime))
                ntmBuilder.Append(notam.SendTime.Substring(notam.SendTime.Length - 6, 6) + " " +
                                  notam.Origin.Code + Environment.NewLine);
            #endregion

            #region 3rd line
            ntmBuilder.Append(openPar + notam.Type + notam.Number + slash + notam.Year + " " +
                              "NOTAM" + notam.NotamType + Environment.NewLine);
            #endregion

            #region Q line
            if (notam.FIR != null)
                ntmBuilder.Append("Q" + closePar + notam.FIR.Code);

            if (notam.NotamCode != null)
            {
                ntmBuilder.Append(slash + notam.NotamCode.ToString());
                if (!string.IsNullOrEmpty(notam.NotamCode.Traffic))
                    ntmBuilder.Append(slash + notam.NotamCode.Traffic);
                if (!string.IsNullOrEmpty(notam.NotamCode.Purpose))
                    ntmBuilder.Append(slash + notam.NotamCode.Purpose);
                if (!string.IsNullOrEmpty(notam.NotamCode.Scope))
                    ntmBuilder.Append(slash + notam.NotamCode.Scope);
            }
            if (!string.IsNullOrEmpty(notam.LowerLimit))
                ntmBuilder.Append(slash + notam.LowerLimit);
            if (!string.IsNullOrEmpty(notam.HigherLimit))
                ntmBuilder.Append(slash + notam.HigherLimit);
            if (!string.IsNullOrEmpty(notam.Latitude))
                ntmBuilder.Append(slash + notam.Latitude);
            if (!string.IsNullOrEmpty(notam.Longtitude))
                ntmBuilder.Append(slash + notam.Longtitude);
            if (!string.IsNullOrEmpty(notam.Radius))
                ntmBuilder.Append(slash + notam.Radius);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region A B C line

            ntmBuilder.Append("A" + closePar + notam.FirAero);
            if (!string.IsNullOrEmpty(notam.FirA2))
                ntmBuilder.Append(slash + notam.FirA2);
            if (!string.IsNullOrEmpty(notam.FirA3))
                ntmBuilder.Append(slash + notam.FirA3);
            if (!string.IsNullOrEmpty(notam.FirA4))
                ntmBuilder.Append(slash + notam.FirA4);
            if (!string.IsNullOrEmpty(notam.FirA5))
                ntmBuilder.Append(slash + notam.FirA5);

            ntmBuilder.Append(" B" + closePar + notam.FromDate);

            ntmBuilder.Append(" C" + closePar + notam.ToDate);

            if (!string.IsNullOrEmpty(notam.PermEst))
                ntmBuilder.Append(" " + notam.PermEst);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region D E line

            if (!string.IsNullOrEmpty(notam.DFreeText))
                ntmBuilder.Append("D" + closePar + notam.DFreeText + Environment.NewLine);


            if (!string.IsNullOrEmpty(notam.EFreeText))
                ntmBuilder.Append("E" + closePar + notam.EFreeText);
            #endregion

            #region F G line
            ntmBuilder.Append(closePar);
            #endregion

            return ntmBuilder.ToString();

        }
        void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                FlowDocument flowDocument = new FlowDocument();
                foreach (string line in ReportResult.Split('\n'))
                {
                    Paragraph myParagraph = new Paragraph();
                    myParagraph.Margin = new Thickness(0);
                    myParagraph.Inlines.Add(new Run(line));
                    flowDocument.Blocks.Add(myParagraph);
                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                printDialog.PrintDocument(paginator, "Print");
            }

        }
        void SaveToFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Intl Notams"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                System.IO.File.WriteAllText(filename, ReportResult);
            }

        }
        void RunReport()
        {
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = (FIR==null) ? "":FIR.Code;
            filter.NotamAeroFilter = (Aerodom==null) ? "":Aerodom.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = From;
            filter.ToDateFilter = To;
            List<IntlNotam> ListResult = _notamService.GetFilterNotams(filter);
            if (ListResult == null)
                return;
            if (ListResult.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            var ntmBuilder = new StringBuilder();
            string str = "";
            #region Header
            str = "Date : " + System.DateTime.Now.ToLongDateString() + Environment.NewLine;
            ntmBuilder.Append(str);
            str = "Time : " + System.DateTime.Now.ToLongTimeString() + Environment.NewLine;
            ntmBuilder.Append(str);
            ntmBuilder.Append("Base on following query" + Environment.NewLine);
            ntmBuilder.Append("ORIGIN : " +OriginItem+ Environment.NewLine);
            ntmBuilder.Append("FIR : " + (FIR==null ? "": FIR.Code) +Environment.NewLine);
            ntmBuilder.Append("Aerodome :" + (Aerodom==null? "":Aerodom.Code )+ Environment.NewLine);
            ntmBuilder.Append("Type : " +Type+ Environment.NewLine);
            ntmBuilder.Append("Year : " + Year + Environment.NewLine);
            ntmBuilder.Append("==============================================================" + Environment.NewLine);
            #endregion
            foreach (IntlNotam notam in ListResult)
            {
                ntmBuilder.Append("__________________________________________________________________" + Environment.NewLine);
                str = GenerateNotamText(notam);
                ntmBuilder.Append(str + Environment.NewLine);
            }
            _ReportResult = ntmBuilder.ToString();
            base.OnPropertyChanged("ReportResult");

        }

        void DisplayValidNotams()
        {
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = (FIR == null) ? "" : FIR.Code;
            filter.NotamAeroFilter = (Aerodom == null) ? "" : Aerodom.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = From;
            filter.ToDateFilter = To;
            filter.notamStatus = "D";
            List<IntlNotam> ListResult = _notamService.GetFilterNotams(filter);

            if (ListResult == null)
                return;
            if (ListResult.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            DisplayInGrid(filter);

        }
        void DisplayArchiveNotams()
        {
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = (FIR == null) ? "" : FIR.Code;
            filter.NotamAeroFilter = (Aerodom == null) ? "" : Aerodom.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = From;
            filter.ToDateFilter = To;
            filter.notamStatus = "A";
            List<IntlNotam> ListResult = _notamService.GetFilterNotams(filter);

            if (ListResult == null)
                return;
            if (ListResult.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            DisplayInGrid(filter);

        }

        void DisplayInGrid(NotamFilter filter)
        {
            AllIntlNotamsViewModel workspace =
                parentWorkSpaces.FirstOrDefault(vm => vm is AllIntlNotamsViewModel)
                as AllIntlNotamsViewModel;
            //if (workspace == null)
            //{
            workspace = new AllIntlNotamsViewModel(_notamService, parentWorkSpaces,filter);
            parentWorkSpaces.Add(workspace);
            // }
            this.SetActiveWorkspace(workspace);

        }
        string AddValidNotamStr(List<IntlNotam> ListOfNotams)
        {
            string strValid = "";
            int i = 0;
            foreach (IntlNotam ntm in ListOfNotams)
            {
                if (!string.IsNullOrEmpty(ntm.EFreeText) && ntm.EFreeText.Contains("THIS NOTAM IS NOT RECIEVED BY TEHRAN NOF"))
                    continue;
                strValid += ntm.Number + ",";
                i++;
                if (i % 12 == 0)
                    strValid += Environment.NewLine;
            }
            if (strValid.Length > 0 && strValid[strValid.Length - 1] == ',')
                strValid = strValid.Remove(strValid.Length - 1);
            strValid += Environment.NewLine;
            return strValid;

        }
        string AddBlancNotamStr(List<IntlNotam> ListOfNotams)
        {
            string strValid = "";
            int i = 0;
            List<IntlNotam> newList = ListOfNotams.OrderByDescending(n => Int32.Parse(n.Number)).ToList();
            int iLastNum=Int32.Parse(newList[0].Number);
            for (int iNumber=1;iNumber<iLastNum;iNumber++)
            {
                IntlNotam ntFind = ListOfNotams.Where(x => x.Number == iNumber.ToString()).FirstOrDefault();
                if (ntFind != null)
                {
                    if (!string.IsNullOrEmpty(ntFind.EFreeText) && !ntFind.EFreeText.Contains("THIS NOTAM IS NOT RECIEVED BY TEHRAN NOF"))
                        continue;
                }
                strValid += string.Format("{0:0000}",iNumber) + ",";
                i++;
                if (i % 12 == 0)
                    strValid += Environment.NewLine;
            }
            if (strValid.Length > 0 && strValid[strValid.Length - 1]==',')
                strValid = strValid.Remove(strValid.Length - 1);
            strValid += Environment.NewLine;
            return strValid;

        }
        bool CheckNotamNumberInList(string strNotamNo, string strCheckList)
        {
            if (strCheckList.IndexOf(strNotamNo) >= 0)
                return true;
            return false;
        }
        bool IsDigitsOnly(string str)
        {
            if (str.Length < 4)
                return false;
            foreach (char c in str)
            {
                if (c == '\n' || c == '\r')
                    continue;
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        List<string> GenerateNotalListFromImportedList(string strCheckList)
        {
            string[] strLine = strCheckList.Split('\n');
            List<string> strList=new List<string>();
            foreach (string s in strLine)
            {
                if(s.ToLower().IndexOf("latest publication")>=0)
                    break;
                string[] strListLine = s.Split(' ');
                if(strListLine.Count()==0)
                    continue;
                foreach (string sItem in strListLine)
                {
                    if (IsDigitsOnly(sItem))
                        strList.Add(sItem.Replace("\r",""));
                }
            }
            return strList;
        }
        string AddDiffrenceNotamsFromLastList(List<IntlNotam> ListOfNotams)
        {
            string strValid = "";
            int i = 0;
//             NotamCode nCode= _notamcodeService.GetNotamCodes().Where(o => (o.Subject + o.Condition).Equals("KKKK")).FirstOrDefault();
//             IntlNotam ntmCheckList = ListOfNotams.Where(m => (m.NotamCode == nCode)).OrderByDescending(x => x.Number).FirstOrDefault();
//             if (ntmCheckList == null)
//                 return "";
            List<string> strNotamNums = GenerateNotalListFromImportedList(_ImportedListText);
            foreach (string strNum in strNotamNums)
            {
                IntlNotam ntFind = ListOfNotams.Where(x => x.Number == strNum).FirstOrDefault();
                if (ntFind == null)
                    strValid += strNum + ",";
                else
                    if ( !string.IsNullOrEmpty(ntFind.EFreeText) && ntFind.EFreeText.Contains("THIS NOTAM IS NOT RECIEVED BY TEHRAN NOF"))
                    strValid += strNum + ",";
                i++;
                if (i % 12 == 0)
                    strValid += Environment.NewLine;
            }
            if (strValid.Length > 0 && strValid[strValid.Length - 1] == ',')
                strValid = strValid.Remove(strValid.Length - 1);
            strValid += Environment.NewLine;
            return strValid;

        }
        void GenerateGAPAnalysisReport()
        {
            var rptBuilder = new StringBuilder();
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = FIR.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = FromDate;
            filter.ToDateFilter = ToDate;
            filter.FromNumberFilter= From;
            filter.ToNumberFilter = To;
            filter.notamStatus = "AD";
            List<IntlNotam> ListResult = _notamService.GetFilterNotams(filter).OrderBy(t => t.Number).ToList();
            if (ListResult.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            string str = "Result For  FIR=" + FIR.Code + Environment.NewLine;
            rptBuilder.Append(str);
//             str = "Valid Notams:" + Environment.NewLine;
//             rptBuilder.Append(str);
//             str = AddValidNotamStr(ListResult);
//             rptBuilder.Append(str);
//             str = "Blanc Notams:" + Environment.NewLine;
//             rptBuilder.Append(str);
//             str = AddBlancNotamStr(ListResult);
//             rptBuilder.Append(str);
            str = AddDiffrenceNotamsFromLastList(ListResult);
            if(!string.IsNullOrEmpty(str))
            {
                rptBuilder.Append("Missing Notams From Last CheckList:" + Environment.NewLine);
                rptBuilder.Append(str);
                //break;
            }
            _ReportResult = rptBuilder.ToString();
            base.OnPropertyChanged("ReportResult");

        }

        void GenerateBlancReport()
        {
            var rptBuilder = new StringBuilder();
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = FIR.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = FromDate;
            filter.ToDateFilter = ToDate;
            filter.FromNumberFilter = From;
            filter.ToNumberFilter = To;
            filter.notamStatus = "AD";
            List<IntlNotam> ListResult = _notamService.GetFilterNotams(filter).OrderBy(t => t.Number).ToList();
            if (ListResult.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            string str = "Result For  FIR=" + FIR.Code + Environment.NewLine;
            rptBuilder.Append(str);
            //             str = "Valid Notams:" + Environment.NewLine;
            //             rptBuilder.Append(str);
            //             str = AddValidNotamStr(ListResult);
            //             rptBuilder.Append(str);
            str = "Blanc Notams:" + Environment.NewLine;
            rptBuilder.Append(str);
            str = AddBlancNotamStr(ListResult);
            rptBuilder.Append(str);
            _ReportResult = rptBuilder.ToString();
            base.OnPropertyChanged("ReportResult");

        }


        void CheckListSummary()
        {
            NotamFilter filter = new NotamFilter();
            filter.NotamFirFilter = (FIR == null) ? "" : FIR.Code;
            filter.NotamAeroFilter = (Aerodom == null) ? "" : Aerodom.Code;
            filter.TypeFilter = Type;
            filter.YearFilter = Year;
            filter.FromDateFilter = From;
            filter.ToDateFilter = To;
            List<IntlNotam> notamList = _notamService.GetFilterNotams(filter);
            if (notamList == null)
                return;
            if (notamList.Count == 0)
            {
                MessageBox.Show("No Result");
                return;
            }
            try
            {
                var queryYear = notamList.GroupBy(item => item.Year).Select(group =>
                                        new
                                        {
                                            Year = group.Key,
                                            nums = group.OrderBy(x => x.Number)
                                        }).OrderBy(group => group.Year);
                //                newTable.Rows.Add(newTable.Rows[1]);
                int icol = 1;
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
                            strItemE += "\r\n           ";

                        }
                    }
                    icol = 1;
                }
                strItemE += "\r\nLATEST PUBLICATIONS: ";
                NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewRNotam();
                newNotam.NotamCode = _notamcodeService.GetNotamCodes().Where(o => (o.Subject + o.Condition).Equals("KKKK")).FirstOrDefault();
                List<Notam> AllNotams = _notamServiceLocal.GetAllNotams();
                Notam ntm = AllNotams.Where(m => (m.NotamCode == newNotam.NotamCode)).OrderByDescending(x => x.Number).FirstOrDefault();
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
                NotamViewModel workspace = new NotamViewModel(newNotam, _notamServiceLocal, parentWorkSpaces);
                parentWorkSpaces.Add(workspace);

                this.SetActiveWorkspace(workspace);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }

        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<IntlNotamsSearchViewModel, object>> propertyGetters;
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

        private Func<IntlNotamsSearchViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<IntlNotamsSearchViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion



        private bool ValidateType()
        {
            var validation = true;

            return validation;

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
