using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;

namespace NOTAM.ViewModel
{
    public class AerodomViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Aerodom _aerodom;
        readonly AerodomService _aerodomService;
        bool _isSelected;
        RelayCommand _saveCommand;
        RelayCommand _localizeCommand;

        #endregion // Fields

        #region Constructor

        public AerodomViewModel(Aerodom aerodom, AerodomService aerodomService, ObservableCollection<WorkspaceViewModel> parent)
            : this(aerodom, aerodomService)
        {
            parentWorkSpaces = parent;
        }

        public AerodomViewModel(Aerodom aerodom, AerodomService aerodomService)
        {
            if (aerodom == null)
                throw new ArgumentNullException("aerodom");

            if (aerodomService == null)
                throw new ArgumentNullException("aerodomService");

            _aerodom = aerodom;
            _aerodomService = aerodomService;
        }

        #endregion // Constructor

        #region Origin Properties
        public string Code
        {
            get { return _aerodom.Code; }
            set
            {
                if (value == _aerodom.Code)
                    return;

                _aerodom.Code = value;

                base.OnPropertyChanged("Code");
            }
        }

        public string Name
        {
            get { return _aerodom.Name; }
            set
            {
                if (value == _aerodom.Name)
                    return;

                _aerodom.Name = value;

                base.OnPropertyChanged("Name");
            }
        }



        public string Latitude
        {
            get { return _aerodom.Latitude; }
            set
            {
                if (value == _aerodom.Latitude)
                    return;

                _aerodom.Latitude = value;
                base.OnPropertyChanged("Latitude");
            }
        }
        public string LongTitude
        {
            get { return _aerodom.Longtitude; }
            set
            {
                if (value == _aerodom.Longtitude)
                    return;

                _aerodom.Longtitude = value;
                base.OnPropertyChanged("LongTitude");
            }
        }

        public string Direction
        {
            get { return _aerodom.Direction; }
            set
            {
                if (value == _aerodom.Direction)
                    return;

                _aerodom.Direction = value;
                base.OnPropertyChanged("Direction");
            }
        }


        public string VOR
        {
            get { return _aerodom.VOR; }
            set
            {
                if (value == _aerodom.VOR)
                    return;

                _aerodom.VOR = value;
                base.OnPropertyChanged("VOR");
            }
        }

        public string I_L
        {
            get { return _aerodom.I_L; }
            set
            {
                if (value == _aerodom.I_L)
                    return;

                _aerodom.I_L = value;
                base.OnPropertyChanged("I_L");
            }
        }


        #endregion // Aerodom Properties

        #region Presentation Properties


        public override string DisplayName
        {
            get
            {
                if (this.IsNewAerodom)
                {
                    return Entity.AerodomViewModel_DisplayName;
                }

                else
                {
                    return _aerodom.Name;
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



        public FIR FIR
        {
            get { return _aerodom.FIR; }
            set
            {
                if (value == _aerodom.FIR || value == null)
                    return;

                _aerodom.FIR = value;

                base.OnPropertyChanged("FIR");
            }
        }

        private List<FIR> _FirOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<FIR> FIROptions
        {
            get
            {
                if (_FirOptions == null)
                {

                    _FirOptions = _aerodomService.GetAllFIRs();
                }
                return _FirOptions;
            }
        }


        public Country Country
        {
            get { return _aerodom.Country; }
            set
            {
                if (value == _aerodom.Country || value == null)
                    return;

                _aerodom.Country = value;

                base.OnPropertyChanged("Country");
            }
        }

        private List<Country> _CountryOptions;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<Country> CountryOptions
        {
            get
            {
                if (_CountryOptions == null)
                {

                    _CountryOptions = _aerodomService.GetAllCountries();
                }
                return _CountryOptions;
            }
        }

        


        /// <summary>
        /// Returns a command that saves the Aerodom.
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
            if (!_aerodom.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewAerodom)
                _aerodomService.Insert(_aerodom);
            else
                _aerodomService.Update(_aerodom);

            base.OnPropertyChanged("DisplayName");
        }


        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewAerodom
        {
            get { return !_aerodomService.ContainsAerodom(_aerodom); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateCustomerType()) && _aerodom.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_aerodom as IDataErrorInfo).Error; }
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
                    error = (_aerodom as IDataErrorInfo)[propertyName];
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
            parentWorkSpaces.Add(parameter as AerodomViewModel);
            this.SetActiveWorkspace(parameter as AerodomViewModel);
        }
        #endregion
    
    }
}
