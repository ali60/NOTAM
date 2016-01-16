using NOTAM.Service;
using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace NOTAM.SERVICE.Model
{
    [Table(Name = "dbo.NTM_SNOWTAM")]
    public class Snowtam : NotamBase, IDataErrorInfo
    {
        private string _number;
        private string _location;
        private string _obsrvdate;
        private DateTime? _obsrvrdate;
        private string _opgroup;
        private string _obsrvfulldate;
        private DateTime? _obsrvfullrdate;
        [Column(Name = "Origin_Id", DbType = "Int", UpdateCheck = UpdateCheck.Never)]
        public int? Origin_Id;
        private EntityRef<Origin> _Origin;
        [Column(Name = "Country_Id", DbType = "Int", UpdateCheck = UpdateCheck.Never)]
        public int? Country_Id;
        private EntityRef<Country> _Country;
        private string _runway;
        private string _clearedRunwayLen;
        private string _clearedRunwayWidth;
        private string _depositon;
        private string _meanDepth;
        private string _friction;
        private string _criticalSnowbank;
        private string _runwayLight;
        private string _furtherClearance;
        private string _furtherClearanceExp;
        private string _taxiway;
        private string _taxiwaySnowbank;
        private string _obsrvDate2;
        private string _runway2;
        private string _clearedRunwayLen2;
        private string _clearedRunwayWidth2;
        private string _depositon2;
        private string _meanDepth2;
        private string _friction2;
        private string _criticalSnowbank2;
        private string _runwayLight2;
        private string _furtherClearance2;
        private string _furtherClearanceexp2;
        private string _taxiway2;
        private string _taxiwaySnowbank2;
        private string _obsrvDate3;
        private string _runway3;
        private string _clearedRunwayLen3;
        private string _clearedRunwayWidth3;
        private string _depositon3;
        private string _meanDepth3;
        private string _friction3;
        private string _criticalSnowbank3;
        private string _furtherClearance3;
        private string _furtherClearanceexp3;
        private string _taxiway3;
        private string _taxiwaySnowbank3;
        private string _apron;
        private string _nextObsrv;
        private string _freeTextt;
        private string _sendtime;
        private DateTime? _archtime;
        private string _status;
        private string _origin;
        private string _country;
        private string _aerodome;
        private static readonly string[] ValidatedProperties = new string[0];
        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Always)]
        public int Id
        {
            get;
            set;
        }
        [Column(Name = "Number", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string Number
        {
            get
            {
                return this._number;
            }
            set
            {
                if (this._number != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Number");
                    this._number = value;
                }
            }
        }
        [Column(Name = "Location", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                if (this._location != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Location");
                    this._location = value;
                }
            }
        }
        [Column(Name = "Obsrvdate", DbType = "NVarChar(8)", UpdateCheck = UpdateCheck.Never)]
        public string Obsrvdate
        {
            get
            {
                return this._obsrvdate;
            }
            set
            {
                if (this._obsrvdate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Obsrvdate");
                    this._obsrvdate = value;
                }
            }
        }
        [Column(Name = "Obsrvrdate", DbType = "DateTime", UpdateCheck = UpdateCheck.Never)]
        public DateTime? Obsrvrdate
        {
            get
            {
                return this._obsrvrdate;
            }
            set
            {
                if (this._obsrvrdate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Obsrvrdate");
                    this._obsrvrdate = value;
                }
            }
        }
        [Column(Name = "Opgroup", DbType = "NVarChar(3)", UpdateCheck = UpdateCheck.Never)]
        public string Opgroup
        {
            get
            {
                return this._opgroup;
            }
            set
            {
                if (this._opgroup != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Opgroup");
                    this._opgroup = value;
                }
            }
        }
        [Column(Name = "Obsrvfulldate", DbType = "NVarChar(10)", UpdateCheck = UpdateCheck.Never)]
        public string Obsrvfulldate
        {
            get
            {
                return this._obsrvfulldate;
            }
            set
            {
                if (this._obsrvfulldate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Obsrvfulldate");
                    this._obsrvfulldate = value;
                }
            }
        }
        [Column(Name = "Obsrvfullrdate", DbType = "DateTime", UpdateCheck = UpdateCheck.Never)]
        public DateTime? Obsrvfullrdate
        {
            get
            {
                return this._obsrvfullrdate;
            }
            set
            {
                if (this._obsrvfullrdate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Obsrvfullrdate");
                    this._obsrvfullrdate = value;
                }
            }
        }
        [Association(Name = "NTM_ORIGIN_NTM_SNOWTAM", Storage = "_Origin", ThisKey = "Origin_Id", OtherKey = "Id", IsForeignKey = true)]
        public Origin Origin
        {
            get
            {
                return this._Origin.Entity;
            }
            set
            {
                Origin entity = this._Origin.Entity;
                if (entity != value || !this._Origin.HasLoadedOrAssignedValue)
                {
                    if (entity != null)
                    {
                        this._Origin.Entity = null;
                    }
                    this._Origin.Entity = value;
                    if (value != null)
                    {
                        this.Origin_Id = new int?(value.Id);
                    }
                    else
                    {
                        this.Origin_Id = null;
                    }
                }
            }
        }
        [Association(Name = "NTM_COUNTRY_NTM_SNOWTAM", Storage = "_Country", ThisKey = "Country_Id", OtherKey = "Id", IsForeignKey = true)]
        public Country Country
        {
            get
            {
                return this._Country.Entity;
            }
            set
            {
                Country entity = this._Country.Entity;
                if (entity != value || !this._Country.HasLoadedOrAssignedValue)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Country");
                    if (entity != null)
                    {
                        this._Country.Entity = null;
                    }
                    this._Country.Entity = value;
                    if (value != null)
                    {
                        this.Country_Id = new int?(value.Id);
                    }
                    else
                    {
                        this.Country_Id = null;
                    }
                    this.SendPropertyChanged("Country");
                }
            }
        }
        [Column(Name = "Runway", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Runway
        {
            get
            {
                return this._runway;
            }
            set
            {
                if (this._runway != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Runway");
                    this._runway = value;
                }
            }
        }
        [Column(Name = "ClearedRunwayLen", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayLen
        {
            get
            {
                return this._clearedRunwayLen;
            }
            set
            {
                if (this._clearedRunwayLen != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ClearedRunwayLen");
                    this._clearedRunwayLen = value;
                }
            }
        }
        [Column(Name = "ClearedRunwayWidth", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayWidth
        {
            get
            {
                return this._clearedRunwayWidth;
            }
            set
            {
                if (this._clearedRunwayWidth != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ClearedRunwayWidth");
                    this._clearedRunwayWidth = value;
                }
            }
        }
        [Column(Name = "Depositon", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Depositon
        {
            get
            {
                return this._depositon;
            }
            set
            {
                if (this._depositon != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Depositon");
                    this._depositon = value;
                }
            }
        }
        [Column(Name = "MeanDepth", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string MeanDepth
        {
            get
            {
                return this._meanDepth;
            }
            set
            {
                if (this._meanDepth != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("MeanDepth");
                    this._meanDepth = value;
                }
            }
        }
        [Column(Name = "Friction", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Friction
        {
            get
            {
                return this._friction;
            }
            set
            {
                if (this._friction != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Friction");
                    this._friction = value;
                }
            }
        }
        [Column(Name = "CriticalSnowbank", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string CriticalSnowbank
        {
            get
            {
                return this._criticalSnowbank;
            }
            set
            {
                if (this._criticalSnowbank != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("CriticalSnowbank");
                    this._criticalSnowbank = value;
                }
            }
        }
        [Column(Name = "RunwayLight", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string RunwayLight
        {
            get
            {
                return this._runwayLight;
            }
            set
            {
                if (this._runwayLight != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("RunwayLight");
                    this._runwayLight = value;
                }
            }
        }
        [Column(Name = "FurtherClearance", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearance
        {
            get
            {
                return this._furtherClearance;
            }
            set
            {
                if (this._furtherClearance != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FurtherClearance");
                    this._furtherClearance = value;
                }
            }
        }
        [Column(Name = "FurtherClearanceExp", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearanceExp
        {
            get
            {
                return this._furtherClearanceExp;
            }
            set
            {
                if (this._furtherClearanceExp != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FurtherClearanceExp");
                    this._furtherClearanceExp = value;
                }
            }
        }
        [Column(Name = "Taxiway", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Taxiway
        {
            get
            {
                return this._taxiway;
            }
            set
            {
                if (this._taxiway != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Taxiway");
                    this._taxiway = value;
                }
            }
        }
        [Column(Name = "TaxiwaySnowbank", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string TaxiwaySnowbank
        {
            get
            {
                return this._taxiwaySnowbank;
            }
            set
            {
                if (this._taxiwaySnowbank != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("TaxiwaySnowbank");
                    this._taxiwaySnowbank = value;
                }
            }
        }
        [Column(Name = "ObsrvDate2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ObsrvDate2
        {
            get
            {
                return this._obsrvDate2;
            }
            set
            {
                if (this._obsrvDate2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ObsrvDate2");
                    this._obsrvDate2 = value;
                }
            }
        }
        [Column(Name = "Runway2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Runway2
        {
            get
            {
                return this._runway2;
            }
            set
            {
                if (this._runway2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Runway2");
                    this._runway2 = value;
                }
            }
        }
        [Column(Name = "ClearedRunwayLen2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayLen2
        {
            get
            {
                return this._clearedRunwayLen2;
            }
            set
            {
                if (this._clearedRunwayLen2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ClearedRunwayLen2");
                    this._clearedRunwayLen2 = value;
                }
            }
        }
        [Column(Name = "ClearedRunwayWidth2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayWidth2
        {
            get
            {
                return this._clearedRunwayWidth2;
            }
            set
            {
                if (this._clearedRunwayWidth2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ClearedRunwayWidth2");
                    this._clearedRunwayWidth2 = value;
                }
            }
        }
        [Column(Name = "Depositon2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Depositon2
        {
            get
            {
                return this._depositon2;
            }
            set
            {
                if (this._depositon2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Depositon2");
                    this._depositon2 = value;
                }
            }
        }
        [Column(Name = "MeanDepth2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string MeanDepth2
        {
            get
            {
                return this._meanDepth2;
            }
            set
            {
                if (this._meanDepth2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("MeanDepth2");
                    this._meanDepth2 = value;
                }
            }
        }
        [Column(Name = "Friction2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Friction2
        {
            get
            {
                return this._friction2;
            }
            set
            {
                if (this._friction2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Friction2");
                    this._friction2 = value;
                }
            }
        }
        [Column(Name = "CriticalSnowbank2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string CriticalSnowbank2
        {
            get
            {
                return this._criticalSnowbank2;
            }
            set
            {
                if (this._criticalSnowbank2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("CriticalSnowbank2");
                    this._criticalSnowbank2 = value;
                }
            }
        }
        [Column(Name = "RunwayLight2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string RunwayLight2
        {
            get
            {
                return this._runwayLight2;
            }
            set
            {
                if (this._runwayLight2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("RunwayLight2");
                    this._runwayLight2 = value;
                }
            }
        }
        [Column(Name = "FurtherClearance2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearance2
        {
            get
            {
                return this._furtherClearance2;
            }
            set
            {
                if (this._furtherClearance2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FurtherClearance2");
                    this._furtherClearance2 = value;
                }
            }
        }
        [Column(Name = "FurtherClearanceexp2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearanceexp2
        {
            get
            {
                return this._furtherClearanceexp2;
            }
            set
            {
                if (this._furtherClearanceexp2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FurtherClearanceexp2");
                    this._furtherClearanceexp2 = value;
                }
            }
        }
        [Column(Name = "Taxiway2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Taxiway2
        {
            get
            {
                return this._taxiway2;
            }
            set
            {
                if (this._taxiway2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Taxiway2");
                    this._taxiway2 = value;
                }
            }
        }
        [Column(Name = "TaxiwaySnowbank2", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string TaxiwaySnowbank2
        {
            get
            {
                return this._taxiwaySnowbank2;
            }
            set
            {
                if (this._taxiwaySnowbank2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("TaxiwaySnowbank2");
                    this._taxiwaySnowbank2 = value;
                }
            }
        }
        [Column(Name = "ObsrvDate3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ObsrvDate3
        {
            get
            {
                return this._obsrvDate3;
            }
            set
            {
                if (this._obsrvDate3 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ObsrvDate3");
                    this._obsrvDate3 = value;
                }
            }
        }
        [Column(Name = "Runway3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Runway3
        {
            get
            {
                return this._runway3;
            }
            set
            {
                this._runway3 = value;
            }
        }
        [Column(Name = "ClearedRunwayLen3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayLen3
        {
            get
            {
                return this._clearedRunwayLen3;
            }
            set
            {
                this._clearedRunwayLen3 = value;
            }
        }
        [Column(Name = "ClearedRunwayWidth3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string ClearedRunwayWidth3
        {
            get
            {
                return this._clearedRunwayWidth3;
            }
            set
            {
                this._clearedRunwayWidth3 = value;
            }
        }
        [Column(Name = "Depositon3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Depositon3
        {
            get
            {
                return this._depositon3;
            }
            set
            {
                this._depositon3 = value;
            }
        }
        [Column(Name = "MeanDepth3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string MeanDepth3
        {
            get
            {
                return this._meanDepth3;
            }
            set
            {
                this._meanDepth3 = value;
            }
        }
        [Column(Name = "Friction3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Friction3
        {
            get
            {
                return this._friction3;
            }
            set
            {
                this._friction3 = value;
            }
        }
        [Column(Name = "CriticalSnowbank3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string CriticalSnowbank3
        {
            get
            {
                return this._criticalSnowbank3;
            }
            set
            {
                this._criticalSnowbank3 = value;
            }
        }
        [Column(Name = "RunwayLight3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string RunwayLight3
        {
            get;
            set;
        }
        [Column(Name = "FurtherClearance3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearance3
        {
            get
            {
                return this._furtherClearance3;
            }
            set
            {
                this._furtherClearance3 = value;
            }
        }
        [Column(Name = "FurtherClearanceexp3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string FurtherClearanceexp3
        {
            get
            {
                return this._furtherClearanceexp3;
            }
            set
            {
                this._furtherClearanceexp3 = value;
            }
        }
        [Column(Name = "Taxiway3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Taxiway3
        {
            get
            {
                return this._taxiway3;
            }
            set
            {
                this._taxiway3 = value;
            }
        }
        [Column(Name = "TaxiwaySnowbank3", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string TaxiwaySnowbank3
        {
            get
            {
                return this._taxiwaySnowbank3;
            }
            set
            {
                this._taxiwaySnowbank3 = value;
            }
        }
        [Column(Name = "Apron", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string Apron
        {
            get
            {
                return this._apron;
            }
            set
            {
                this._apron = value;
            }
        }
        [Column(Name = "NextObsrv", DbType = "NVarChar(24)", UpdateCheck = UpdateCheck.Never)]
        public string NextObsrv
        {
            get
            {
                return this._nextObsrv;
            }
            set
            {
                this._nextObsrv = value;
            }
        }
        [Column(Name = "FreeTextt", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string FreeTextt
        {
            get
            {
                return this._freeTextt;
            }
            set
            {
                if (this._freeTextt != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FreeTextt");
                    this._freeTextt = value;
                }
            }
        }
        [Column(Name = "Sendtime", DbType = "NVarChar(14)", UpdateCheck = UpdateCheck.Never)]
        public string Sendtime
        {
            get
            {
                return this._sendtime;
            }
            set
            {
                if (this._sendtime != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Sendtime");
                    this._sendtime = value;
                }
            }
        }
        [Column(Name = "Archtime", DbType = "DateTime", UpdateCheck = UpdateCheck.Never)]
        public DateTime? Archtime
        {
            get
            {
                return this._archtime;
            }
            set
            {
                if (this._archtime != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Archtime");
                    this._archtime = value;
                }
            }
        }
        [Column(Name = "Status", DbType = "NChar(10)", UpdateCheck = UpdateCheck.Never)]
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Status");
                    this._status = value;
                }
            }
        }
        [Column(Name = "origin", DbType = "NVarChar(50)", UpdateCheck = UpdateCheck.Never)]
        public string origin
        {
            get
            {
                return this._origin;
            }
            set
            {
                this._origin = value;
            }
        }
        [Column(Name = "country", DbType = "NVarChar(50)", UpdateCheck = UpdateCheck.Never)]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
            }
        }
        [Column(Name = "aerodome", DbType = "NVarChar(50)", UpdateCheck = UpdateCheck.Never)]
        public string aerodome
        {
            get
            {
                return this._aerodome;
            }
            set
            {
                this._aerodome = value;
            }
        }
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }
        public bool IsValid
        {
            get
            {
                string[] validatedProperties = Snowtam.ValidatedProperties;
                bool result;
                for (int i = 0; i < validatedProperties.Length; i++)
                {
                    string propertyName = validatedProperties[i];
                    if (this.GetValidationError(propertyName) != null)
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
                return result;
            }
        }
        public static Snowtam CreateNewSnowtam()
        {
            return new Snowtam();
        }
        private string GetValidationError(string propertyName)
        {
            string result;
            if (Array.IndexOf<string>(Snowtam.ValidatedProperties, propertyName) < 0)
            {
                result = null;
            }
            else
            {
                string text = null;
                result = text;
            }
            return result;
        }
        private string ValidateProperty()
        {
            return null;
        }
    }
}
