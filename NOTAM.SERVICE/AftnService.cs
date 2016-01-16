using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE
{
    public class AftnService
    {
        #region Fields

        readonly List<Aftn> _AftnList;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public AftnService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _AftnList = LoadAftnList(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<Aftn>> AftnAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the Aftn is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(Aftn aftn)
        {
            if (aftn == null)
                throw new ArgumentNullException("aftn");

            if (!_AftnList.Contains(aftn))
            {
                _AftnList.Add(aftn);
                _dataContext.Aftns.InsertOnSubmit(aftn);
                _dataContext.SubmitChanges();
                if (this.AftnAdded != null)
                    this.AftnAdded(this, new EntityAddedEventArgs<Aftn>(aftn));
            }

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(Aftn aftn)
        {
            _dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(Aftn aftn)
        {

        }



        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<Aftn> GetAftnList()
        {
            return new List<Aftn>(_AftnList);
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<Aftn> LoadAftnList(NotamDataContext dataContext)
        {
            var result = new List<Aftn>();
            if (dataContext != null)
            {
                var aftn = dataContext.Aftns;
                result.AddRange(aftn);
            }
            return result;

        }
        public bool ContainsAftn(Aftn aftn)
        {
            if (aftn == null)
                throw new ArgumentNullException("aftn");

            return _AftnList.Contains(aftn);
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
