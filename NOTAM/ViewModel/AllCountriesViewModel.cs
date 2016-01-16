using System;using System.Collections.Generic;using System.Collections.ObjectModel;using System.Collections.Specialized;using System.ComponentModel;using System.Linq;using NOTAM.Properties;using NOTAM.Service;using NOTAM.SERVICE;namespace NOTAM.ViewModel{    public class AllCountriesViewModel : WorkspaceViewModel    {        #region Fields        readonly CountryService _countryService;        #endregion // Fields        #region Constructor        public AllCountriesViewModel(CountryService countryService)        {            if (countryService == null)                throw new ArgumentNullException("countryService");            base.DisplayName = Entity.AllCountriesViewModel_DisplayName;            _countryService = countryService;            // Subscribe for notifications of when a new customer is saved.            _countryService.CountryAdded += this.OnCountryAddedToRepository;            // Populate the AllCustomers collection with CustomerViewModels.            this.CreateAllCountries();        }        void CreateAllCountries()        {            List<CountryViewModel> all =                (from cust in _countryService.GetCountries()                 select new CountryViewModel(cust, _countryService)).ToList();            foreach (CountryViewModel cvm in all)                cvm.PropertyChanged += this.OnCountryViewModelPropertyChanged;            this.AllCountries = new ObservableCollection<CountryViewModel>(all);            this.AllCountries.CollectionChanged += this.OnCollectionChanged;        }        #endregion // Constructor        #region Public Interface        /// <summary>        /// Returns a collection of all the CustomerViewModel objects.        /// </summary>        public ObservableCollection<CountryViewModel> AllCountries { get; private set; }        #endregion // Public Interface        #region  Base Class Overrides        protected override void OnDispose()        {            foreach (CountryViewModel orVM in this.AllCountries)                orVM.Dispose();            this.AllCountries.Clear();            this.AllCountries.CollectionChanged -= this.OnCollectionChanged;            _countryService.CountryAdded -= this.OnCountryAddedToRepository;        }        #endregion // Base Class Overrides        #region Event Handling Methods        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)        {            if (e.NewItems != null && e.NewItems.Count != 0)                foreach (CountryViewModel orVM in e.NewItems)                    orVM.PropertyChanged += this.OnCountryViewModelPropertyChanged;            if (e.OldItems != null && e.OldItems.Count != 0)                foreach (CountryViewModel orVM in e.OldItems)                    orVM.PropertyChanged -= this.OnCountryViewModelPropertyChanged;        }        void OnCountryViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)        {            string IsSelected = "IsSelected";            // Make sure that the property name we're referencing is valid.            // This is a debugging technique, and does not execute in a Release build.            (sender as CountryViewModel).VerifyPropertyName(IsSelected);            // When a customer is selected or unselected, we must let the            // world know that the TotalSelectedSales property has changed,            // so that it will be queried again for a new value.            //if (e.PropertyName == IsSelected)            //    this.OnPropertyChanged("TotalSelectedSales");        }        void OnCountryAddedToRepository(object sender, EntityAddedEventArgs<Country> e)        {            var viewModel = new CountryViewModel(e.NewEntity, _countryService);            this.AllCountries.Add(viewModel);        }        #endregion // Event Handling Methods    }}