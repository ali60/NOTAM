using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using NOTAM.Service;


namespace NOTAM.SERVICE
{
    public class OriginService
    {
        #region Fields

        readonly List<Origin> _origins;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public OriginService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _origins = LoadOrigins(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<Origin>> OriginAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(Origin origin)
        {
            if (origin == null)
                throw new ArgumentNullException("origin");

            if (!_origins.Contains(origin))
            {
                _origins.Add(origin);
                _dataContext.Origins.InsertOnSubmit(origin);
                _dataContext.SubmitChanges();
                if (this.OriginAdded != null)
                    this.OriginAdded(this, new EntityAddedEventArgs<Origin>(origin));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(Origin origin)
        {
           if (origin == null)
                throw new ArgumentNullException("origin");
           _dataContext.SubmitChanges();
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(Origin origin)
        {

        }
     

        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsOrigin(Origin origin)
        {
            if (origin == null)
                throw new ArgumentNullException("origin");

            return _origins.Contains(origin);
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<Origin> GetOrigins()
        {
            return new List<Origin>(_origins);
        }

      

        #endregion // Public Interface

        #region Private Helpers

        static List<Origin> LoadOrigins(NotamDataContext  dataContext)
        {
            var result = new List<Origin>();
            if(dataContext!=null )
            {
                var origins = dataContext.Origins;
                result.AddRange(origins);
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
