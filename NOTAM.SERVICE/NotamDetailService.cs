using System;using System.Collections.Generic;using System.IO;using System.Linq;using System.Text;using NOTAM.Service;using NOTAM.SERVICE.Model;namespace NOTAM.SERVICE{    public class NotamDetailService    {        #region Fields        private NotamDataContext _dataContext;        readonly List<NotamDetail> _NotamDetails;        #endregion // Fields        #region Constructor        /// <summary>        /// Creates a new repository of origins.        /// </summary>        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>        public NotamDetailService(NotamDataContext dataContext)        {            _dataContext = dataContext;            _NotamDetails = LoadNotamDetails(_dataContext);        }        #endregion // Constructor        #region Public Interface               /// <summary>        /// Places the specified origin into the repository.        /// If the origin is already in the repository, an        /// exception is not thrown.        /// </summary>        public void Insert(NotamDetail notam)        {            if (notam == null)                throw new ArgumentNullException("notam");            if (!_NotamDetails.Contains(notam))            {                _NotamDetails.Add(notam);                _dataContext.NotamDetails.InsertOnSubmit(notam);                _dataContext.SubmitChanges();                //if (this.NotamAdded != null)                //    this.NotamAdded(this, new EntityAddedEventArgs<Notam>(notam));            }        }        /// <summary>        ///         /// </summary>        /// <param name="origin"></param>        public void Update(NotamDetail notam)        {            _dataContext.SubmitChanges();        }        /// <summary>        ///         /// </summary>        /// <param name="origin"></param>        public void Delete(Notam notam)        {        }        /// <summary>        /// Returns true if the specified origin exists in the        /// repository, or false if it is not.        /// </summary>        public bool ContainsNotam(NotamDetail notam)        {            if (notam == null)                throw new ArgumentNullException("notam");            return _NotamDetails.Contains(notam);        }        public NotamDetail GetByNotamId(int notamId)        {            NotamDetail result = new NotamDetail();            if (_dataContext != null)            {                result = _dataContext.NotamDetails.Where(n => n.Notam.Id.Equals(notamId) ).FirstOrDefault();            }            return result;        }               /// <summary>        /// Returns a shallow-copied list of all origins in the repository.        /// </summary>        public List<NotamDetail> GetNotamDetails()        {            return new List<NotamDetail>(_NotamDetails);        }        public List<Aftn> GetAllAftns()        {            var aftnService = new AftnService(_dataContext);            return aftnService.GetAftnList();        }        public List<Notam> FilterByUser(List<Notam> notamList,string strUser)        {            List<Notam> retList = new List<Notam>();            foreach (Notam nt in notamList)            {                NotamDetail ntDetail= GetByNotamId(nt.Id);                if (ntDetail != null && !string.IsNullOrEmpty(ntDetail.User) && ntDetail.User.Equals(strUser))                    retList.Add(nt);            }            return retList;        }        #endregion // Public Interface        #region Private Helpers        static List<NotamDetail> LoadNotamDetails(NotamDataContext dataContext)        {            var result = new List<NotamDetail>();            if (dataContext != null)            {                var notams = dataContext.NotamDetails;                result.AddRange(notams);            }            return result;        }        static Stream GetResourceStream(string resourceFile)        {            return null;            //Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);            //StreamResourceInfo info = Application.GetResourceStream(uri);            //if (info == null || info.Stream == null)            //    throw new ApplicationException("Missing resource file: " + resourceFile);            //return info.Stream;        }        #endregion // Private Helpers    }}