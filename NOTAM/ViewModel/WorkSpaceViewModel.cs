using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace NOTAM.ViewModel
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Fields

        RelayCommand _closeCommand;

        protected ObservableCollection<WorkspaceViewModel> parentWorkSpaces;

        #endregion // Fields

        #region Constructor

        protected WorkspaceViewModel()
        {
        }

        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]

        private ICommand _handleDoubleClick;

        public ICommand HandleDoubleClick
        {
            get
            {
                if (_handleDoubleClick == null)
                {
                    _handleDoubleClick = new RelayCommand(DoubleClick, param => true);
                        //new RelayCommand(
                        //param => this.DoubleClick(),
                        //param => true
                        //);
                }
                return _handleDoubleClick;
            }
            set { _handleDoubleClick = value; } //{

            //}
        }

        protected virtual void DoubleClick(object parameter)
        {
           

        }

        protected void SetActiveWorkspace(WorkspaceViewModel workspace)
        {

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(parentWorkSpaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

    }
}
