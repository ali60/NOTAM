using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;

namespace NOTAM.ViewModel
{
  public class AllNotamsViewModel: WorkspaceViewModel
    {

           #region Fields

         private NotamService _notamService;
         private ObservableCollection<WorkspaceViewModel> ParentWorkspaces;
         private String _notamStatus;
         private NotamFilter _notamFilter;
         private bool bDisplayByList;
         RelayCommand _refreshCommand;
         RelayCommand _statisticsCommand;
         RelayCommand _reportCommand;
         private string strUser;
           #endregion // Fields

       // public event EventHandler<IList<Notam>> Reload;

        #region Constructor

      public AllNotamsViewModel(NotamService notamService,ObservableCollection<WorkspaceViewModel> parent, string notamStatus)
      {

          bDisplayByList = false;
          ParentWorkspaces = parent;

          Setup(notamService, notamStatus,null);

      }
      public AllNotamsViewModel(NotamService notamService, ObservableCollection<WorkspaceViewModel> parent, NotamFilter filter)
      {

          bDisplayByList = false;
          ParentWorkspaces = parent;
          _notamFilter = filter;
          Setup(notamService, filter.notamStatus, filter);

      }

      public AllNotamsViewModel(List<Notam> notamList, ObservableCollection<WorkspaceViewModel> parent)
      {

          ParentWorkspaces = parent;

          bDisplayByList = true;
          CreateAllNotamsFromList(notamList);

      }




      public void Setup(NotamService notamService, string notamStatus, NotamFilter filter)
      {
          if (notamService == null)
              throw new ArgumentNullException("notamService");
          switch(notamStatus)
          {
              case "A":
                  base.DisplayName = Entity.ArchiveNotamsViewModel_DisplayName;
                  break;
              case "H":
                  base.DisplayName = Entity.HoldNotamsViewModel_DisplayName;
                  break;
              case "AD":
                  base.DisplayName = Entity.AllArchAndValidNotamsViewModel_DisplayName;
                  break;
              default:
                  base.DisplayName = Entity.AllNotamsViewModel_DisplayName;
                  break;
          }
          _notamService = notamService;

          // Subscribe for notifications of when a new customer is saved.
          _notamService.NotamAdded += this.OnNotamAddedToRepository;
          _notamService.NotamDeleted += this.OnNotamDeletedFromRepository;
          // Reload += this.OnReload;

          // Populate the AllCustomers collection with CustomerViewModels.

          _notamStatus = notamStatus;
          if (filter != null)
              this.GetFilterNotams(filter);
          else
              this.CreateAllNotams(notamStatus);
         
          
                  
      }

      private void OnReload(object sender, IList<Notam> e)
      {
          var notamfilter = new NotamFilter();
          notamfilter.TypeFilter = TypeFilter;
          notamfilter.NumberFilter = NumberFilter;
          notamfilter.YearFilter = YearFilter;
          notamfilter.NotamCodeFilter = NotamCodeFilter;
          List<NotamViewModel> all =
              (from cust in _notamService.GetFilterNotams(notamfilter)
               select new NotamViewModel(cust, _notamService)).ToList();

          foreach (NotamViewModel cvm in all)
              cvm.PropertyChanged += this.OnNotamViewModelPropertyChanged;

          this.AllNotams.Clear(); 

          // this.AllNotams = new ObservableCollection<NotamViewModel>(all);
      }

      void CreateAllNotams(string notamStatus)
        {
          List<NotamViewModel> all = null;
          switch (notamStatus)
          {
              case "D":
                  all =
               (from ntm in _notamService.GetNotams()
                select new NotamViewModel(ntm, _notamService, ParentWorkspaces)).ToList();
                  break;
              case "A":
                   all =
               (from ntm in _notamService.GetArchiveNotams()
                select new NotamViewModel(ntm, _notamService, ParentWorkspaces)).ToList();
                  break;
              case "H":
                   all =
               (from ntm in _notamService.GetHoldNotams()
                select new NotamViewModel(ntm, _notamService, ParentWorkspaces)).ToList();
                  break;
          }
         

            foreach (NotamViewModel cvm in all)
                cvm.PropertyChanged += this.OnNotamViewModelPropertyChanged;

            this.AllNotams = new ObservableCollection<NotamViewModel>(all);
            this.AllNotams.CollectionChanged += this.OnCollectionChanged;
            
        }

