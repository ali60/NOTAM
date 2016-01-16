using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using NOTAM.Behavior;
using NOTAM.Properties;
using NOTAM.SERVICE;
using System.Windows;

namespace NOTAM.ViewModel
{
    public class UserViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {

         #region Fields

        readonly User _user;
        readonly AuthenticationService _userService;
        bool _isSelected;
       string _passwordError;
       RelayCommand _saveCommand;
       RelayCommand _deleteCommand;

        #endregion // Fields

        #region Constructor

        public UserViewModel(User user, AuthenticationService userService)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (userService == null)
                throw new ArgumentNullException("userService");

            _user = user;
            _userService = userService;

            this.validators = this.GetType()
               .GetProperties()
               .Where(p => this.GetValidations(p).Length != 0)
               .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));

//            IsAdmin = false;
        }

        #endregion // Constructor

        #region User Properties
        [Required(ErrorMessage = "username is required")]
        public string Username
        {
            get { return _user.Username ; }
            set
            {
                if (value == _user.Username)
                    return;

                _user.Username = value;
                base.OnPropertyChanged("Username");
            }
        }

        //[Required (ErrorMessage = "Password is required")]
        public string Password
        {
            get { return _user.Password; }
            set
            {
                if (value == _user.Password)
                    return;

                _user.Password  = value;

                base.OnPropertyChanged("Password");
            }
        }


       public Boolean IsAdmin
        {
            get
            {
                if (_user.Role.Equals(UserRole.Administrator))
                    return true;
                return false;
            }

           set
            {
             if(value.Equals(true ) )  
                _user.Role  = UserRole.Administrator ;
             else
                 _user.Role = UserRole.User;

             base.OnPropertyChanged("IsAdmin");
            }
        }

      
        
        
        #endregion // User Properties

        #region Presentation Properties

       
        public override string DisplayName
        {
            get
            {
                if (this.IsNewUser)
                {
                    return Entity.UserViewModel_DisplayName;
                }
                
                else
                {
                    return _user.Username ; 
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


        public string PasswordError
        {
            get { return _passwordError; }
            set
            {
                if (value == _passwordError)
                    return;

                _passwordError = value;

                base.OnPropertyChanged("PasswordError");
            }
        }

        public string PasswordValidator
        {
            get { return _passwordError; }
            set
            {
                if (value == _passwordError)
                    return;

                _passwordError = value;

                base.OnPropertyChanged("PasswordValidator");
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
                        Save,
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.Delete(),
                        param => !this.IsNewUser
                        );
                }
                return _deleteCommand;
            }
        }
        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            Password  = passwordBox.Password;
            if (String.IsNullOrEmpty(Password))
            {
                PasswordError = "Password is required";
                return;
            }

            if (!_user.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);

            if (this.IsNewUser)
                _userService.Insert(_user );
            else
                _userService.Update(_user);
            #region Reset
//             PasswordError = String.Empty;
//             Username = string.Empty; //reset
//             passwordBox.Password = string.Empty; //reset
            #endregion
            MessageBox.Show("New User Saved Successfully");
            base.OnPropertyChanged("DisplayName");
        }
        public void Delete()
        {

            if (!this.IsNewUser)
                _userService.Delete(_user);
            MessageBox.Show("User Deleted Successfully");

        }
        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewUser
        {
            get { return !_userService.ContainsUser(_user); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        
        bool CanSave
        {
            get
            {
                return String.IsNullOrEmpty(this.ValidateType()) && _user.IsValid && string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount;
            }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<UserViewModel, object>> propertyGetters;
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

        private Func<UserViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<UserViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion



        string ValidateType()
        {
            return _passwordError + PasswordValidator;

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
