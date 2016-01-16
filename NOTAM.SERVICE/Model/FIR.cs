using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
namespace NOTAM.Service
{
    [Table(Name = "dbo.NTM_FIR")]
    public class FIR: CodeBase , IDataErrorInfo
    {
        #region Fields
        

        #endregion
         #region Creation

        public static FIR CreateNewFIR()
        {
            return new FIR();
        }


        public FIR()
        {
            this._Origin = default(EntityRef<Origin>);
        }

        #endregion // Creation

         [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get;
            set;
        }

        private string _code;

        [Column(Name = "Code", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
        public override string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Code");
                    _code = value;
                }
            }
        }

        private string _name;

        [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Name");
                    _name = value;
                }
            }
        }

        private string _northLimit;

        [Column(Name = "NorthLimit", DbType = "VarChar(4)")]
        public string NorthLimit
        {
            get { return _northLimit; }
            set
            {
                if (_northLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("NorthLimit");
                    _northLimit = value;
                }
            }
        }

        private string _southLimit;

        [Column(Name = "SouthLimit", DbType = "VarChar(4)")]
        public string SouthLimit
        {
            get { return _southLimit; }
            set
            {
                if (_southLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("SouthLimit");
                    _southLimit = value;
                }
            }
        }

        private string _westLimit;

        [Column(Name = "WestLimit", DbType = "VarChar(4)")]
        public string WestLimit
        {
            get { return _westLimit; }
            set
            {
                if (_westLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("WestLimit");
                    _westLimit = value;
                }
            }
        }

        private string _eastLimit;

        [Column(Name = "EastLimit", DbType = "VarChar(4)")]
        public string EastLimit
        {
            get { return _eastLimit; }
            set
            {
                if (_eastLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("EastLimit");
                    _eastLimit = value;
                }
            }
        }

        private string _atcCode;

        [Column(Name = "Code_atc", DbType = "VarChar(8)")]
        public string AtcCode
        {
            get { return _atcCode; }
            set
            {
                if (_atcCode != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("AtcCode");
                    _atcCode = value;
                }
            }
        }

        [Column(Name = "Origin_Id", DbType = "Int")]
        public Nullable<int> Origin_Id;


        //[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "NTM_ORIGIN_NTM_FIR", Storage = "_NTM_ORIGIN", ThisKey = "Origin_Id", OtherKey = "Id", IsForeignKey = true)]
        
        private EntityRef<Origin> _Origin;

        [Association(Name = "NTM_ORIGIN_NTM_FIR",Storage = "_Origin", ThisKey = "Origin_Id", OtherKey = "Id", IsForeignKey = true)]
        public Origin Origin
        {
            get
            {
                return this._Origin.Entity;
            }
            set
            {
                Origin previousValue = this._Origin.Entity;
                if (((previousValue != value)
                            || (this._Origin.HasLoadedOrAssignedValue == false)))
                {
                   
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Origin");
                    
                    if ((previousValue != null))
                    {
                        this._Origin.Entity = null;
                        previousValue.FIRs.Remove(this);
                    }
                    this._Origin.Entity = value;
                    if ((value != null))
                    {
                        value.FIRs .Add(this);
                        this.Origin_Id = value.Id;
                    }
                    else
                    {
                        this.Origin_Id = default(Nullable<int>);
                    }
                    //this.SendPropertyChanged("NTM_ORIGIN");
                }
            }
        }


        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion // IDataErrorInfo Members

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties = 
        { 
            //todo: add properties name
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                //case SerialPro:
                //    error = this.ValidateProperty();
                //    break;

                //case DeskTypePro:
                //    error = this.ValidateProperty();
                //    break;

                //case CenterTypePro:
                //    error = this.ValidateProperty();
                //    break;

                //default:
                //    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                //    break;
            }

            return error;
        }

        string ValidateProperty()
        {
            return null;

        }






        #endregion // Validation

        #region Object Members
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Code))
                return Code;
            return "Code";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion

       

       
        
    }
}
