using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class CountryViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Country _country;
        readonly CountryService _countryService;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

        public CountryViewModel(Country country, CountryService countryService)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            if (countryService == null)
                throw new ArgumentNullException("countryService");

            _country = country;
            _countryService = countryService;
            // _customerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
        }

        #endregion // Constructor

        #region Countrys Properties
        public string Code
        {
            get { return _country.Code; }
            set
            {
                if (value == _country.Code)
                    return;

                _country.Code = value;

                base.OnPropertyChanged("Code");
            }
        }
        public string Name
        {
            get { return _country.Name; }
            set
            {
                if (value == _country.Name)
                    return;

                _country.Name = value;

                base.OnPropertyChanged("Name");
            }
        }

        #endregion // FIR Properties

        #region Presentation Properties


        public override string DisplayName
        {
            get
            {
                if (this.IsNewCountry)
                {
                    return Entity.CountryViewModel_DisplayName;
                }

                else
                {
                    return _country.Name;
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
            if (!_country.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewCountry)
                _countryService.Insert(_country);
            else
                _countryService.Update(_country);

            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewCountry
        {
            get { return !_countryService.ContainsCountry(_country); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateCustomerType()) && _country.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_country as IDataErrorInfo).Error; }
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
                    error = (_country as IDataErrorInfo)[propertyName];
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

    }
}
