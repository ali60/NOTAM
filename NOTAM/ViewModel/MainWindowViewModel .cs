using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Data;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using Word = Microsoft.Office.Interop.Word;

namespace NOTAM.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        ReadOnlyCollection<CommandGroupViewModel> _commands;
        readonly OriginService _originService;
        readonly FIRService _firService;
        readonly NotamService _notamService;
        readonly AerodomService _aerodomService;
        readonly NotamCodeService _notamCodeService;
         readonly SnowtamService _snowtamService;
         readonly AuthenticationService _authService;
         readonly AftnService _aftnService;
         readonly IntlNotamService _intlNotamService;
        ObservableCollection<WorkspaceViewModel> _workspaces;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel(NotamDataContext  dataContext )
        {
            base.DisplayName = Entity.MainWindowViewModel_DisplayName;

            _originService = new OriginService(dataContext);
            _firService = new FIRService(dataContext);
            _notamService = new NotamService(dataContext);
            _aerodomService = new AerodomService(dataContext);
            _notamCodeService = new NotamCodeService(dataContext);
            _snowtamService = new SnowtamService(dataContext);
            _authService = new AuthenticationService(dataContext);
            _aftnService = new AftnService(dataContext);
            _intlNotamService = new IntlNotamService(dataContext);
            var workspace = new NotamSearchViewModel(_notamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,1,0);
            dispatcherTimer.Start();


        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 5 && DateTime.Now.Minute == 30 )
            {
                var notamFilter = new NotamFilter();
                notamFilter.notamStatus =  "D" ;
                notamFilter.PermEstFilter = "EST" ;
                _notamService.Archive();
                _notamService.Reload();
                _snowtamService.Archive();
                _snowtamService.Reload();
                List<Notam> ExpiredNotams = _notamService.GetFilterNotams(notamFilter);
                if (ExpiredNotams.Count == 0)
                    return;
                string subPath = "c:\\AISAdmin\\"; // your code goes here

                try
                {
                    bool exists = System.IO.Directory.Exists((subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory((subPath));
                    subPath += "EXPIRED\\";
                    exists = System.IO.Directory.Exists((subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory((subPath));
                    string strNotams = "THESE FOLLOWING NOTAMS:\n";
                    int i = 1;
                    foreach (Notam nt in ExpiredNotams)
                    {
                        strNotams += (nt.Type + nt.Number + "/" + nt.Year + ",");
                        if ((i % 10) == 0)
                            strNotams += "\n";
                        i++;
                    }
                    strNotams += "\nSHALL BE CANCELED OR REPLACED";
                    var FileName = subPath + DateTime.Now.ToString("yyMMdd") + ".txt";
                    System.IO.File.WriteAllText(FileName, strNotams);
                    MessageBox.Show(strNotams, "**Caution**");
                }
                catch (System.Exception ex)
                {

                }
            }
        }

        public MainWindowViewModel(NotamDataContext dataContext, Notam notam):this(dataContext)
        {
          NotamViewModel workspace = new NotamViewModel(notam, _notamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        #endregion // Constructor

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandGroupViewModel> GroupCommands
        {
            get
            {
                if (_commands == null)
                {
                    //List<CommandViewModel> cmds = this.CreateCommands();
                    List<CommandGroupViewModel> grpCmds = new List<CommandGroupViewModel>()
                                                              {
                                                                  new CommandGroupViewModel("Create Notam", CreateNotamCommands()),
                                                                  new CommandGroupViewModel("Search", CreateSearchCommands()),
                                                                  new CommandGroupViewModel("Check List", CreateCheckListCommands()),
                                                                   new CommandGroupViewModel("Administrative", CreateAdministrativeCommands())
                                                              };
                    _commands = new ReadOnlyCollection<CommandGroupViewModel>(grpCmds);
                }
                return _commands;
            }
        }


        List<CommandViewModel> CreateNotamCommands()
        {
            return new List<CommandViewModel>
            {
                 new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewNotam,
                    new RelayCommand(param => this.CreateNewNNotam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_CancelNotam,
                    new RelayCommand(param => this.CreateNewCNotam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ReplaceNotam,
                    new RelayCommand(param => this.CreateNewRNotam())),


                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_Snowtam,
                    new RelayCommand(param => this.CreateSNowtam())),
                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewFreeStyle,
                    new RelayCommand(param => this.CreateFreeStyleNotam()))
                       };
        }
        List<CommandViewModel> CreateSearchCommands()
        {
            return new List<CommandViewModel>
                       {
                           new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllNotams,
                    new RelayCommand(param => this.ShowAllNotams("D") )),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllArchiveNotams,
                    new RelayCommand(param => this.ShowAllNotams("A") )),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllHoldNotams,
                    new RelayCommand(param => this.ShowAllNotams("H") )),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllNotamCodes,
                    new RelayCommand(param => this.ShowAllNotamCodes())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllSnowtams,
                    new RelayCommand(param => this.ShowAllSnowtams("D"))),
                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllArichiveSnowtams,
                    new RelayCommand(param => this.ShowAllSnowtams("A") )),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_SearchIntlNotams,
                    new RelayCommand(param => this.CreateNewIntlSearch() ))
                          };
        }

        List<CommandViewModel> CreateCheckListCommands()
        {
            return new List<CommandViewModel>
                       {
                            new CommandViewModel(
                    Entity.MainWindowViewModel_Command_RunArchive,
                    new RelayCommand(param => this.RunArchive())),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_RunSummary,
                    new RelayCommand(param => this.RunSummary())),
                          };
        }

        List<CommandViewModel> CreateAdministrativeCommands()
        {
            return new List<CommandViewModel>
                       {
                             new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllOrigins,
                    new RelayCommand(param => this.ShowAllOrigins())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewOrigins,
                    new RelayCommand(param => this.CreateNewOrigin())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllFIRs,
                    new RelayCommand(param => this.ShowAllFIRs())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewFIR,
                    new RelayCommand(param => this.CreateNewFIR())),

                      new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllAerodoms,
                    new RelayCommand(param => this.ShowAllAerodoms())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewAerodom,
                    new RelayCommand(param => this.CreateNewAerodom())),


                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewNotamCode,
                    new RelayCommand(param => this.CreateNewNotamCode())),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllAftns,
                    new RelayCommand(param => this.ShowAllAftns())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewAftn,
                    new RelayCommand(param => this.CreateNewAftn())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllUsers,
                    new RelayCommand(param => this.ShowAllUsers())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewUser,
                    new RelayCommand(param => this.CreateNewUser())),
                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_Setting,
                    new RelayCommand(param => this.CreateSettingWindow())),
               
                          };
        }


        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                 new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewNotam,
                    new RelayCommand(param => this.CreateNewNNotam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_CancelNotam,
                    new RelayCommand(param => this.CreateNewCNotam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ReplaceNotam,
                    new RelayCommand(param => this.CreateNewRNotam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_Snowtam,
                    new RelayCommand(param => this.CreateSNowtam())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllNotams,
                    new RelayCommand(param => this.ShowAllNotams("D") )),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllArchiveNotams,
                    new RelayCommand(param => this.ShowAllNotams("A") )),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllHoldNotams,
                    new RelayCommand(param => this.ShowAllNotams("H") )),
                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllOrigins,
                    new RelayCommand(param => this.ShowAllOrigins())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewOrigins,
                    new RelayCommand(param => this.CreateNewOrigin())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllFIRs,
                    new RelayCommand(param => this.ShowAllFIRs())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewFIR,
                    new RelayCommand(param => this.CreateNewFIR())),

                      new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllAerodoms,
                    new RelayCommand(param => this.ShowAllAerodoms())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewAerodom,
                    new RelayCommand(param => this.CreateNewAerodom())),

                      new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllNotamCodes,
                    new RelayCommand(param => this.ShowAllNotamCodes())),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewNotamCode,
                    new RelayCommand(param => this.CreateNewNotamCode())),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllAftns,
                    new RelayCommand(param => this.ShowAllAftns())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewAftn,
                    new RelayCommand(param => this.CreateNewAftn())),

                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_ViewAllUsers,
                    new RelayCommand(param => this.ShowAllUsers())),

                new CommandViewModel(
                    Entity.MainWindowViewModel_Command_NewUser,
                    new RelayCommand(param => this.CreateNewUser())),
               
                    new CommandViewModel(
                    Entity.MainWindowViewModel_Command_RunArchive,
                    new RelayCommand(param => this.RunArchive())),

                     new CommandViewModel(
                    Entity.MainWindowViewModel_Command_RunSummary,
                    new RelayCommand(param => this.RunSummary())),
               };

        }

       

        #endregion // Commands

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

         [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewOrigin()
        {
            Origin newOrigin = Origin.CreateNewOrigin();
            OriginViewModel workspace = new OriginViewModel(newOrigin, _originService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllOrigins()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllOriginsViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            AllOriginsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllOriginsViewModel)
                as AllOriginsViewModel;

            if (workspace == null)
            {
                workspace = new AllOriginsViewModel(_originService,Workspaces);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

         [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewFIR()
        {
            FIR newFir = FIR.CreateNewFIR() ;
            FIRViewModel workspace = new FIRViewModel(newFir, _firService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllFIRs()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllFIRsViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            AllFIRsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllFIRsViewModel)
                as AllFIRsViewModel;
            if (workspace == null)
            {
                workspace = new AllFIRsViewModel(_firService,  Workspaces);
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }


        void ShowAllNotams(string notamStatus)
        {
            string strTitle = Entity.AllNotamsViewModel_DisplayName;
            if (notamStatus == "A")
                strTitle = Entity.MainWindowViewModel_Command_ViewAllArchiveNotams;
            if (notamStatus == "H")
                strTitle = Entity.MainWindowViewModel_Command_ViewAllHoldNotams;
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == strTitle)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            _notamService.Reload();
            _notamService.Archive();
            AllNotamsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllNotamsViewModel)
                as AllNotamsViewModel;
            //if (workspace == null)
            //{
                workspace = new AllNotamsViewModel(_notamService, Workspaces,notamStatus);
                this.Workspaces.Add(workspace);
           // }
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllSnowtams(string notamStatus)
        {
            string strTitle = Entity.AllSnowtamsViewModel_DisplayName;
            if (notamStatus == "A")
                strTitle = Entity.ArchiveSnowtamsViewModel_DisplayName;
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == strTitle)
                {
                    Workspaces.Remove(obj);
                    break;
                }
 
            }
            _snowtamService.Archive();
            _snowtamService.Reload();
            
            AllSnowtamsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllSnowtamsViewModel)
                as AllSnowtamsViewModel;
            //if (workspace == null)
            //{
            workspace = new AllSnowtamsViewModel(_snowtamService, Workspaces, notamStatus);
            this.Workspaces.Add(workspace);
            // }
            this.SetActiveWorkspace(workspace);
        }

       
        void CreateNewNNotam()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.NotamViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewNNotam();
            NotamViewModel workspace = new NotamViewModel(newNotam, _notamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateFreeStyleNotam()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.NotamViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewNNotam();
            NotamViewModel workspace = new NotamViewModel(newNotam, _notamService, Workspaces);
            workspace.FreeStyle = true;
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        
        void CreateNewIntlSearch()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.MainWindowViewModel_Command_SearchIntlNotams)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            IntlNotamsSearchViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is IntlNotamsSearchViewModel)
                as IntlNotamsSearchViewModel;
            _intlNotamService.Archive();
            if (workspace == null)
            {
                workspace = new IntlNotamsSearchViewModel(_intlNotamService, _notamService, _aerodomService, _notamCodeService, Workspaces);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        void CreateNewCNotam()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.CNotamViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewCNotam();
            NotamViewModel workspace = new NotamViewModel(newNotam, _notamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void CreateNewRNotam()
        {
            foreach (var obj in this.Workspaces)
            {
                 if (obj.DisplayName == Entity.RNotamViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            NOTAM.SERVICE.Model.Notam newNotam = NOTAM.SERVICE.Model.Notam.CreateNewRNotam();
            NotamViewModel workspace = new NotamViewModel(newNotam, _notamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void CreateSNowtam()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.SnowtamViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            Snowtam newSnowtam = NOTAM.SERVICE.Model.Snowtam.CreateNewSnowtam();
            SnowtamViewModel workspace = new SnowtamViewModel(newSnowtam, _snowtamService, Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

         [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewAerodom()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AerodomViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            Aerodom newAerodom = Aerodom.CreateNewAerodom();
            AerodomViewModel workspace = new AerodomViewModel(newAerodom, _aerodomService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllAerodoms()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllAerodomsViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            AllAerodomsViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllAerodomsViewModel)
                as AllAerodomsViewModel;
            if (workspace == null)
            {
                workspace = new AllAerodomsViewModel(_aerodomService, Workspaces);
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewAftn()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AftnViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            Aftn newAftn = Aftn.CreateNewAftn();
            var workspace = new AftnViewModel(newAftn, _aftnService,_workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllAftns()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllAftnsViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            var workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllAftnsViewModel)
                as AllAftnsViewModel;
            if (workspace == null)
            {
                workspace = new AllAftnsViewModel(_aftnService,Workspaces);
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }


        void CreateSettingWindow()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.UserViewModel_SettingName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            SettingViewModel workspace = new SettingViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewUser()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.UserViewModel_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            User newUser = User.CreateNewUser();
            UserViewModel workspace = new UserViewModel(newUser, _authService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }


        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void ShowAllUsers()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllUsersViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            AllUsersViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllUsersViewModel)
                as AllUsersViewModel;
            if (workspace == null)
            {
                workspace = new AllUsersViewModel(_authService);
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        void CreateNewNotamCode()
        {
            NotamCode newNotamCode = NotamCode.CreateNewNotamCodes();
            NotamCodeViewModel workspace = new NotamCodeViewModel(newNotamCode, _notamCodeService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllNotamCodes()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.AllNotamCodesViewModel_DisplayName)
                {
                    Workspaces.Remove(obj);
                    break;
                }

            }
            AllNotamCodesViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is AllNotamCodesViewModel)
                as AllNotamCodesViewModel;
            if (workspace == null)
            {
                workspace = new AllNotamCodesViewModel(_notamCodeService,Workspaces);
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        void RunArchive()
        {
            _notamService.Archive();
            _notamService.Reload();
            _snowtamService.Archive();
            _snowtamService.Reload();
            MessageBox.Show("NOTAMS with toDate older than now are sent to archive.");
            
        }

        void RunSummary()
        {
            foreach (var obj in this.Workspaces)
            {
                if (obj.DisplayName == Entity.NotamSummary_DisplayName)
                {
                    this.SetActiveWorkspace(obj);
                    return;
                }

            }
            var workspace = new NotamSummaryViewModel(_notamService,_intlNotamService, _aerodomService, _notamCodeService, this.Workspaces);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

     
        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            

            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}
