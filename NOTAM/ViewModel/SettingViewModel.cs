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
    public class SettingViewModel : WorkspaceViewModel, IDataErrorInfo, IValidationExceptionHandler
    {

         #region Fields

       RelayCommand _saveCommand;
       string _strSettingFile;

        #endregion // Fields

        #region Constructor

       public SettingViewModel( )
        {
            _strSettingFile = "C:\\AISAdmin\\settiing.txt";
            if (!System.IO.File.Exists(_strSettingFile))
            {
                _printCount = "3";
                _IsLaser = true;
            }
            else
            {
                string strAllFile = System.IO.File.ReadAllText(_strSettingFile);
                if (!string.IsNullOrEmpty(strAllFile) && strAllFile.IndexOf(',') >= 0)
                {
                    _printCount = strAllFile.Split(',')[0];
                    if (strAllFile.Split(',')[1] == "TRUE")
                        _IsLaser = true;
                    else
                        _IsLaser = false;


                }

            }

        }

        #endregion // Constructor

        private string _printCount ;      
        public string PrintCount
       {
           get { return _printCount; }
            
            set
            {
                _printCount = value;
            }
       }

        private Boolean _IsLaser;
       public Boolean IsLaser
        {
            get
            {
                return _IsLaser;
            }

           set
            {
             _IsLaser=value;
             base.OnPropertyChanged("IsLaser");
            }
        }
        public bool bCanSave
        {
            get
            {
                if(!string.IsNullOrEmpty(_printCount))
                    return  Int32.Parse(_printCount)<6;
                return true;
            }
        }
        public override string DisplayName
        {
            get
            {
                    return Entity.UserViewModel_SettingName;
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
                        param => bCanSave
                        );
                }
                return _saveCommand;
            }
        }

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save(object parameter)
        {
            string strWrite = PrintCount+",";
            if(_IsLaser)
                strWrite += "TRUE";
            else
                strWrite += "FALSE";
            System.IO.File.WriteAllText(_strSettingFile,strWrite);

            MessageBox.Show("New Printer Setting Saved Successfully");
        }
        public static bool IfLaser()
        {
            string sfile = "C:\\AISAdmin\\settiing.txt";
            if (!System.IO.File.Exists(sfile))
            {
                return false;
            }
            else
            {
                string strAllFile = System.IO.File.ReadAllText(sfile);
                if (!string.IsNullOrEmpty(strAllFile) && strAllFile.IndexOf(',') >= 0)
                {
                    if (strAllFile.Split(',')[1] == "TRUE")
                        return true;


                }

            }
            return false;

        }

        public static int GetPrintCount()
        {
            string sfile = "C:\\AISAdmin\\settiing.txt";
            if (!System.IO.File.Exists(sfile))
            {
                return 3;
            }
            else
            {
                string strAllFile = System.IO.File.ReadAllText(sfile);
                if (!string.IsNullOrEmpty(strAllFile) && strAllFile.IndexOf(',') >= 0)
                {
                    return Int32.Parse(strAllFile.Split(',')[0]);


                }

            }
            return 3;

        }
        
        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>

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
//                 if (this.propertyGetters.ContainsKey(propertyName))
//                 {
//                     var propertyValue = this.propertyGetters[propertyName](this);
//                     var errorMessages = this.validators[propertyName]
//                         .Where(v => !v.IsValid(propertyValue))
//                         .Select(v => v.ErrorMessage).ToArray();
// 
//                     return string.Join(Environment.NewLine, errorMessages);
//                 }

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
//                 var errors = from validator in this.validators
//                              from attribute in validator.Value
//                              where !attribute.IsValid(this.propertyGetters[validator.Key](this))
//                              select attribute.ErrorMessage;
// 


                
                //                 return string.Join(Environment.NewLine, errors.ToArray());

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the number of properties which have a validation attribute and are currently valid
        /// </summary>
        public int ValidPropertiesCount
        {
            get
            {
//                 var query = from validator in this.validators
//                             where validator.Value.All(attribute => attribute.IsValid(this.propertyGetters[validator.Key](this)))
//                             select validator;
// 
//                 var count = query.Count(); //- this.validationExceptionCount;
//                 return count;
                return 0;
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
            return "";

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
