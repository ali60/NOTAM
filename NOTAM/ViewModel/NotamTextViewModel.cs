using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NOTAM.SERVICE.Model;

namespace NOTAM.ViewModel
{
    public class NotamTextViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        private Notam _notam;
        #region Constructor

        public NotamTextViewModel(Notam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");
            _notam = notam;
            
        }

        #endregion // Constructor

        #region Public Properties

        public string NotamText { get; set; }

        #endregion // Public Properties
        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
