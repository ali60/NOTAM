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
    public class AftnViewModel : WorkspaceViewModel, IDataErrorInfo
    {
         #region Fields

        readonly Aftn _aftn;
        readonly AftnService _aftnService;
        bool _isSelected;
        RelayCommand _saveCommand;
        RelayCommand _addCommand;

        #endregion // Fields

        #region Constructor

         public AftnViewModel(Aftn aftn, AftnService aftnService, ObservableCollection<WorkspaceViewModel> parent):this(aftn,aftnService)
        {
            parentWorkSpaces = parent;
        }

         public AftnViewModel(Aftn aftn, AftnService aftnService)
        {
            if (aftn == null)
                throw new ArgumentNullException("aftn");

            if (aftnService == null)
                throw new ArgumentNullException("aftnService");

            _aftn = aftn;
            _aftnService = aftnService;
             AftnList = AftnList2;
           // _customerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
        }

        #endregion // Constructor

        #region Aftn Properties
         public string Series
        {
            get { return _aftn.Series; }
            set
            {
                if (value == _aftn.Series)
                    return;

                _aftn.Series = value;
                base.OnPropertyChanged("Series");
            }
        }

        private ObservableCollection<string> _aftnList;

        public ObservableCollection<string> AftnList
        {
            get { return _aftnList; }
            set { 
                _aftnList = value;
                base.OnPropertyChanged("AftnList");
            }
        }

        public ObservableCollection<string> AftnList2
        {
            get
            {
                if (_aftn.AftnList != null)
                {
                    if (_aftn.AftnList.Contains(","))
                        return new ObservableCollection<string>(_aftn.AftnList.Split(','));
                    else if (!string.IsNullOrEmpty(_aftn.AftnList))
                        return new ObservableCollection<string>(new List<string>() {_aftn.AftnList});
                    else
                        return new ObservableCollection<string>();
                }
                else
                {
                    return new ObservableCollection<string>();
                }
            }
            set
            {
                var aftns = String.Join(",", value);
                if (aftns == _aftn.AftnList)
                    return;

                _aftn.AftnList = aftns;
                base.OnPropertyChanged("AftnList");
            }
        }


       
        #endregion // AftnViewModel Properties

        #region Presentation Properties

       
        public override string DisplayName
        {
            get
            {
                if (this.IsNewAftn)
                {
                    return Entity.AftnViewModel_DisplayName;
                }
                
                else
                {
                    return _aftn.Series; 
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

        public String NewAftn
        { get; set; }

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

        /// <summary>
        /// Returns a command that Adds the Aftn
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        param => this.AddAftn(),
                         param => true
                        );
                }
                return _addCommand;
            }
        }

       

        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {

            AftnList2 = AftnList;
            if (!_aftn.IsValid)
                throw new InvalidOperationException(Entity.AftnViewModel_Exception_CannotSave);

            if (this.IsNewAftn)
                _aftnService.Insert(_aftn);
            else
                _aftnService.Update(_aftn); 
            
            base.OnPropertyChanged("DisplayName");
        }

        private void AddAftn()
        {
            if(!string.IsNullOrEmpty(NewAftn)&& !AftnList.Contains(NewAftn) )
            {
                AftnList.Add(NewAftn);
                base.OnPropertyChanged("AftnList");
            } 
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewAftn
        {
            get { return !_aftnService.ContainsAftn(_aftn); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateAftnViewModel()) && _aftn.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_aftn as IDataErrorInfo).Error; }
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
                    error = this.ValidateAftnViewModel();
                }
                else
                {
                    error = (_aftn as IDataErrorInfo)[propertyName];
                }

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateAftnViewModel()
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

            parentWorkSpaces.Add(parameter as AftnViewModel);
            this.SetActiveWorkspace(parameter as AftnViewModel);

        }
        #endregion
   
    }
}