      void CreateAllNotamsFromList(List<Notam> notamList)
      {
          base.DisplayName = Entity.AllArchAndValidNotamsViewModel_DisplayName;

          // Subscribe for notifications of when a new customer is saved.
          List<NotamViewModel> all = null;
          all =
          (from ntm in notamList
           select new NotamViewModel(ntm, _notamService, ParentWorkspaces)).ToList();


          foreach (NotamViewModel cvm in all)
              cvm.PropertyChanged += this.OnNotamViewModelPropertyChanged;

          this.AllNotams = new ObservableCollection<NotamViewModel>(all);
          this.AllNotams.CollectionChanged += this.OnCollectionChanged;

      }



      void GetFilterNotams()
      {
          var notamfilter = new NotamFilter();
          notamfilter.TypeFilter = TypeFilter;
          notamfilter.NumberFilter = NumberFilter;
          notamfilter.YearFilter = YearFilter;
          notamfilter.NotamCodeFilter = NotamCodeFilter;
          notamfilter.NotamAeroFilter = NotamAeroFilter;
          notamfilter.notamStatus = _notamStatus;
          notamfilter.ItemEFilter = _itemEFilter;
          if (_notamFilter != null)
          {
              notamfilter.TypeFilter = TypeFilter ?? _notamFilter.TypeFilter;
              notamfilter.NumberFilter = NumberFilter ?? _notamFilter.NumberFilter;
              notamfilter.YearFilter = YearFilter ?? _notamFilter.YearFilter;
              notamfilter.NotamCodeFilter = NotamCodeFilter ?? _notamFilter.NotamCodeFilter;
              notamfilter.NotamAeroFilter = NotamAeroFilter ?? _notamFilter.NotamAeroFilter;
              notamfilter.notamStatus = _notamStatus ?? _notamFilter.notamStatus;
              notamfilter.ItemEFilter = _itemEFilter ?? _notamFilter.ItemEFilter;

          }
          GetFilterNotams(notamfilter);
      }

      void GetFilterNotams(NotamFilter notamfilter)
        {
            strUser = notamfilter.UserFilter;
            if (AllNotams != null)
                this.AllNotams.Clear();
            else
                AllNotams = new ObservableCollection<NotamViewModel>();

            List<NotamViewModel> all =
                  (from cust in _notamService.GetFilterNotams(notamfilter)
                   select new NotamViewModel(cust, _notamService, ParentWorkspaces)).ToList();

            foreach (NotamViewModel cvm in all)
            {
                cvm.PropertyChanged += this.OnNotamViewModelPropertyChanged;
                this.AllNotams.Add(cvm);
            }

            //this.AllNotams = new ObservableCollection<NotamViewModel>(all);
            this.AllNotams.CollectionChanged += this.OnCollectionChanged;
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
              GetFilterNotams();
          }
      }


      private string _numberFilter;
      public string NumberFilter
      {
          get { return _numberFilter; }
          set {
              _numberFilter = value;
              GetFilterNotams();
          }
      }

      private string _yearFilter;
      public string YearFilter
      {
          get { return _yearFilter; }
          set
          {
              _yearFilter = value;
              GetFilterNotams();
          }
      }

      private string _notamCodeFilter;
      public string NotamCodeFilter
      {
          get { return _notamCodeFilter; }
          set
          {
              _notamCodeFilter = value;
              GetFilterNotams();
          }
      }

      private string _notamAeroFilter;
      public string NotamAeroFilter
      {
          get { return _notamAeroFilter; }
          set
          {
              _notamAeroFilter = value;
              GetFilterNotams();
          }
      }
      private string _itemEFilter;
      public string ItemEFilter
      {
          get { return _itemEFilter; }
          set
          {
              _itemEFilter = value;
              GetFilterNotams();
          }
      }

      
     

      #endregion

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<NotamViewModel> AllNotams { get; private set; }

