using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;

namespace NOTAM.ViewModel
{
    public class AllAftnsViewModel : WorkspaceViewModel
    {

          #region Fields

        private AftnService _aftnService;

        #endregion // Fields

        #region Constructor

        public AllAftnsViewModel(AftnService aftnService, ObservableCollection<WorkspaceViewModel> parent)
      {
            parentWorkSpaces = parent;
            Setup(aftnService);
      }

       

        public AllAftnsViewModel(AftnService aftnService)
        {
            Setup(aftnService); 
        }

        void CreateAllAftns()
        {
            List<AftnViewModel> all =
                (from cust in _aftnService.GetAftnList()
                 select new AftnViewModel(cust, _aftnService,parentWorkSpaces)).ToList();

            foreach (AftnViewModel cvm in all)
                cvm.PropertyChanged += this.OnAftnViewModelPropertyChanged;

            this.AllAftns = new ObservableCollection<AftnViewModel>(all);
            this.AllAftns.CollectionChanged += this.OnCollectionChanged;
        }

        private void Setup(AftnService aftnService)
        {
            if (aftnService == null)
                throw new ArgumentNullException("aftnService");

            base.DisplayName = Entity.AllAftnsViewModel_DisplayName;

            _aftnService = aftnService;

            // Subscribe for notifications of when a new customer is saved.
            _aftnService.AftnAdded += this.OnAftnAddedToRepository;

            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllAftns();
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<AftnViewModel> AllAftns { get; private set; }

       #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (AftnViewModel orVM in this.AllAftns)
                orVM.Dispose();

            this.AllAftns.Clear();
            this.AllAftns.CollectionChanged -= this.OnCollectionChanged;

            _aftnService.AftnAdded -= this.OnAftnAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (AftnViewModel aftn in e.NewItems)
                    aftn.PropertyChanged += this.OnAftnViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (AftnViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnAftnViewModelPropertyChanged;
        }

        void OnAftnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as AftnViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnAftnAddedToRepository(object sender, EntityAddedEventArgs<Aftn> e)
        {
            var viewModel = new AftnViewModel(e.NewEntity, _aftnService);
            this.AllAftns.Add(viewModel);
        }

        #endregion // Event Handling Methods
 
    }
}
