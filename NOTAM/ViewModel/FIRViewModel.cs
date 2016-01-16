using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class FIRViewModel : WorkspaceViewModel, IDataErrorInfo
    {
         #region Fields

        readonly FIR _fir;
        readonly FIRService _firService;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

        public FIRViewModel(FIR fir, FIRService firService, ObservableCollection<WorkspaceViewModel> parent)
            : this(fir, firService)
        {
            parentWorkSpaces = parent;
        }

        public FIRViewModel(FIR fir, FIRService firService)
        {
            if (fir == null)
                throw new ArgumentNullException("fir");

            if (firService == null)
                throw new ArgumentNullException("firService");

            _fir = fir;
            _firService = firService;
           // _customerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
        }

        #endregion // Constructor

        #region Origin Properties
        public string Code
        {
            get { return _fir.Code; }
            set
            {
                if (value == _fir.Code)
                    return;

                _fir.Code = value;

                base.OnPropertyChanged("Code");
            }
        }
        public string Name
        {
            get { return _fir.Name; }
            set
            {
                if (value == _fir.Name)
                    return;

                _fir.Name = value;

                base.OnPropertyChanged("Name");
            }
        }


       public string AtcCode
        {
            get { return _fir.AtcCode; }
            set
            {
                if (value == _fir.AtcCode)
                    return;

                _fir.AtcCode = value;

                base.OnPropertyChanged("AtcCode");
            }
        }

       public string NorthLimit
        {
            get { return _fir.NorthLimit; }
            set
            {
                if (value == _fir.NorthLimit)
                    return;

                _fir.NorthLimit = value;

                base.OnPropertyChanged("NorthLimit");
            }
        }

       public string SouthLimit
        {
            get { return _fir.SouthLimit ; }
            set
            {
                if (value == _fir.SouthLimit)
                    return;

                _fir.SouthLimit = value;

                base.OnPropertyChanged("SouthLimit");
            }
        }

       public string WestLimit
       {
           get { return _fir.WestLimit ; }
           set
           {
               if (value == _fir.WestLimit)
                   return;

               _fir.WestLimit = value;

               base.OnPropertyChanged("WestLimit");
           }
       }

       public string EastLimit
       {
           get { return _fir.EastLimit; }
           set
           {
               if (value == _fir.EastLimit)
                   return;

               _fir.EastLimit = value;

               base.OnPropertyChanged("EastLimit");
           }
       }

        
        
        #endregion // FIR Properties

        #region Presentation Properties

       
        public override string DisplayName
        {
            get
            {
                if (this.IsNewFIR)
                {
                    return Entity.FIRViewModel_DisplayName;
                }
                
                else
                {
                    return _fir.Code; 
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

        
        public Origin Origin
        {
            get { return _fir.Origin; }
            set
            {
                if (value == _fir.Origin || value == null)
                    return;

                _fir.Origin = value; 

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

                    _originOptions = _firService.GetAllOrigins(); 
                }
                return _originOptions;
            }
        }

        /// <summary>
        /// Returns a command that saves the customer.
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

        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            if (!_fir.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewFIR)
                _firService.Insert(_fir );
            else
                _firService.Update(_fir); 
            
            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewFIR
        {
            get { return !_firService.ContainsFIR(_fir); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateCustomerType()) && _fir.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_fir as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                if (propertyName == "CustomerType")
                {
                    // The IsCompany property of the Customer class 
                    // is Boolean, so it has no concept of being in
                    // an "unselected" state.  The CustomerViewModel
                    // class handles this mapping and validation.
                    error = this.ValidateCustomerType();
                }
                else
                {
                    error = (_fir as IDataErrorInfo)[propertyName];
                }

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateCustomerType()
        {
            return null;
            //if (this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
            //   this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
            //    return null;

            //return Strings.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion // IDataErrorInfo Members


        #region WorkspaceViewModel

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        protected override void DoubleClick(object parameter)
        {

            parentWorkSpaces.Add(parameter as FIRViewModel );
            this.SetActiveWorkspace(parameter as FIRViewModel);

        }
        #endregion
    
    }
}
