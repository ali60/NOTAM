using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;

namespace NOTAM.SERVICE
{
    public class AerodomService
    {
        #region Fields

        readonly List<Aerodom> _Aerodom;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public AerodomService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _Aerodom = LoadAerodoms(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<Aerodom>> AerodomAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(Aerodom aerodom)
        {
            if (aerodom == null)
                throw new ArgumentNullException("aerodom");

            if (!_Aerodom.Contains(aerodom))
            {
                _Aerodom.Add(aerodom);
                _dataContext.Aerodoms.InsertOnSubmit(aerodom);
                _dataContext.SubmitChanges();
                if (this.AerodomAdded != null)
                    this.AerodomAdded(this, new EntityAddedEventArgs<Aerodom>(aerodom));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(Aerodom aerodom)
        {
            if (aerodom == null)
                throw new ArgumentNullException("aerodom");
            _dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(Aerodom aerodom)
        {

        }


        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsAerodom(Aerodom aerodom)
        {
            if (aerodom == null)
                throw new ArgumentNullException("aerodom");

            return _Aerodom.Contains(aerodom);
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<Aerodom> GetAerodoms()
        {
            return new List<Aerodom>(_Aerodom);
        }

        public List<Country> GetAllCountries()
        {
            var countryService = new CountryService(_dataContext);
            return countryService.GetCountries();
        }
        public List<FIR> GetAllFIRs()
        {
            var firService = new FIRService(_dataContext);
            return firService.GetFIRs();
        }

        public Dictionary<string, string> GetAddressList(List<string> AeroList)
        {
            var firService = new FIRService(_dataContext);
            Dictionary<string, string> retDic = new Dictionary<string, string>();
            List<Aerodom> AerodomRecords = GetAerodoms();
            foreach (string aeroItem in AeroList)
            {
                Aerodom result = AerodomRecords.Find(x => x.Code == aeroItem);
                if (result != null)
                    if (!retDic.ContainsKey(aeroItem))
                        retDic.Add(aeroItem, result.Address);
            }
            return retDic;
        }
        public Aerodom FindAeroByName(string strFirAero)
        {
            Aerodom result = _Aerodom.Find(x => x.Code == strFirAero);
            return result;
        }


        #endregion // Public Interface

        #region Private Helpers

        static List<Aerodom> LoadAerodoms(NotamDataContext dataContext)
        {
            var result = new List<Aerodom>();
            if (dataContext != null)
            {
                var aerodom = dataContext.Aerodoms;
                result.AddRange(aerodom);
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