      #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (NotamViewModel nVM in this.AllNotams)
                nVM.Dispose();

            this.AllNotams.Clear();
            this.AllNotams.CollectionChanged -= this.OnCollectionChanged;

            _notamService.NotamAdded -= this.OnNotamAddedToRepository;
            _notamService.NotamDeleted -= this.OnNotamDeletedFromRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (NotamViewModel orVM in e.NewItems)
                    orVM.PropertyChanged += this.OnNotamViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (NotamViewModel orVM in e.OldItems)
                    orVM.PropertyChanged -= this.OnNotamViewModelPropertyChanged;
        }

        void OnNotamViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as NotamViewModel).VerifyPropertyName(IsSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            //if (e.PropertyName == IsSelected)
            //    this.OnPropertyChanged("TotalSelectedSales");
        }

        public void RemoveNotam()
        {

        }
        void OnNotamAddedToRepository(object sender, EntityAddedEventArgs<Notam> e)
        {
            var viewModel = new NotamViewModel(e.NewEntity, _notamService);
            this.AllNotams.Add(viewModel);
        }
        void OnNotamDeletedFromRepository(object sender, EntityAddedEventArgs<Notam> e)
        {
            var delNotam = AllNotams.Where(n => n.NotamId == e.NewEntity.Id).First();
            if (delNotam != null)
                this.AllNotams.Remove(delNotam);
        }
        void Refresh()
        {
            GetFilterNotams();
        }

        void RunStat()
        {
            int iN=0,iR=0,iC=0,iA=0,iB=0;
            foreach (NotamViewModel cvm in AllNotams)
            {
                iN += (cvm.NotamType == "N" ? 1 : 0);
                iC += (cvm.NotamType == "C" ? 1 : 0);
                iR += (cvm.NotamType == "R" ? 1 : 0);
                iA += (cvm.Type == "A" ? 1 : 0);
                iB += (cvm.Type == "B" ? 1 : 0);
                
            }
            string strWrite="";

            if(!string.IsNullOrEmpty(strUser))
                strWrite = "NOTAMS FOR USER :\t" + strUser + "\r\n";
            strWrite += "TOTALS NOTAMS:\t" + AllNotams.Count.ToString() + "\r\n";
            strWrite += "NEW NOTAMS:\t" + iN.ToString() + "\r\n";
            strWrite += "CANCEL NOTAMS:\t" + iC.ToString() + "\r\n";
            strWrite += "REPLACE NOTAMS:\t" + iR.ToString() + "\r\n";
            strWrite += "A Type NOTAMS:\t" + iA.ToString() + "\r\n";
            strWrite += "B Type NOTAMS:\t" + iB.ToString() + "\r\n";
            string subPath = "c:\\AISADMin\\"; // your code goes here

            bool exists = System.IO.Directory.Exists((subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory((subPath));
            string strFile = subPath+"stat.txt";
            System.IO.File.WriteAllText(strFile, strWrite);
            System.Diagnostics.Process.Start("notepad.exe", strFile);
        }

        void RunReport()
        {
            string strText = "";
            var ntmBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(strUser))
                ntmBuilder.Append("NOTAMS FOR USER :\t" + strUser + Environment.NewLine);
            strText = "TOTALS NOTAMS:\t" + AllNotams.Count.ToString() + Environment.NewLine;
            ntmBuilder.Append(strText);
            foreach (NotamViewModel cvm in AllNotams)
            {
                NotamSender ns = new NotamSender(cvm.Notam);
                ntmBuilder.Append("__________________________________________________________________" + Environment.NewLine);
                strText = ns.GenerateNotamText();
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
      
      public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand== null)
                {
                    _refreshCommand = new RelayCommand(
                        param => this.Refresh(),
                        param => true
                        );
                }
                return _refreshCommand;
            }
        }
        public ICommand StatisticsCommand
        {
            get
            {
                if (_statisticsCommand == null)
                {
                    _statisticsCommand = new RelayCommand(
                        param => this.RunStat(),
                        param => true
                        );
                }
                return _statisticsCommand;
            }
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

        #endregion // Event Handling Methods
 
    }
}
