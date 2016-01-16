using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using NOTAM.Behavior;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;

namespace NOTAM.ViewModel
{
    public class NotamViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {
        #region Fields

         Notam _notam;
        readonly NotamService _notamService;
        bool _isSelected;
        RelayCommand _saveCommand;
         RelayCommand _viewCommand;
        RelayCommand _localizeCommand;
        RelayCommand _sendCommand;
        RelayCommand _holdCommand;
        RelayCommand _detailCommand;
        private bool _isEnabled;
       

      #endregion // Fields

        

        #region Constructor
        public NotamViewModel(Notam notam, NotamService notamService, ObservableCollection<WorkspaceViewModel> parent,string archiveReason)
            : this(notam, notamService,parent)
        {
            ArchiveReason = archiveReason;
        }
        public NotamViewModel(Notam notam, NotamService notamService, ObservableCollection<WorkspaceViewModel> parent):this(notam,notamService)
        {
            parentWorkSpaces = parent;
        }

        public NotamViewModel(Notam notam, NotamService notamService)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");

            if (notamService == null)
                throw new ArgumentNullException("notamService");

            _notam = notam;
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

        #region NOTAM Properties

         [RegularExpression(@"^[\d]{10}$",ErrorMessage="Value should be 10 digits" )]
        public string Filling
        {
            get { return _notam.SendTime; }
            set
            {
                if (value == _notam.SendTime)
                    return;

                _notam.SendTime  = value;

                base.OnPropertyChanged("Filling");
            }
        }
         public Notam Notam
         {
             get { return _notam; }
         }
         private bool _FreeStyle=false;
         public bool FreeStyle
         {
             set
             {
                 _FreeStyle = value;
             }
             get
             {
                 return _FreeStyle;
             }
         }

        private List<string> _TypeOptions;
        public List<string> TypeOptions
        {
            get
            {
                if (_TypeOptions == null)
                {

                    _TypeOptions = new List<string> { "A", "B" };
                }
                return _TypeOptions;
            }
        }

        [Required(ErrorMessage ="value is required")]
        public string Type
        {
            get { return _notam.Type; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.Type)
                    return;

                _notam.Type = value;

                base.OnPropertyChanged("Type");
            }
        }

       // [Required]
        //[RegularExpression(@"^[\d]{0,4}$")]
        public string NotamNum
        {
            get {
                base.OnPropertyChanged("DisplayName");
                if (!string.IsNullOrEmpty(_notam.Number))
                    return _notam.Number;
                else
                {
                    if (_FreeStyle)
                        return _notam.Number;
                    return _notam.Id > 0 ? "H-" + _notam.Id : "****";
                }
            }
            set
            {
                if (value == _notam.Number)
                    return;

                _notam.Number = value;

                base.OnPropertyChanged("NotamNum");
            }
        }

