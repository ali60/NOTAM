using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE
{
    public class FIRService
    {
         #region Fields

        readonly List<FIR> _FIRs;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public FIRService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _FIRs = LoadFIRs(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<FIR>> FIRAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(FIR fir)
        {
            if (fir == null)
                throw new ArgumentNullException("fir");

            if (!_FIRs.Contains(fir))
            {
                _FIRs.Add(fir);
                _dataContext.FIRs.InsertOnSubmit(fir);
                _dataContext.SubmitChanges();
                if (this.FIRAdded != null)
                    this.FIRAdded(this, new EntityAddedEventArgs<FIR>(fir));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(FIR fir)
        {
            if (fir == null)
                throw new ArgumentNullException("fir");
            _dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(FIR fir)
        {

        }
     

        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsFIR(FIR fir)
        {
            if (fir == null)
                throw new ArgumentNullException("fir");

            return _FIRs.Contains(fir);
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<FIR> GetFIRs()
        {
            return new List<FIR>(_FIRs).OrderBy(x => x.Code).ToList();
        }

        public List<Origin> GetAllOrigins()
        {
            var originService = new OriginService(_dataContext);
            return originService.GetOrigins(); 
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<FIR> LoadFIRs(NotamDataContext  dataContext)
        {
            var result = new List<FIR>();
            if(dataContext!=null )
            {
                var firs = dataContext.FIRs;
                result.AddRange(firs);
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
