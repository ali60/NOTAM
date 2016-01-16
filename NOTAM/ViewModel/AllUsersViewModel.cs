using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NOTAM.Properties;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class AllUsersViewModel: WorkspaceViewModel
    {

         #region Fields

        readonly AuthenticationService _authService;

        #endregion // Fields

        #region Constructor

        public AllUsersViewModel(AuthenticationService authenticationService)
        {
            if (authenticationService == null)
                throw new ArgumentNullException("authenticationService");

            base.DisplayName = Entity.AllUsersViewModel_DisplayName;

            _authService = authenticationService;

            // Subscribe for notifications of when a new customer is saved.
            _authService.UserAdded += this.OnUserAddedToRepository;
            _authService.UserDeleted += this.OnUserDeletedToRepository;
            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllUsers();              
        }

        void CreateAllUsers()
        {
            List<UserViewModel> all =
                (from cust in _authService.GetUsers()
                 select new UserViewModel(cust, _authService)).ToList();

            foreach (UserViewModel cvm in all)
                cvm.PropertyChanged += this.OnUserViewModelPropertyChanged;

            this.AllUsers = new ObservableCollection<UserViewModel>(all);
            this.AllUsers.CollectionChanged += this.OnCollectionChanged;
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<UserViewModel> AllUsers { get; private set; }

        /// <summary>
        /// Returns the total sales sum of all selected customers.
        /// </summary>
        //public double TotalSelectedSales
        //{
        //    get
        //    {
        //        return this.AllUsers.Sum(
        //            orVM => orVM.IsSelected ? custVM.TotalSales : 0.0);
        //    }
        //}

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (UserViewModel orVM in this.AllUsers)
                orVM.Dispose();

            this.AllUsers.Clear();
            this.AllUsers.CollectionChanged -= this.OnCollectionChanged;

            _authService.UserAdded -= this.OnUserAddedToRepository;
            _authService.UserDeleted -= this.OnUserDeletedToRepository;
        }

        void OnUserDeletedToRepository(object sender, EntityAddedEventArgs<User> e)
        {
            var curUser = this.AllUsers.Where(u => u.Username == e.NewEntity.Username).First();
            if (curUser != null)
                this.AllUsers.Remove(curUser);
        }
        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (UserViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnUserViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (UserViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnUserViewModelPropertyChanged;
        }

        void OnUserViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as UserViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnUserAddedToRepository(object sender, EntityAddedEventArgs<User> e)
        {
            var viewModel = new UserViewModel(e.NewEntity, _authService);
            this.AllUsers.Add(viewModel);
        }

        #endregion // Event Handling Methods
    }
}