        [Required(ErrorMessage = "Year is required")]
        [RegularExpression(@"^[\d]{0,2}$",ErrorMessage = "value is not in correct format")]
        public string Year
        {
            get { return _notam.Year; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.Year)
                    return;

                _notam.Year = value;

                base.OnPropertyChanged("Year");
            }
        }

        public string NotamType
        {
            get { return _notam.NotamType; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.NotamType)
                    return;

                _notam.NotamType = value;

                base.OnPropertyChanged("NotamType");
            }
        }

        public string Traffic
        {
            get { return _notam.NotamCode != null ? _notam.NotamCode.Traffic : null; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.NotamCode.Traffic)
                    return;
            }
        }
        public string Purpose
        {
            get { return _notam.NotamCode != null ?  _notam.NotamCode.Purpose: null ; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.NotamCode.Purpose)
                    return;
            }
        }
        public string Scope
        {
            get { return _notam.NotamCode != null ?  _notam.NotamCode.Scope: null; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.NotamCode.Scope)
                    return;
            }
        }

        [StringLength(3, MinimumLength = 0, ErrorMessage = "length must be between 0 and 3")]
        [RegularExpression(@"^[\d]{0,3}$",ErrorMessage = "value is not in correct format")]
        public string LowerLimit
        {
            get { return _notam.LowerLimit??(NotamCode != null ? "000" : null); }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.LowerLimit)
                    return;

                _notam.LowerLimit  = value;
                base.OnPropertyChanged("LowerLimit");
            }
        }

        [StringLength(3, MinimumLength = 0, ErrorMessage = "length must be between 0 and 3")]
        [RegularExpression(@"^[\d]{0,3}$", ErrorMessage = "value is not in correct format")]
        public string UpperLimit
        {
            get { return _notam.HigherLimit??(NotamCode!=null?"999":null ) ; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.HigherLimit)
                    return;

                _notam.HigherLimit = value;
                base.OnPropertyChanged("UpperLimit");
            }
        }

        [StringLength(5, MinimumLength = 0,ErrorMessage ="length must be between 0 and 5")]
  //      [RegularExpression(@"^[\d]{0,4}[\c]{1}$", ErrorMessage="value is incorrect")]
        public string Latitude
        {
            get { return _notam.Latitude ; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.Latitude)
                    return;

                _notam.Latitude  = value;
                base.OnPropertyChanged("Latitude");
            }
        }

        [StringLength(6, MinimumLength = 0, ErrorMessage = "length must be between 0 and 6")]
