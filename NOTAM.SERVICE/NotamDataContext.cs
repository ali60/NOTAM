using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;

namespace NOTAM.SERVICE
{
    public  class NotamDataContext: DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();


        public NotamDataContext()
            : base(ConfigurationManager.ConnectionStrings["NOTAMDBConnectionString"].ConnectionString, mappingSource)
		{
			//OnCreated();
		}

        public Table<Origin> Origins
        {
            get
            {
                return this.GetTable<Origin>();
            }
        }

        public Table<FIR> FIRs
        {
            get
            {
                return this.GetTable<FIR>();
            }
        }
        public Table<NotamCode> NotamCodes
        {
            get
            {
                return this.GetTable<NotamCode>();
            }
        }
        public Table<Notam> Notams
        {
            get
            {
                return this.GetTable<Notam>();
            }
        }
        public Table<IntlNotam> IntlNotams
        {
            get
            {
                return this.GetTable<IntlNotam>();
            }
        }

        public Table<NotamArchive> NotamArchives
        {
            get
            {
                return this.GetTable<NotamArchive>();
            }
        }

        public Table<NotamDetail> NotamDetails
        {
            get
            {
                return this.GetTable<NotamDetail>();
            }
        }
        public Table<Aerodom> Aerodoms
        {
            get
            {
                return this.GetTable<Aerodom>();
            }
        }
        public Table<Country> Countries
        {
            get
            {
                return this.GetTable<Country>();
            }
        }

        public Table<Snowtam> Snowtams
        {
            get
            {
                return this.GetTable<Snowtam>();
            }
        }

        public Table<Aftn> Aftns
        {
            get
            {
                return this.GetTable<Aftn>();
            }
        }

        public Table<User> Users
        {
            get
            {
                return this.GetTable<User>();
            }
        }
        public Table<SnowtamDetail> SnowtamDetails
        {
            get
            {
                return this.GetTable<SnowtamDetail>();
            }
        }
    }
}
