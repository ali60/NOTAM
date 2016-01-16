using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE
{
    public class CountryService
    {
        #region Fields

        readonly List<Country> _Country;

        private NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public CountryService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _Country = LoadCountries(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<Country>> CountryAdded;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("Country");

            if (!_Country.Contains(country))
            {
                _Country.Add(country);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(Country country)
        {
            //_dataContext.Origins.OnSubmit(origin);
            //_dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(Country country)
        {

        }



        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<Country> GetCountries()
        {
            return new List<Country>(_Country);
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<Country> LoadCountries(NotamDataContext dataContext)
        {
            var result = new List<Country>();
            if (dataContext != null)
            {
                var countries = dataContext.Countries;
                result.AddRange(countries);
            }
            return result;

        }
        public bool ContainsCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            return _Country.Contains(country);
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
