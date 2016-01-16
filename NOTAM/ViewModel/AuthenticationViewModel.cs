using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Controls;
using NOTAM.Service;
using NOTAM.SERVICE;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;

namespace NOTAM.ViewModel
{
    public interface IViewModel { }

    public class AuthenticationViewModel : IViewModel, INotifyPropertyChanged
    {

        private static NotamDataContext _dataDC = new NotamDataContext(); 

        private readonly IAuthenticationService _authenticationService;
        private readonly RelayCommand _loginCommand;
        private readonly RelayCommand _logoutCommand;
        private readonly RelayCommand _showViewCommand;
        private string _username;
        private string _status;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new RelayCommand(Login, CanLogin);
            _logoutCommand = new RelayCommand(Logout, CanLogout);
        }

        #region Properties
        [Required]
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}.",
                                         Thread.CurrentPrincipal.Identity.Name);
                return  string.Empty;
            }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get { return _loginCommand; } }

        public RelayCommand LogoutCommand { get { return _logoutCommand; } }

        public RelayCommand ShowViewCommand { get { return _showViewCommand; } }
        #endregion

       
        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                //Validate credentials through the authentication service
                User user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Role);

                //Update UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                NotifyPropertyChanged("ShowLogin");
               //_loginCommand.RaiseCanExecuteChanged();
               //_logoutCommand.RaiseCanExecuteChanged();
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }

            if (IsAuthenticated)
            {

                MainWindow window = new MainWindow();

                // Create the ViewModel to which 
                // the main window binds.

                var viewModel = new MainWindowViewModel(_dataDC);

                // When the ViewModel asks to be closed, 
                // close the window.
                EventHandler handler = null;
                handler = delegate
                              {
                                  viewModel.RequestClose -= handler;
                                  window.Close();
                              };
                viewModel.RequestClose += handler;

                // Allow all controls in the window to h
                // bind to the ViewModel by setting the 
                // DataContext, which propagates down 
                // the element tree.
                window.DataContext = viewModel;
                window.Closing += OnMainWindowClosing;
                try
                {
                    window.ShowDialog();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
                
            }


        }
        private void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to exit?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }
        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        private void Logout(object parameter)
        {

            try
            {
                test();
            }
            catch (SecurityException ex)
            {
                throw ex;
            }
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                NotifyPropertyChanged("ShowLogin");
                //_loginCommand.RaiseCanExecuteChanged();
                //_logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
                
            }

           
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        private void test()
        {
            var str = "";

        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }         

        public bool ShowLogin
        {
            get { return !IsAuthenticated; } 
        }
                  
       


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
