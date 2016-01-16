using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class AllAerodomsViewModel : WorkspaceViewModel
    {

        #region Fields

        private AerodomService _aerodomService;

        #endregion // Fields

        #region Constructor

        public AllAerodomsViewModel(AerodomService aerodomService, ObservableCollection<WorkspaceViewModel> parent)
      {
            parentWorkSpaces = parent;
            Setup(aerodomService);
      }

       
        public AllAerodomsViewModel(AerodomService aerodomService)
        {
            Setup(aerodomService);
        }

        void CreateAllAerodoms()
        {
            List<AerodomViewModel> all =
                (from cust in _aerodomService.GetAerodoms()
                 select new AerodomViewModel(cust, _aerodomService,parentWorkSpaces)).ToList();

            foreach (AerodomViewModel cvm in all)
                cvm.PropertyChanged += this.OnAerodomViewModelPropertyChanged;

            this.AllAerodoms = new ObservableCollection<AerodomViewModel>(all);
            this.AllAerodoms.CollectionChanged += this.OnCollectionChanged;
        }

        private void Setup(AerodomService aerodomService)
        {
            if (aerodomService == null)
                throw new ArgumentNullException("aerodomService");

            base.DisplayName = Entity.AllAerodomsViewModel_DisplayName;

            _aerodomService = aerodomService;

            // Subscribe for notifications of when a new customer is saved.
            _aerodomService.AerodomAdded += this.OnAerodomAddedToRepository;

            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllAerodoms();
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<AerodomViewModel> AllAerodoms { get; private set; }

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (AerodomViewModel orVM in this.AllAerodoms)
                orVM.Dispose();

            this.AllAerodoms.Clear();
            this.AllAerodoms.CollectionChanged -= this.OnCollectionChanged;

            _aerodomService.AerodomAdded -= this.OnAerodomAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (AerodomViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnAerodomViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (AerodomViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnAerodomViewModelPropertyChanged;
        }

        void OnAerodomViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as AerodomViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnAerodomAddedToRepository(object sender, EntityAddedEventArgs<Aerodom> e)
        {
            var viewModel = new AerodomViewModel(e.NewEntity, _aerodomService);
            this.AllAerodoms.Add(viewModel);
        }

        #endregion // Event Handling Methods

    }
}
