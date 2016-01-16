using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class NotamCodeViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly NotamCode _notamcode;
        readonly NotamCodeService _notamcodeService;
        bool _isSelected;
        RelayCommand _saveCommand;
        RelayCommand _deleteCommand;

        #endregion // Fields

        #region Constructor

        public NotamCodeViewModel(NotamCode notamcode, NotamCodeService notamcodeService, ObservableCollection<WorkspaceViewModel> parent)
            : this(notamcode, notamcodeService)
        {
            parentWorkSpaces = parent;
        }

        public NotamCodeViewModel(NotamCode notamcode, NotamCodeService notamcodeService)
        {
            if (notamcode == null)
                throw new ArgumentNullException("notamcode");

            if (notamcodeService == null)
                throw new ArgumentNullException("notamcodeService");

            _notamcode = notamcode;
            _notamcodeService = notamcodeService;
           
        }

        #endregion // Constructor

        #region NotamCodes Properties
        public string Subject
        {
            get { return _notamcode.Subject; }
            set
            {
                if (value == _notamcode.Subject)
                    return;

                _notamcode.Subject = value;

                base.OnPropertyChanged("Subject");
            }
        }
        public string Subject_Desc
        {
            get { return _notamcode.Subject_Desc; }
            set
            {
                if (value == _notamcode.Subject_Desc)
                    return;

                _notamcode.Subject_Desc = value;

                base.OnPropertyChanged("Subject_Desc");
            }
        }
        public string Condition
        {
            get { return _notamcode.Condition; }
            set
            {
                if (value == _notamcode.Condition)
                    return;

                _notamcode.Condition = value;

                base.OnPropertyChanged("Condition");
            }
        }
        public string Condition_Desc
        {
            get { return _notamcode.Condition_Desc; }
            set
            {
                if (value == _notamcode.Condition_Desc)
                    return;

                _notamcode.Condition_Desc = value;

                base.OnPropertyChanged("Condition_Desc");
            }
        }
        public string Scope
        {
            get { return _notamcode.Scope; }
            set
            {
                if (value == _notamcode.Scope)
                    return;

                _notamcode.Scope = value;

                base.OnPropertyChanged("Scope");
            }
        }
        public string Category
        {
            get { return _notamcode.Category; }
            set
            {
                if (value == _notamcode.Category)
                    return;

                _notamcode.Category = value;

                base.OnPropertyChanged("Category");
            }
        }

        public string Traffic
        {
            get { return _notamcode.Traffic; }
            set
            {
                if (value == _notamcode.Traffic)
                    return;

                _notamcode.Traffic = value;

                base.OnPropertyChanged("Traffic");
            }
        }
        public string Purpose
        {
            get { return _notamcode.Purpose; }
            set
            {
                if (value == _notamcode.Purpose)
                    return;

                _notamcode.Purpose = value;

                base.OnPropertyChanged("Purpose");
            }
        }



        #endregion // FIR Properties

        #region Presentation Properties


        public override string DisplayName
        {
            get
            {
                if (this.IsNewNotamCode)
                {
                    return Entity.NotamCodeViewModel_DisplayName;
                }

                else
                {
                    return _notamcode.Subject + _notamcode.Condition;
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


        public void Delete()
        {

            if (!this.IsNewNotamCode)
                _notamcodeService.Delete(_notamcode);
            MessageBox.Show("NOTAM Code Deleted Successfully");

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


        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.Delete(),
                        param => this.CanSave
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
        public void Save()
        {
            if (!_notamcode.IsValid)
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);
            if (!_notamcodeService.IsNew(_notamcode))
            {
                if (MessageBox.Show("NOTAM Code with Subject=" + _notamcode.Subject + " and Condition=" + _notamcode.Condition + " Already Exists\nDo you want to update?", "Update", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (this.IsNewNotamCode)
                _notamcodeService.Insert(_notamcode);
            else
                _notamcodeService.Update(_notamcode);

            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewNotamCode
        {
            get { return !_notamcodeService.ContainsNotamCode(_notamcode); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return !String.IsNullOrEmpty(Subject) && !String.IsNullOrEmpty(Condition)  && _notamcode.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_notamcode as IDataErrorInfo).Error; }
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
                    error = (_notamcode as IDataErrorInfo)[propertyName];
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

            parentWorkSpaces.Add(parameter as NotamCodeViewModel);
            this.SetActiveWorkspace(parameter as NotamCodeViewModel);

        }
        #endregion
    
    }
}
