using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;

namespace NOTAM.SERVICE
{
    public class SnowtamDetailService
    {

        #region Fields

        private NotamDataContext _dataContext;

        readonly List<SnowtamDetail> _SnowtamDetails;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public SnowtamDetailService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _SnowtamDetails = LoadSnowtamDetails(_dataContext);

        }

        #endregion // Constructor

        #region Public Interface



        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(SnowtamDetail snowtam)
        {
            if (snowtam == null)
                throw new ArgumentNullException("snowtam");

            if (!_SnowtamDetails.Contains(snowtam))
            {
                _SnowtamDetails.Add(snowtam);
                _dataContext.SnowtamDetails.InsertOnSubmit(snowtam);
                _dataContext.SubmitChanges();
                //if (this.NotamAdded != null)
                //    this.NotamAdded(this, new EntityAddedEventArgs<Notam>(notam));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(SnowtamDetail snowtam)
        {
            _dataContext.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(SnowtamDetail snowtam)
        {

        }


        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsSnowtam(SnowtamDetail snowtam)
        {
            if (snowtam == null)
                throw new ArgumentNullException("snowtam");

            return _SnowtamDetails.Contains(snowtam);
        }

        public SnowtamDetail GetByNotamId(int snowtamId)
        {
            SnowtamDetail result = new SnowtamDetail();
            if (_dataContext != null)
            {
                result = _dataContext.SnowtamDetails.Where(n => n.Snowtam.Id.Equals(snowtamId)).FirstOrDefault();
            }
            return result;
        }



        public List<SnowtamDetail> GetSnowtamDetails()
        {
            return new List<SnowtamDetail>(_SnowtamDetails);
        }

        public List<Aftn> GetAllAftns()
        {
            var aftnService = new AftnService(_dataContext);
            return aftnService.GetAftnList();
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<SnowtamDetail> LoadNotamDetails(NotamDataContext dataContext)
        {
            var result = new List<SnowtamDetail>();
            if (dataContext != null)
            {
                var notams = dataContext.SnowtamDetails;
                result.AddRange(notams);
            }
            return result;

        }
        static List<SnowtamDetail> LoadSnowtamDetails(NotamDataContext dataContext)
        {
            var result = new List<SnowtamDetail>();
            if (dataContext != null)
            {
                var notams = dataContext.SnowtamDetails;
                result.AddRange(notams);
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
