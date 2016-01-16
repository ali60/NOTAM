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
   public class AllFIRsViewModel: WorkspaceViewModel
    {

           #region Fields

            private FIRService _firService;

        #endregion // Fields

        #region Constructor

      public AllFIRsViewModel(FIRService firService, ObservableCollection<WorkspaceViewModel> parent)
      {
            parentWorkSpaces = parent;
            Setup(firService);
      }

        public AllFIRsViewModel(FIRService firService)
        {
            Setup(firService);
        }

       private void Setup(FIRService firService)
       {
           if (firService == null)
               throw new ArgumentNullException("firService");

           base.DisplayName = Entity.AllFIRsViewModel_DisplayName;

           _firService = firService;

           // Subscribe for notifications of when a new customer is saved.
           _firService.FIRAdded += this.OnFIRAddedToRepository;

           // Populate the AllCustomers collection with CustomerViewModels.
           this.CreateAllFIRs();      
       }

      

        void CreateAllFIRs()
        {
            List<FIRViewModel> all =
                (from cust in _firService.GetFIRs()
                 select new FIRViewModel(cust, _firService,parentWorkSpaces)).ToList();

            foreach (FIRViewModel cvm in all)
                cvm.PropertyChanged += this.OnFIRViewModelPropertyChanged;

            this.AllFIRs = new ObservableCollection<FIRViewModel>(all);
            this.AllFIRs.CollectionChanged += this.OnCollectionChanged;
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<FIRViewModel> AllFIRs { get; private set; }

      #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (FIRViewModel orVM in this.AllFIRs)
                orVM.Dispose();

            this.AllFIRs.Clear();
            this.AllFIRs.CollectionChanged -= this.OnCollectionChanged;

            _firService.FIRAdded -= this.OnFIRAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (FIRViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnFIRViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (FIRViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnFIRViewModelPropertyChanged;
        }

        void OnFIRViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as FIRViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnFIRAddedToRepository(object sender, EntityAddedEventArgs<FIR> e)
        {
            var viewModel = new FIRViewModel(e.NewEntity, _firService);
            this.AllFIRs.Add(viewModel);
        }

        #endregion // Event Handling Methods
 
    }
}
