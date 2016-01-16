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
    public class AllOriginsViewModel:WorkspaceViewModel
    {

        
        #region Fields

        private OriginService _originService;

        #endregion // Fields

        #region Constructor

        public AllOriginsViewModel(OriginService originService, ObservableCollection<WorkspaceViewModel> parent)
      {
            parentWorkSpaces = parent;
            Setup(originService);
      }

       

        public AllOriginsViewModel(OriginService originService)
        {
            Setup(originService); 
        }

        void CreateAllOrigins()
        {
            List<OriginViewModel> all =
                (from cust in _originService.GetOrigins()
                 select new OriginViewModel(cust, _originService,parentWorkSpaces)).ToList();

            foreach (OriginViewModel cvm in all)
                cvm.PropertyChanged += this.OnOriginViewModelPropertyChanged;

            this.AllOrigins = new ObservableCollection<OriginViewModel>(all);
            this.AllOrigins.CollectionChanged += this.OnCollectionChanged;
        }

        private void Setup(OriginService originService)
        {
            if (originService == null)
                throw new ArgumentNullException("originService");

            base.DisplayName = Entity.AllOriginsViewModel_DisplayName;

            _originService = originService;

            // Subscribe for notifications of when a new customer is saved.
            _originService.OriginAdded += this.OnOriginAddedToRepository;

            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllOrigins();
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<OriginViewModel> AllOrigins { get; private set; }

        /// <summary>
        /// Returns the total sales sum of all selected customers.
        /// </summary>
        //public double TotalSelectedSales
        //{
        //    get
        //    {
        //        return this.AllOrigins.Sum(
        //            orVM => orVM.IsSelected ? custVM.TotalSales : 0.0);
        //    }
        //}

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (OriginViewModel orVM in this.AllOrigins)
                orVM.Dispose();

            this.AllOrigins.Clear();
            this.AllOrigins.CollectionChanged -= this.OnCollectionChanged;

            _originService.OriginAdded -= this.OnOriginAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (OriginViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnOriginViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (OriginViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnOriginViewModelPropertyChanged;
        }

        void OnOriginViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as OriginViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnOriginAddedToRepository(object sender, EntityAddedEventArgs<Origin> e)
        {
            var viewModel = new OriginViewModel(e.NewEntity, _originService);
            this.AllOrigins.Add(viewModel);
        }

        #endregion // Event Handling Methods
 
    }
}