//        [RegularExpression(@"^[\d]{0,5}[\c]{1}$")]
        public string Longtitude
        {
            get { return _notam.Longtitude; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.Longtitude)
                    return;

                _notam.Longtitude  = value;
                base.OnPropertyChanged("Longtitude");
            }
        }

        [StringLength(3, MinimumLength = 0, ErrorMessage = "length must be between 0 and 3")]
        [RegularExpression(@"^[\d]{0,3}$",ErrorMessage ="value is not in correct format")]
        public string Radius
        {
            get { return _notam.Radius; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.Radius)
                    return;

                _notam.Radius  = value;
                base.OnPropertyChanged("Radius");
            }
        }

        public string FirAero
        {
            get { return _notam.FirAero ; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.FirAero)
                    return;

                _notam.FirAero = value;
                base.OnPropertyChanged("FirAero");
            }
        }

        public string FirAero2
        {
            get { return _notam.FirA2 ; }
            set
            {
                if (value == _notam.FirA2 )
                    return;

                _notam.FirA2 = value;
                base.OnPropertyChanged("FirAero2");
            }
        }

        public string FirAero3
        {
            get { return _notam.FirA3; }
            set
            {
                if (value == _notam.FirA3)
                    return;

                _notam.FirA3 = value;
                base.OnPropertyChanged("FirAero3");
            }
        }

        public string FirAero4
        {
            get { return _notam.FirA4; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.FirA4)
                    return;

                _notam.FirA4 = value;
                base.OnPropertyChanged("FirAero4");
            }
        }

        public string FirAero5
        {
            get { return _notam.FirA5; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.FirA5)
                    return;

                _notam.FirA5 = value;
                base.OnPropertyChanged("FirAero5");
            }
        }

        [RegularExpression(@"^[\d]{10}$",ErrorMessage="Value should be 10 digits" )]
        public string FromDate
        {
            get { return _notam.FromDate; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.FromDate)
                    return;

                _notam.FromDate = value;
                base.OnPropertyChanged("FromDate");
            }
        }

        [RegularExpression(@"^[\d]{10}$",ErrorMessage="Value should be 10 digits")]
        public string ToDate
        {
            get { return _notam.ToDate; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.ToDate)
                    return;

                _notam.ToDate  = value;
                base.OnPropertyChanged("ToDate");
            }
        }

        private List<string> _PermOptions;

        public List<string> PermOptions
        {
            get
            {
                if (_PermOptions == null)
                {

                    _PermOptions = new List<string> { "EST", "PERM" };
                }
                return _PermOptions;
            }
        }

        public string PermEst
        {
            get { return _notam.PermEst; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.PermEst)
                    return;

                _notam.PermEst  = value;
                if (value == "PERM")
                    ToDate = "";
                base.OnPropertyChanged("PermEst");
                OnPropertyChanged("IsEnabled");
                OnPropertyChanged("ToDate");
                 
            }
        }

        #region Free text Properties
        public string DFreeText
        {
            get { return _notam.DFreeText ; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.DFreeText)
                    return;

                _notam.DFreeText = value;
                base.OnPropertyChanged("DFreeText");
            }
        }

        public string EFreeText
        {
            get {
                if (string.IsNullOrEmpty(_notam.EFreeText))
                {
                    _notam.EFreeText = _notam.NotamCode != null ? _notam.NotamCode.Subject_Desc + " " + _notam.NotamCode.Condition_Desc : null;
                    if (!string.IsNullOrEmpty(_notam.EFreeText))
                        _notam.EFreeText = _notam.EFreeText.ToUpper();
                }
                return _notam.EFreeText;
            }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.EFreeText)
                    return;

                _notam.EFreeText = value;
                base.OnPropertyChanged("EFreeText");
                OnPropertyChanged("ETxtColor");
            }
        }

        public string FFreeText
        {
            get { return _notam.FFreeText; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.FFreeText)
                    return;

                _notam.FFreeText = value;
                base.OnPropertyChanged("FFreeText");
            }
        }

        public string GFreeText
        {
            get { return _notam.GFreeText; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.GFreeText)
                    return;

                _notam.GFreeText = value;
                base.OnPropertyChanged("GFreeText");
            }
        }
        #endregion

        public string ArchiveReason
        {

            get { return _notam.ArchiveReason; }
            set
            {
                if (value == _notam.ArchiveReason)
                    return;

                _notam.ArchiveReason = value;

                base.OnPropertyChanged("ArchiveReason");
            }
        }
        #endregion // Notam Properties

        #region Presentation Properties


        public override string DisplayName
        {
            get
            {
                if (this.IsNewNotam)
                {
                    if(NotamType.Equals("C"))
                        return Entity.CNotamViewModel_DisplayName;
                    if (NotamType.Equals("R"))
                        return Entity.RNotamViewModel_DisplayName;
                    return Entity.NotamViewModel_DisplayName;
                }
                else
                {
                    return _notam.Number ?? "H-"+_notam.Id.ToString();
                }
            }
        }

        /// <summary>
        /// Gets/sets whether this customer is selected in the UI.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
        }
        
        public bool IsEnabled
        {
            get
            {
                if (  (!string.IsNullOrEmpty(PermEst) && PermEst.Equals("PERM")) || NotamType=="C")
                {
                    //EFreeText = string.Empty;
                    _isEnabled = false;
                }
                else
                    _isEnabled = true;
                if (_FreeStyle)
                    _isEnabled = true;
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }

        public bool IsFreeStyle
        {
            get
            {
                return _FreeStyle;
            }
        }

        private bool _isFirEanble;
        public bool IsFirEnabled
        {
            get
            {
                //if (_notam.NotamType=="R")
                {
                    //EFreeText = string.Empty;
                    _isFirEanble = false;
                    if (_FreeStyle)
                        _isFirEanble = true;
                }
                //else
                 //   _isFirEanble = true;
                return _isFirEanble;
            }
            set
            {
                _isFirEanble = value;
            }
        }
        private bool _isFirAdEanble;
        public bool IsFirAdEnabled
        {
            get
            {
                if ( (_notam.NotamType == "R" || _notam.NotamType == "C" ) && !string.IsNullOrEmpty(_notam.FirAero))
                {
                    //EFreeText = string.Empty;
                    _isFirAdEanble = false;
                }
                else
                    _isFirAdEanble = true;
                if (_FreeStyle)
                    _isFirAdEanble = true;
                return _isFirAdEanble;
            }
            set
            {
                _isFirAdEanble = value;
            }
        }

        private string _ETxtColor;
        public String ETxtColor
        {
            get
            {
                if(EFreeText!=null && EFreeText.Length>1000)
                    _ETxtColor = "Yellow";
                else
                    _ETxtColor = "White";
                return _ETxtColor;
            }
            set
            {
                _ETxtColor = value;
            }
        }



        private void ViewText()
        {
            NotamTextViewModel workspace = new NotamTextViewModel(_notam);
            //workspace. 
            //this.Workspaces.Add(workspace);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(workspace);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
            //this.SetActiveWorkspace(workspace);
        

        }


        public Origin Origin
        {
            get { return _notam.Origin; }
            set
            {
                if (value == _notam.Origin || value == null)
                    return;

                _notam.Origin = value;

                base.OnPropertyChanged("Origin");
            }
        }

        private List<Origin> _originOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<Origin> OriginOptions
        {
            get
            {
                if (_originOptions == null)
                {

                    _originOptions = _notamService.GetAllOrigins();
                }
                return _originOptions;
            }
        }

        public FIR  FIR
        {
            get { return _notam.FIR; }
            set
            {
                if (value == _notam.FIR || value == null)
                    return;

                _notam.FIR  = value;

                base.OnPropertyChanged("FIR");
                base.OnPropertyChanged("Aerodom");
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
                }
                return _FirOptions;
            }
        }
        private Aerodom _aerodom;
        public Aerodom Aerodom
        {
            get { 
                return _notamService.GetAeroItem(_notam.FirAero); }
            set
            {
                if (!CanSet || value==null)
                    return;
                _aerodom = value;
                _notam.FirAero = value.Code;
                
                base.OnPropertyChanged("Aerodom");
            }
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

                    _AerodomOptions = _notamService.GetAllAerodomsForFIR(_notam.FIR);
                }
                return _AerodomOptions;
            }
        }

        public string SubjectCondition
        {
            get { return _notam.NotamCode.ToString(); }
        }
        public NotamCode NotamCode
        {
            get { 
                return _notam.NotamCode;
            }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.NotamCode || value == null)
                    return;
                _notam.NotamCode = value;
                if(NotamType == "N")
                    _notam.EFreeText = null;

                base.OnPropertyChanged("NotamCode");
                OnPropertyChanged("Traffic");
                OnPropertyChanged("Purpose");
                OnPropertyChanged("Scope");
                OnPropertyChanged("EFreeText");
                OnPropertyChanged("UpperLimit");
                OnPropertyChanged("LowerLimit");
                OnPropertyChanged("NotamCodeToolTip");

            }
        }

        private string _NotamCodeToolTip;
        public string NotamCodeToolTip
        {
            get
            {
                if (NotamCode!=null && string.IsNullOrEmpty(_NotamCodeToolTip ))
                {
                    if(!string.IsNullOrEmpty(NotamCode.Subject_Desc ))
                        _NotamCodeToolTip = "Subject Desc: " + NotamCode.Subject_Desc +Environment.NewLine  ;
                    if(!string.IsNullOrEmpty(NotamCode.Condition_Desc ))
                        _NotamCodeToolTip += "Condition Desc: " + NotamCode.Condition_Desc + Environment.NewLine;
                    if(!string.IsNullOrEmpty(NotamCode.Scope))
                        _NotamCodeToolTip += "Scope: " + NotamCode.Scope + Environment.NewLine;
                    if(!string.IsNullOrEmpty(NotamCode.Category ))
                        _NotamCodeToolTip += "Category: " + NotamCode.Category + Environment.NewLine;
                    if(!string.IsNullOrEmpty(NotamCode.Traffic))
                        _NotamCodeToolTip += "Traffic: " + NotamCode.Traffic + Environment.NewLine;
                    if(!string.IsNullOrEmpty(NotamCode.Purpose ))
                        _NotamCodeToolTip += "Purpose: " + NotamCode.Purpose + Environment.NewLine;
                }
                return _NotamCodeToolTip;
            }

            
            set { _NotamCodeToolTip = value;}
        }
        public bool IsNotamCodeEditable
        {
            get
            {
                if((NotamType == "C") || (NotamType=="R"))
                    return false;
                return true;
            }
        }
        void FilterCNotamCodes()
        {
            List<NotamCode> newList = new List<NotamCode>();
            foreach (NotamCode nc in _NotamCodeOptions)
            {
                switch (nc.Condition)
                {
                    case "AK":
                    case "AL":
                    case "AO":
                    case "CC":
                    case "CN":
                    case "HV":
                    case "XX":
                    case "KK":
                        newList.Add(nc);
                        break;
                }
            }
            _NotamCodeOptions = newList;

        }

        private List<NotamCode> _NotamCodeOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<NotamCode> NotamCodeOptions
        {
            get
            {
                if(_NotamCodeOptions == null)
                {

                    _NotamCodeOptions = _notamService.GetAllNotamCodes();
                    if (!string.IsNullOrEmpty(NotamType) && (NotamType=="C" || NotamType=="R") && NotamCode!=null && !string.IsNullOrEmpty(NotamCode.Subject) )
                    {
                        _NotamCodeOptions = _NotamCodeOptions.Where(x => x.Subject.Equals(NotamCode.Subject)).ToList();
                    }
                }
                return _NotamCodeOptions;
            }
        }


        public int RefId
        {
            get { return _notam.RefId; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.RefId)
                    return;

                _notam.RefId = value;

                base.OnPropertyChanged("RefId");
            }
        }
        
        public string RefType
        {
            get { return _notam.RefType; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.RefType)
                    return;

                _notam.RefType = value;

                base.OnPropertyChanged("RefType");
            }
        }

        [RegularExpression(@"^[\d]{0,4}$")]
        public string RefNum
        {
            get { return _notam.RefNum; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.RefNum)
                {
                    GetByNumber();
                    return;
                }

                _notam.RefNum = value;

                base.OnPropertyChanged("RefNum");
            }
        }

        [RegularExpression(@"^[\d]{0,2}$")]
        public string RefYear
        {
            get { return _notam.RefYear; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _notam.RefYear)
                    return;

                _notam.RefYear = value;

                base.OnPropertyChanged("RefYear");
            }
        }
        public int NotamId
        {
            get { return _notam.Id; }
        }
        public Visibility RefVisibility
        {
            get
            {
                if (NotamType.Equals("C") || NotamType.Equals("R") || _FreeStyle)
                    return Visibility.Visible;
                return Visibility.Hidden;
            }
        }
        public string HoldCaption
        {
            get {
                if (_FreeStyle)
                    return "Send";
                return string.IsNullOrEmpty(_notam.Number) ? "HOLD" : "Send Again"; 
            }
        }

        /// <summary>
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
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        public ICommand HoldCommand
        {
            get
            {
                if (_holdCommand == null)
                {
                    _holdCommand = new RelayCommand(
                        param => this.Hold(),
                        param => this.CanSave
                        );
                }
                return _holdCommand;
            }
        }

        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(
                        param => this.Send(),
                        param => !this.IsNewNotam 
                        );
                }
                return _sendCommand;
            }
        }

        
        /// <summary>
        /// Returns a command that Localize the Notam.
        /// </summary>
        public ICommand LocalizeCommand
        {
            get
            {
                if (_localizeCommand == null)
                {
                    _localizeCommand = new RelayCommand(
                        param => this.Localize(),
                        param => true
                        );
                }
                return _localizeCommand;
            }
        }

        /// <summary>
        /// Returns a command for Notam details.
        /// </summary>
        public ICommand DetailCommand
        {
            get
            {
                if (_detailCommand == null)
                {
                    _detailCommand = new RelayCommand(
                        param => this.ShowDetail(),
                        param => true
                        );
                }
                return _detailCommand;
            }
        }
        RelayCommand _MenuRemoveCommand;

        public ICommand MenuRemoveCommand
        {
            get
            {
                if (_MenuRemoveCommand == null)
                {
                    _MenuRemoveCommand = new RelayCommand(RemoveNotam, param => true);

                }
                return _MenuRemoveCommand;
            }
        }
        RelayCommand _MenuArchiveCommand;

        public ICommand MenuArchiveCommand
        {
            get
            {
                if (_MenuArchiveCommand == null)
                {
                    _MenuArchiveCommand = new RelayCommand(ArchiveNotam, param => true);

                }
                return _MenuArchiveCommand;
            }
        }


        public void Logger(String strLogText)
        {

            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            string subPath = "c:\\AISAdmin\\"; // your code goes here
            bool exists = System.IO.Directory.Exists((subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory((subPath));
            subPath += "LOG\\"; // your code goes here
            exists = System.IO.Directory.Exists((subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory((subPath));
            System.IO.StreamWriter file = new System.IO.StreamWriter(subPath+"log.txt", true);
            string strLine = "[" + System.DateTime.Now.ToString("yyMMddHHmm") + "] " + strLogText;
            file.WriteLine(strLine);

            file.Close();

        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        public void ArchiveNotam(Object parameter)
        {
            var notamViewModel = parameter as NotamViewModel;
            if (notamViewModel != null)
            {
                if (notamViewModel._notam != null && notamViewModel._notam.Status == "D")
                {
                    _notamService.Archive(notamViewModel._notam);
                    Logger("NOTAM " + notamViewModel._notam.Type + notamViewModel._notam.Number
                        + "/" + notamViewModel._notam.Year + " Archived");
                }
                else
                {
                    MessageBox.Show(Entity.NotValidStateForArchive);
                }
            }

        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        public void RemoveNotam(Object parameter)
        {
            var notamViewModel = parameter as NotamViewModel;
            if (notamViewModel != null)
            {
                _notamService.Delete(notamViewModel._notam);
                Logger("NOTAM " + notamViewModel._notam.Type + notamViewModel._notam.Number
                    + "/" + notamViewModel._notam.Year + " Deleted");
            }

        }
        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        /// 

        public Notam getNotam()
        {
            return _notam;
        }
        public void Save()
        {
            if (!_notam.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewNotam)
            {
                _notamService.Insert(_notam);
            }
            else
                _notamService.Update(_notam);

            base.OnPropertyChanged("DisplayName");
            OnPropertyChanged("NotamNum");
        }
        private string _SubjectCode;
        public void GetByNumber()
        {
            Notam refrenceNotam = null;
            string strStatus = (NotamCode!=null && NotamCode.Subject == "KK" && NotamCode.Condition == "KK") ? null : "D";
            if (!string.IsNullOrEmpty(RefNum))
                refrenceNotam = _notamService.GetByNumber(RefNum,RefType,RefYear,strStatus);
            if (refrenceNotam == null)
            {
                if(!_FreeStyle)
                    MessageBox.Show("Invalid NOTAM Number");
                //throw new InvalidOperationException(Entity.NotamViewModel_Exception_CannotLoad);
            }
            else
            {
                //NotamNum = refrenceNotam.Number;
                RefYear = refrenceNotam.Year;
                RefType = refrenceNotam.Type;
                RefId = refrenceNotam.Id;
                Type = RefType;
                //Year = RefYear;
                FIR = refrenceNotam.FIR;
                _notam.NotamCode = refrenceNotam.NotamCode;
                Origin = refrenceNotam.Origin;
                UpperLimit = refrenceNotam.HigherLimit;
                LowerLimit = refrenceNotam.LowerLimit;
                Filling = refrenceNotam.SendTime;
                //FromDate = refrenceNotam.FromDate;
                if (NotamType == "R")
                {
                    ToDate = refrenceNotam.ToDate;
                    PermEst = refrenceNotam.PermEst;
                }
                Latitude = refrenceNotam.Latitude;
                Longtitude = refrenceNotam.Longtitude;
                Radius = refrenceNotam.Radius;
                DFreeText = refrenceNotam.DFreeText;
                EFreeText = refrenceNotam.EFreeText;
                FFreeText = refrenceNotam.FFreeText;
                GFreeText = refrenceNotam.GFreeText;
                _SubjectCode = NotamCode.Subject;
                _AerodomOptions = null;
                _NotamCodeOptions = null;
                base.OnPropertyChanged("AerodomOptions");
                base.OnPropertyChanged("NotamCodeOptions");
                base.OnPropertyChanged("NotamCode");
                Aerodom = _notamService.GetAeroItem(refrenceNotam.FirAero);

            }
        }

        public void Localize()
        {
         
            this.Origin = OriginOptions.Where(x => x.Code.Equals("OIIIYNYX")).FirstOrDefault();
            this.FIR = FirOptions.Where(x => x.Code.Equals("OIIX")).FirstOrDefault();
            _AerodomOptions = null;
            _NotamCodeOptions = null;
            base.OnPropertyChanged("AerodomOptions");
            base.OnPropertyChanged("NotamCodeOptions");
            this.Year = DateTime.Now.ToString("yy");
            this.FromDate = DateTime.Now.ToString("yyMMddHHmm");
            switch (NotamType)
            {
                case "R":
                    Aerodom = _notamService.GetAeroItem(_notam.FirAero);
                    GetByNumber();
                    base.OnPropertyChanged("Aerodom");
              
                    //this.NotamNum = "****";re
                    break;
                case "C":
                    this.ToDate = string.Empty;
                    //this.NotamNum = "****";
                    break;
                default:
                this.NotamType = "N";
                //this.NotamNum = "****";
                  
                    break;
            }
            this.Filling = DateTime.Now.ToString("yyMMddHHmm");
        }

    

        public void ShowDetail()
        {
            //if(parentWorkSpaces==null)
            //    parentWorkSpaces = new NotamDetailViewModel(notamDetail, detailService, _notamService, parentWorkSpaces, this);



            parentWorkSpaces.Add(this);
            this.SetActiveWorkspace(this);
        }
        bool CheckdateTimeValidity(string StrDateTime)
        {
            if (String.IsNullOrEmpty(StrDateTime))
                return true;
            if (StrDateTime.Length != 10)
                return false;
            try
            {
                DateTime expectedDate = DateTime.ParseExact(StrDateTime, "yyMMddHHmm", null);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;

        }
        public void Send()
        {
            _notamService.UpdateWithNumber(_notam);
            base.OnPropertyChanged("NotamNum");
            base.OnPropertyChanged("DisplayName");
           var notamDetail = NOTAM.SERVICE.Model.NotamDetail.CreateNewNotamDetail(_notam) ;
           var detailService = new NotamDetailService(_notamService._dataContext);
            var result = detailService.GetByNotamId(_notam.Id);
            if (result != null)
                notamDetail = result;
            var workspace = new NotamDetailViewModel(notamDetail, detailService,_notamService,parentWorkSpaces,this);
           parentWorkSpaces.Add(workspace);
           this.SetActiveWorkspace(workspace);
        }


        
        public void Hold()
        {
            
            if (string.IsNullOrEmpty(_notam.Number))
            if (ToDate != null && ToDate.Length > 0)
            {
                CultureInfo MyCultureInfo = new CultureInfo("en-US");
                DateTime dtfrom = DateTime.ParseExact(FromDate, "yyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime dtto = DateTime.ParseExact(ToDate, "yyMMddHHmm", CultureInfo.InvariantCulture);
                TimeSpan s = dtto.Subtract(dtfrom);
                if (s.Days > 90)
                {
                    MessageBoxResult dresult = MessageBox.Show("Different between FromDate And ToDate is more than 90 days\nAre you sure want to hold?", "Caution", MessageBoxButton.YesNo);
                    if (dresult == MessageBoxResult.No)
                        return ;

                }
            }
            if (!_notam.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (string.IsNullOrEmpty(_notam.HigherLimit))
                _notam.HigherLimit = "999";
            if (string.IsNullOrEmpty(_notam.LowerLimit))
                _notam.LowerLimit = "000";
            if (_FreeStyle)
            {
                if (!_notamService.CheckUnique(NotamNum, Type, Year))
                {
                    MessageBox.Show("Invalid NOTAM Number", "ERROR");
                    return;

                }
                _notamService.UpdateWithNumber(_notam);
            }
            else
            {
                if (this.IsNewNotam)
                {
                    _notamService.Insert(_notam);
                }
                else
                {
                    if (string.IsNullOrEmpty(_notam.Number))
                        _notamService.Update(_notam);
                    else
                        _notam = _notamService.GetByNumber(_notam.Number, _notam.Type, _notam.Year);
                }

            }
            var notamDetail = NOTAM.SERVICE.Model.NotamDetail.CreateNewNotamDetail(_notam);
            var detailService = new NotamDetailService(_notamService._dataContext);
            var result = detailService.GetByNotamId(_notam.Id);
            if (result != null)
                notamDetail = result;
            var workspace = new NotamDetailViewModel(notamDetail, detailService,_notamService, parentWorkSpaces,this);
            parentWorkSpaces.Add(workspace);
            workspace.FreeStyle = FreeStyle;


            this.SetActiveWorkspace(workspace);
            
            OnPropertyChanged("NotamNum");
        }
            
       


        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewNotam
        {
            get { return !_notamService.ContainsNotam(_notam); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get
            {
                return this.ValidateType() && _notam.IsValid && string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount && NotamCode!=null;
            }
        }
        bool CanSet
        {
            get
            {
                return string.IsNullOrEmpty(_notam.Number) || _FreeStyle;
            }
        }

        

        #endregion // Private Helpers

        
        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<NotamViewModel, object>> propertyGetters;
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

                return string.Join(Environment.NewLine,errors.ToArray());
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

        private Func<NotamViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<NotamViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion



        private bool ValidateType()
        {
            var validation = true;
            if (_FreeStyle)
                return true;
            if (_notam == null)
                return false;
            if (!string.IsNullOrEmpty(_notam.Number))
                return true;
            if (string.IsNullOrEmpty(FirAero))
                return false;
            if ((NotamType == "C" || NotamType == "R") && string.IsNullOrEmpty(RefNum))
                return false;
            if (String.IsNullOrEmpty(PermEst) && String.IsNullOrEmpty(ToDate) && NotamType!="C")
                return false;
            if (PermEst=="EST" && String.IsNullOrEmpty(ToDate))
                return false;
            if (!string.IsNullOrEmpty(RefType) && !RefType.Equals(Type))
                return false;
            if (!CheckdateTimeValidity(FromDate))
                return false;
            if(!CheckdateTimeValidity(ToDate))
                return false;
            if (!String.IsNullOrEmpty(ToDate) && !String.IsNullOrEmpty(FromDate)  )
            {
                if (ToDate.Length == 10 && FromDate.Length == 10)
                {
                    if (Convert.ToInt64(ToDate) <= Convert.ToInt64(FromDate))
                        return false;

                }
                else
                    return false;

            }
            string currDate = DateTime.Now.ToString("yyMMddHHmm");
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if(!customPrincipal.IsAdmin(UserRole.Administrator))
            if (Convert.ToInt64(currDate) > Convert.ToInt64(FromDate))
                validation = false;
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
