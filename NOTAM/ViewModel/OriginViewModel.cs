using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class OriginViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Origin _origin;
        readonly OriginService _originService;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

         public OriginViewModel(Origin origin, OriginService originService, ObservableCollection<WorkspaceViewModel> parent):this(origin,originService)
        {
            parentWorkSpaces = parent;
        }

        public OriginViewModel(Origin origin, OriginService originService)
        {
            if (origin == null)
                throw new ArgumentNullException("origin");

            if (originService == null)
                throw new ArgumentNullException("originService");

            _origin = origin;
            _originService = originService;
           // _customerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
        }

        #endregion // Constructor

        #region Origin Properties
        public string Code
        {
            get { return _origin.Code; }
            set
            {
                if (value == _origin.Code)
                    return;

                _origin.Code = value;

                base.OnPropertyChanged("Code");
            }
        }
        public string Name
        {
            get { return _origin.Name; }
            set
            {
                if (value == _origin.Name)
                    return;

                _origin.Name = value;

                base.OnPropertyChanged("Name");
            }
        }


       public string Serial
        {
            get { return _origin.Serial; }
            set
            {
                if (value == _origin.Serial)
                    return;

                _origin.Serial = value;

                base.OnPropertyChanged("Serial");
            }
        }

        public string CenterType
        {
            get { return _origin.CenterType; }
            set
            {
                if (value == _origin.CenterType)
                    return;

                _origin.CenterType = value;

                base.OnPropertyChanged("CenterType");
            }
        }
        
        public string DeskType
        {
            get { return _origin.DeskType; }
            set
            {
                if (value == _origin.DeskType)
                    return;

                _origin.DeskType = value;

                base.OnPropertyChanged("DeskType");
            }
        }

        
        #endregion // Customer Properties

        #region Presentation Properties

       
        public override string DisplayName
        {
            get
            {
                if (this.IsNewOrigin)
                {
                    return Entity.OriginViewModel_DisplayName;
                }
                
                else
                {
                    return _origin.Code; 
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
            if (!_origin.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewOrigin)
                _originService.Insert(_origin );
            else
                _originService.Update(_origin); 
            
            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewOrigin
        {
            get { return !_originService.ContainsOrigin(_origin); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateCustomerType()) && _origin.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_origin as IDataErrorInfo).Error; }
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
                    error = (_origin as IDataErrorInfo)[propertyName];
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

            parentWorkSpaces.Add(parameter as OriginViewModel );
            this.SetActiveWorkspace(parameter as OriginViewModel);

        }
        #endregion
    }
}
