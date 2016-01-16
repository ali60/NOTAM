using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Threading;
using NOTAM.Behavior;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class NotamSearchViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {
     #region Fields
        readonly NotamService _notamService;
        RelayCommand _searchCommand;
        RelayCommand _resetCommand;
     #endregion // Fields

        

        #region Constructor
       
        public NotamSearchViewModel( NotamService notamService, ObservableCollection<WorkspaceViewModel> parent):this(notamService)
        {
            parentWorkSpaces = parent;
        }

        public NotamSearchViewModel(NotamService notamService)
        {
            if (notamService == null)
                throw new ArgumentNullException("notamService");
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

        #region NOTAM Search Properties

         private bool _isNof;

        public bool IsNOF
        {
            get { return _isNof; }
            set
            {
                _isNof = value;
                base.OnPropertyChanged("IsNOF");
            }
        }

        private bool _isAd;

        public bool IsAD
        {
            get { return _isAd; }
            set { _isAd = value;
            base.OnPropertyChanged("IsAD");
            }
        }

        private bool _isFir;

        public bool IsFIR
        {
            get { return _isFir; }
            set { _isFir = value;
            base.OnPropertyChanged("IsFIR");
            }
        }


        
        private string _nof;

        public string NOF
        {
            get { return _nof; }
            set {
                _nof = value;
                base.OnPropertyChanged("NOF");
            }
        }


        private bool _isQCode;

        public bool IsQCode
        {
            get { return _isQCode; }
            set { _isQCode = value;
            base.OnPropertyChanged("IsQCode");
            }
        }

        private bool _isQualifier;

        public bool IsQualifier
        {
            get { return _isQualifier; }
            set { _isQualifier = value;
            base.OnPropertyChanged("IsQualifier");
            }
        }

        private string _qCode;
        public string QCode
        {
            get { return _qCode; }
            set { _qCode = value;
            base.OnPropertyChanged("QCode");
            }
        }

        private string _traffic;
        public string Traffic
        {
            get { return _traffic; }
            set { _traffic = value;
            base.OnPropertyChanged("Traffic");
            }
        }

        private string _purpose;
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value;
            base.OnPropertyChanged("Purpose");
            }
        }

        private string _scope;
        public string Scope
        {
            get { return _scope; }
            set { _scope = value;
            base.OnPropertyChanged("Scope");
            }
        }

        private bool _isValid= true;
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value;
            base.OnPropertyChanged("IsValid");
            }
        }

        private bool _isObsolete;
        public bool IsObsolete
        {
            get { return _isObsolete; }
            set { _isObsolete = value;
            base.OnPropertyChanged("IsObsolete");
            }
        }
        private bool _isHold;
        public bool IsHold
        {
            get { return _isHold; }
            set
            {
                _isHold = value;
                base.OnPropertyChanged("IsHold");
            }
        }
        private bool _isAllNotams;
        public bool IsAllNotams
        {
            get { return _isAllNotams; }
            set
            {
                _isAllNotams = value;
                base.OnPropertyChanged("IsAllNotams");
            }
        }

        private string _notamNum;
        public String NotamNum
        {
            get { return _notamNum; }
            set { _notamNum = value;
            base.OnPropertyChanged("NotamNum");
            }
        }

        private string _notamYear;
        public String NotamYear
        {
            get { return _notamYear; }
            set
            {
                _notamYear = value;
                base.OnPropertyChanged("NotamYear");
            }
        }
        private List<string> _TypeOptions;
        public List<string> TypeOptions
        {
            get
            {
                if (_TypeOptions == null)
                {

                    _TypeOptions = new List<string> { "ALL","A", "B" };
                }
                return _TypeOptions;
            }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value;
            base.OnPropertyChanged("Type");
            }
        }

        private bool _isAll;
        public bool IsAll
        {
            get { return _isAll; }
            set { _isAll = value;
            base.OnPropertyChanged("IsAll");
            }
        }

        private bool _isPerm;
        public bool IsPerm
        {
            get { return _isPerm; }
            set { _isPerm = value;
            base.OnPropertyChanged("IsPerm");
            }
        }

        private bool _isEst;
        public bool IsEst
        {
            get { return _isEst; }
            set { _isEst = value;
            base.OnPropertyChanged("IsEst");
            }
        }

        private string _fromDate;
        [RegularExpression(@"^[\d]{10}$", ErrorMessage = "Value should be 10 digits")]
        public String FromDate
        {
            get { return _fromDate; }
            set {
                _fromDate = value;
            base.OnPropertyChanged("FromDate");
            }
        }

        private string _toDate;
        [RegularExpression(@"^[\d]{10}$", ErrorMessage = "Value should be 10 digits")]
        public String ToDate
        {
            get { return _toDate; }
            set { _toDate = value;
            base.OnPropertyChanged("ToDate");
            }
        }

        private string _eFreeText;

        public string EFreeText
        {
            get { return _eFreeText; }
            set { _eFreeText = value;
            base.OnPropertyChanged("EFreeText");
            }
        }

        private string _UserText;
        public string UserText
        {
            get { return _UserText; }
            set
            {
                _UserText = value;
                base.OnPropertyChanged("UserText");
            }
        }
        private string _fromIssueDate;
        public string FromIssueDate
        {
            get { return _fromIssueDate; }
            set
            {
                _fromIssueDate = value;
                ToIssueDate = value;
                base.OnPropertyChanged("FromIssueDate");
                base.OnPropertyChanged("ToIssueDate");
            }
        }
        private string _toIssueDate;
        public string ToIssueDate
        {
            get { return _toIssueDate; }
            set
            {
                _toIssueDate = value;
                base.OnPropertyChanged("ToIssueDate");
            }
        }


        #endregion

        #region Presentation Properties

        public ICommand CloseCommand
        {
            get
            {
                return null;
            }
        }


        public override string DisplayName
        {
            get
            {
                return Entity.NotamSearchViewModel_DisplayName;
            }
        }

        public FIR FIR
        {
            get;
            set;
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
                    this.FIR = _FirOptions.Where(x => x.Code.Equals("OIIX")).FirstOrDefault();
                    _AerodomOptions = null;
                    OnPropertyChanged("AerodomOptions");
                }
                return _FirOptions;
            }
        }
        public Aerodom Aerodom
        {
            get; set;
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
                    if (this.FIR!=null)
                        _AerodomOptions = _notamService.GetAllAerodoms().Where(n=>n.FIR==this.FIR).
                            OrderBy(x => x.Code).ToList() ;
                    else
                        _AerodomOptions = _notamService.GetAllAerodoms();

                }
                return _AerodomOptions;
            }
        }
        bool IsAdmin()
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            return (customPrincipal.Identity.Role == UserRole.Administrator) ;

        }
        public bool IsUserEnabled
        {
            get
            {
                return IsAdmin();
            }
        }
        public bool IsFromIssueEnabled
        {
            get
            {
                return IsAdmin();
            }
        }
        public bool IsToIssueEnabled
        {
            get
            {
                return IsAdmin();
            }
        }

        public NotamCode NotamCode
        {
            get; set;
        }

        public bool ValidateType()
        {
            if (!string.IsNullOrEmpty(FromDate) && FromDate.Length != 10)
            {
                MessageBox.Show("Invalid FromDate");
                return false;
            }
            if (!string.IsNullOrEmpty(ToDate) && ToDate.Length != 10)
            {
                MessageBox.Show("Invalid ToDate");
                return false;
            }
            if (!string.IsNullOrEmpty(QCode) && QCode.Length != 4)
            {
                MessageBox.Show("Invalid QCODE");
                return false;
            }
            if (!string.IsNullOrEmpty(UserText) && UserText.Length != 3)
            {
                MessageBox.Show("Invalid User Initial Name");
                return false;
            }
            if (!string.IsNullOrEmpty(FromIssueDate) && FromIssueDate.Length != 10)
            {
                MessageBox.Show("Invalid From Issue Date");
                return false;
            }
            if (!string.IsNullOrEmpty(ToIssueDate) && ToIssueDate.Length != 10)
            {
                MessageBox.Show("Invalid To Issue Date");
                return false;
            }
            return true;
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
                }
                return _NotamCodeOptions;
            }
        }


       
        /// <summary>
        /// Returns a command that saves the Notam.
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(
                        param => this.Search(),
                        param => true
                        );
                    
                }
                return _searchCommand;
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(
                        param => this.Reset(),
                        param => true
                        );
                }
                return _resetCommand;
            }
        }

       
        #endregion // Presentation Properties

        #region Public Methods

        public void Reset()
        {
            QCode = "";
            IsAD = false;
            IsEst = false;
            IsQCode = false;
            IsValid = true;
            IsHold = false;
            IsPerm = false;
            IsQualifier = false;
            UserText = "";
            Traffic = Purpose = Scope = "";
            NotamNum=NotamYear= FromDate=ToDate= EFreeText = "";
            Type = "ALL";
            Aerodom = null;
            OnPropertyChanged("Aerodom");
            OnPropertyChanged("Type");
        }
        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        
        public void Search()
        {
            if (!ValidateType())
                return;
            var notamFilter = new NotamFilter();
            if(Type!=null )
             notamFilter.TypeFilter = (Type.Equals("A") ||Type.Equals("B"))? Type: string.Empty ;
            notamFilter.NumberFilter = NotamNum ;
            notamFilter.YearFilter = NotamYear;
            notamFilter.NotamCodeFilter =(IsQCode && QCode != null) ? QCode.ToString() : string.Empty;
                                         
            notamFilter.NotamAeroFilter =(IsAD && Aerodom!=null)?Aerodom.Code: string.Empty    ;
            notamFilter.notamStatus = IsValid ? "D" : "A";
            notamFilter.notamStatus = IsHold ? "H" : notamFilter.notamStatus;
            notamFilter.notamStatus = IsAllNotams ? "AD" : notamFilter.notamStatus;
            notamFilter.ItemEFilter = EFreeText;
            notamFilter.NotamFirFilter = FIR!=null?FIR.Code:string.Empty ;
            notamFilter.ScopeFilter = IsQualifier? Scope: string.Empty  ;
            notamFilter.PurposeFilter =IsQualifier ? Purpose: string.Empty ;
            notamFilter.TrafficFilter =IsQualifier ? Traffic: string.Empty  ;
            notamFilter.PermEstFilter = IsPerm ? "PERM" : IsEst ? "EST" : string.Empty;
            notamFilter.FromDateFilter = FromDate;
            notamFilter.ToDateFilter = ToDate;
            notamFilter.UserFilter = UserText;
            notamFilter.SendTimeFilter = FromIssueDate;
            notamFilter.SendTimeEndFilter = ToIssueDate;
            _notamService.Archive();
            _notamService.Reload();

            AllNotamsViewModel workspace =
                parentWorkSpaces.FirstOrDefault(vm => vm is AllNotamsViewModel)
                as AllNotamsViewModel;
            //if (workspace == null)
            //{
                workspace = new AllNotamsViewModel(_notamService, parentWorkSpaces, notamFilter );
                string strTitle = Entity.AllNotamsViewModel_DisplayName;
                if (notamFilter.notamStatus == "A")
                    strTitle = Entity.MainWindowViewModel_Command_ViewAllArchiveNotams;
                if (notamFilter.notamStatus == "H")
                    strTitle = Entity.MainWindowViewModel_Command_ViewAllHoldNotams;
                if (notamFilter.notamStatus == "AD")
                    strTitle = Entity.AllArchAndValidNotamsViewModel_DisplayName;
                foreach (var obj in this.parentWorkSpaces)
                {
                    if (obj.DisplayName == strTitle)
                    {
                        parentWorkSpaces.Remove(obj);
                        break;
                    }

                }
                this.parentWorkSpaces.Add(workspace);
           // }
            this.SetActiveWorkspace(workspace);
            
        }

        

        #endregion // Public Methods

        #region Private Helpers

       
        #endregion // Private Helpers

        
        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<NotamSearchViewModel, object>> propertyGetters;
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

        private Func<NotamSearchViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<NotamSearchViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion
        #endregion // IDataErrorInfo Members

        private int validationExceptionCount;

        public void ValidationExceptionsChanged(int count)
        {
            this.validationExceptionCount = count;
            this.OnPropertyChanged("ValidPropertiesCount");
        }
    }
}
