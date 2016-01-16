using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE
{
    public class NotamCodeService
    {
        #region Fields

        readonly List<NotamCode> _NotamCode;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public NotamCodeService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _NotamCode = LoadNotamCodes(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<NotamCode>> NotamCodesAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(NotamCode NCode)
        {
            if (NCode == null)
                throw new ArgumentNullException("NotamCode");

            if (!_NotamCode.Contains(NCode))
            {
                _NotamCode.Add(NCode);
                _dataContext.NotamCodes.InsertOnSubmit(NCode);
                _dataContext.SubmitChanges();
                if (this.NotamCodesAdded != null)
                    this.NotamCodesAdded(this, new EntityAddedEventArgs<NotamCode>(NCode));
            }
        }
        public bool IsNew(NotamCode NCode)
        {
            if(NCode == null || string.IsNullOrEmpty(NCode.Subject) || string.IsNullOrEmpty(NCode.Condition))
                return false;
            var item = _NotamCode.Where(n => n.Subject.Equals(NCode.Subject) && n.Condition.Equals(NCode.Condition)).ToList();
            if (item.Count==0)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(NotamCode NCode)
        {
           _dataContext.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(NotamCode NCode)
        {

        }


        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsNotamCode(NotamCode NCode)
        {
            if (NCode == null)
                throw new ArgumentNullException("NotamCode");

            return _NotamCode.Contains(NCode);
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<NotamCode> GetNotamCodes()
        {
            return new List<NotamCode>(_NotamCode);
        }

        public List<NotamCode> GetFilterdNotamCodes(NotamCodesFilter codeFilter)
        {
            List<NotamCode> result = null;
            if (_dataContext != null)
            {
                try
                {
                    result = _NotamCode.Where(
                        n => (String.IsNullOrEmpty(codeFilter.SubjectFilter) || (!String.IsNullOrEmpty(n.Subject) && n.Subject.Contains(codeFilter.SubjectFilter)))).Where(
                            n =>
                            (String.IsNullOrEmpty(codeFilter.SubjectDescFilter) || (!String.IsNullOrEmpty(n.Subject_Desc) && n.Subject_Desc.ToUpper().Contains(codeFilter.SubjectDescFilter)))).Where(
                        n => (String.IsNullOrEmpty(codeFilter.ConditionFilter) || (!String.IsNullOrEmpty(n.Condition) && n.Condition.Contains(codeFilter.ConditionFilter)))).Where(
                            n =>
                            (String.IsNullOrEmpty(codeFilter.ConditionDescFilter) || (!String.IsNullOrEmpty(n.Condition_Desc) && (n.Condition_Desc).ToUpper().Contains(codeFilter.ConditionDescFilter))))
                        .ToList();
                }
                catch (System.Exception ex)
                {
                    string s = ex.Message;
                }
            }
            return result;
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<NotamCode> LoadNotamCodes(NotamDataContext dataContext)
        {
            var result = new List<NotamCode>();
            if (dataContext != null)
            {
                var notamcodes = dataContext.NotamCodes;
                result.AddRange(notamcodes);
            }
            return result;

        }

        static Stream GetResourceStream(string resourceFile)
        {
            return null;
            //Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            //StreamResourceInfo info = Application.GetResourceStream(uri);
            //if (info == null || info.Stream == null)
            //    throw new ApplicationException("Missing resource file: " + resourceFile);

            //return info.Stream;
        }

        #endregion // Private Helpers


    }
}
