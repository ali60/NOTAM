using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text;
using NOTAM.Properties;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;

namespace NOTAM.ViewModel
{
    public class AllSnowtamsViewModel : WorkspaceViewModel
    {

        #region Fields

        private SnowtamService _snowtamService;
        private ObservableCollection<WorkspaceViewModel> ParentWorkspaces;
        RelayCommand _reportCommand;

        #endregion // Fields
        // public event EventHandler<IList<Snowtam>> Reload;

        #region Constructor

        public AllSnowtamsViewModel(SnowtamService snowtamService, ObservableCollection<WorkspaceViewModel> parent, string snowtamStatus)
        {

            ParentWorkspaces = parent;

            Setup(snowtamService, snowtamStatus);

        }


        public void Setup(SnowtamService snowtamService, string snowtamStatus)
        {
            if (snowtamService == null)
                throw new ArgumentNullException("snowtamService");
            switch (snowtamStatus)
            {
                case "A":
                    base.DisplayName = Entity.ArchiveSnowtamsViewModel_DisplayName;
                    break;
                default:
                    base.DisplayName = Entity.AllSnowtamsViewModel_DisplayName;
                    break;
            }
            _snowtamService = snowtamService;

            // Subscribe for notifications of when a new customer is saved.
            _snowtamService.SnowtamAdded += this.OnSnowtamAddedToRepository;

            // Reload += this.OnReload;

            // Populate the AllCustomers collection with CustomerViewModels.


            this.CreateAllSnowtams(snowtamStatus);



        }

        private void OnReload(object sender, IList<Snowtam> e)
        {
            List<SnowtamViewModel> all =
                (from cust in _snowtamService.GetSnowtams()
                 select new SnowtamViewModel(cust, _snowtamService, ParentWorkspaces)).ToList();

            foreach (SnowtamViewModel cvm in all)
                cvm.PropertyChanged += this.OnSnowtamViewModelPropertyChanged;

            this.AllSnowtams.Clear();

            // this.AllSnowtams = new ObservableCollection<SnowtamViewModel>(all);
        }

        void CreateAllSnowtams(string snowtamStatus)
        {
            List<SnowtamViewModel> all = null;
            switch (snowtamStatus)
            {
                case "D":
                    all =
                 (from ntm in _snowtamService.GetSnowtams()
                  select new SnowtamViewModel(ntm, _snowtamService,ParentWorkspaces)).ToList();
                    break;
                case "A":
                    all =
                (from ntm in _snowtamService.GetArchiveSnowtams()
                 select new SnowtamViewModel(ntm, _snowtamService,ParentWorkspaces)).ToList();
                    break;
            }


            foreach (SnowtamViewModel cvm in all)
                cvm.PropertyChanged += this.OnSnowtamViewModelPropertyChanged;

            this.AllSnowtams = new ObservableCollection<SnowtamViewModel>(all);
            this.AllSnowtams.CollectionChanged += this.OnCollectionChanged;

        }

        void GetFilterSnowtams()
        {
            List<SnowtamViewModel> all =
                (from cust in _snowtamService.GetSnowtams()
                 select new SnowtamViewModel(cust, _snowtamService)).ToList();

            foreach (SnowtamViewModel cvm in all)
            {
                cvm.PropertyChanged += this.OnSnowtamViewModelPropertyChanged;
                this.AllSnowtams.Add(cvm);
            }
        }



        #endregion // Constructor

        #region Search Properties

        private string _typeFilter;
        public string TypeFilter
        {
            get { return _typeFilter; }
            set
            {

                _typeFilter = value;
                GetFilterSnowtams();
            }
        }


        private string _numberFilter;
        public string NumberFilter
        {
            get { return _numberFilter; }
            set
            {
                _numberFilter = value;
                GetFilterSnowtams();
            }
        }

        private string _yearFilter;
        public string YearFilter
        {
            get { return _yearFilter; }
            set
            {
                _yearFilter = value;
                GetFilterSnowtams();
            }
        }

        private string _snowtamCodeFilter;
        public string SnowtamCodeFilter
        {
            get { return _snowtamCodeFilter; }
            set
            {
                _snowtamCodeFilter = value;
                GetFilterSnowtams();
            }
        }

        private string _snowtamAeroFilter;
        public string SnowtamAeroFilter
        {
            get { return _snowtamAeroFilter; }
            set
            {
                _snowtamAeroFilter = value;
                GetFilterSnowtams();
            }
        }




        #endregion


        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<SnowtamViewModel> AllSnowtams { get; private set; }

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (SnowtamViewModel nVM in this.AllSnowtams)
                nVM.Dispose();

            this.AllSnowtams.Clear();
            this.AllSnowtams.CollectionChanged -= this.OnCollectionChanged;

            _snowtamService.SnowtamAdded -= this.OnSnowtamAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SnowtamViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnSnowtamViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SnowtamViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnSnowtamViewModelPropertyChanged;
        }

        void OnSnowtamViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as SnowtamViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        void OnSnowtamAddedToRepository(object sender, EntityAddedEventArgs<Snowtam> e)
        {
            var viewModel = new SnowtamViewModel(e.NewEntity, _snowtamService);
            this.AllSnowtams.Add(viewModel);
        }



        #endregion // Event Handling Methods

        void RunReport()
        {
            string strText = "";
            var ntmBuilder = new StringBuilder();

            strText = "TOTALS SNOWTAMS:\t" + AllSnowtams.Count.ToString() + Environment.NewLine;
            ntmBuilder.Append(strText);
            foreach (SnowtamViewModel cvm in AllSnowtams)
            {
                ntmBuilder.Append("__________________________________________________________________" + Environment.NewLine);
                strText = NotamSender.GeneratePreviewSnowtamText(cvm.Snowtam);
                ntmBuilder.Append(strText + Environment.NewLine);


            }
            string subPath = "c:\\AISADMin\\"; // your code goes here

            bool exists = System.IO.Directory.Exists((subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory((subPath));
            string strFile = subPath + "report.txt";
            System.IO.File.WriteAllText(strFile, ntmBuilder.ToString());
            System.Diagnostics.Process.Start(strFile);
        }
        public ICommand ReportCommand
        {
            get
            {
                if (_reportCommand == null)
                {
                    _reportCommand = new RelayCommand(
                        param => this.RunReport(),
                        param => true
                        );
                }
                return _reportCommand;
            }
        }

    }
}
