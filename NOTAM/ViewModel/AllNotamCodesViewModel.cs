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
    public class AllNotamCodesViewModel : WorkspaceViewModel
    {


        #region Fields

        NotamCodeService _notamCodeService;

        #endregion // Fields

        #region Constructor

        public AllNotamCodesViewModel(NotamCodeService notamCodeService, ObservableCollection<WorkspaceViewModel> parent)
      {
            parentWorkSpaces = parent;
            Setup(notamCodeService);
      }

       public AllNotamCodesViewModel(NotamCodeService notamCodeService)
        {
            Setup(notamCodeService);
        }

       private void Setup(NotamCodeService notamCodeService)
       {
           if (notamCodeService == null)
               throw new ArgumentNullException("notamCodeService");

           base.DisplayName = Entity.AllNotamCodesViewModel_DisplayName;

           _notamCodeService = notamCodeService;

           // Subscribe for notifications of when a new customer is saved.
           _notamCodeService.NotamCodesAdded += this.OnNotamCodeAddedToRepository;

           // Populate the AllCustomers collection with CustomerViewModels.
           this.CreateAllNotamCodes();
       }


        void CreateAllNotamCodes()
        {
            List<NotamCodeViewModel> all =
                (from cust in _notamCodeService.GetNotamCodes()
                 select new NotamCodeViewModel(cust, _notamCodeService,parentWorkSpaces)).ToList();

            foreach (NotamCodeViewModel cvm in all)
                cvm.PropertyChanged += this.OnNotamCodeViewModelPropertyChanged;

            this.AllNotamCodes = new ObservableCollection<NotamCodeViewModel>(all);
            this.AllNotamCodes.CollectionChanged += this.OnCollectionChanged;
        }
        void GetFilterNotamCodes()
        {
            this.AllNotamCodes.Clear();
            var notamCodefilter = new NotamCodesFilter();
            notamCodefilter.SubjectFilter = SubjectFilter;
            notamCodefilter.SubjectDescFilter = SubjectDescFilter;
            notamCodefilter.ConditionFilter = ConditionFilter;
            notamCodefilter.ConditionDescFilter = ConditionDescFilter;
            List<NotamCodeViewModel> all =
                (from cust in _notamCodeService.GetFilterdNotamCodes(notamCodefilter)
                 select new NotamCodeViewModel(cust, _notamCodeService, parentWorkSpaces)).ToList();

            foreach (NotamCodeViewModel cvm in all)
            {
                cvm.PropertyChanged += this.OnNotamCodeViewModelPropertyChanged;
                this.AllNotamCodes.Add(cvm);
            }
            this.AllNotamCodes.CollectionChanged += this.OnCollectionChanged;

        }

        #endregion // Constructor
#region Properties
        private string _subjectFilter;
        public string SubjectFilter
        {
            get { return _subjectFilter; }
            set 
            { 
                _subjectFilter = value;
                GetFilterNotamCodes();
            }
        }


        private string _subjectdescFilter;
        public string SubjectDescFilter
        {
            get { return _subjectdescFilter; }
            set {
                _subjectdescFilter = value;
                GetFilterNotamCodes();
            }
        }

        private string _conditionFilter;
        public string ConditionFilter
        {
            get { return _conditionFilter; }
            set {
                _conditionFilter = value;
                GetFilterNotamCodes();
            }
        }

        private string _conditiondescFilter;
        public string ConditionDescFilter
        {
            get { return _conditiondescFilter; }
            set
            {
                _conditiondescFilter = value;
                GetFilterNotamCodes();
            }
        }

#endregion
        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<NotamCodeViewModel> AllNotamCodes { get; private set; }

        /// <summary>
        /// Returns the total sales sum of all selected customers.
        /// </summary>
        //public double TotalSelectedSales
        //{
        //    get
        //    {
        //        return this.AllNotamCodes.Sum(
        //            orVM => orVM.IsSelected ? custVM.TotalSales : 0.0);
        //    }
        //}

        #endregion // Public Interface
        void ListView_MouseDoubleClick(object sender, EventArgs e)
        {
            int item = 0;
        }
        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (NotamCodeViewModel orVM in this.AllNotamCodes)
                orVM.Dispose();

            this.AllNotamCodes.Clear();
            this.AllNotamCodes.CollectionChanged -= this.OnCollectionChanged;

            _notamCodeService.NotamCodesAdded -= this.OnNotamCodeAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (NotamCodeViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnNotamCodeViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (NotamCodeViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnNotamCodeViewModelPropertyChanged;
        }

        void OnNotamCodeViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as NotamCodeViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnNotamCodeAddedToRepository(object sender, EntityAddedEventArgs<NotamCode> e)
        {
            var viewModel = new NotamCodeViewModel(e.NewEntity, _notamCodeService);
            this.AllNotamCodes.Add(viewModel);
        }

        #endregion // Event Handling Methods

    }
}
