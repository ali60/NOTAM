using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE.Model
{

    [Table(Name = "dbo.NTM_INTLNOTAM")]
    public class IntlNotam : NotamBase, IDataErrorInfo
    {

        #region Fields


        #endregion
        #region Creation

        public static IntlNotam CreateNewNotam()
        {
            return new IntlNotam();
        }

        public static IntlNotam CreateNewNNotam()
        {
            return new IntlNotam() { NotamType = "N", Status = "D" };
        }

        public static IntlNotam CreateNewCNotam()
        {
            return new IntlNotam() { NotamType = "C", Status = "D" };
        }

        public static IntlNotam CreateNewRNotam()
        {
            return new IntlNotam() { NotamType = "R", Status = "D" };
        }

        public IntlNotam()
        {

        }

        #endregion // Creation

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Always)]
        public int Id { get; set; }

        private string _type;

        [Column(Name = "Type", DbType = "NVarChar(1)", UpdateCheck = UpdateCheck.Never)]
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Type");
                    _type = value;
                }
            }
        }

        private string _status;

        [Column(Name = "Status", DbType = "NChar(2)", UpdateCheck = UpdateCheck.Never)]
        public string Status
        {
            get
            {
                _status = _status.Trim();
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Status");
                    _status = value;
                }
            }
        }

        private string _number;

        [Required]
        [Column(Name = "Number", DbType = "NVarChar(4) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Number");
                    _number = value;
                }
            }
        }

        private string _year;

        [Column(Name = "Year", DbType = "NVarChar(2) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Year");
                    _year = value;
                }
            }
        }

        private string _notamType;

        [Column(Name = "NotamType", DbType = "NVarChar(1)", UpdateCheck = UpdateCheck.Never)]
        public string NotamType
        {
            get { return _notamType; }
            set
            {
                if (_notamType != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("NotamType");
                    _notamType = value;
                }
            }
        }


        public int RefId { get; set; }

        private string _refType;

        [Column(Name = "RefType", DbType = "NVarChar(1)", UpdateCheck = UpdateCheck.Never)]
        public string RefType
        {
            get { return _refType; }
            set
            {
                if (_refType != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("RefType");
                    _refType = value;
                }
            }
        }

        private string _refNum;

        [Column(Name = "RefNum", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string RefNum
        {
            get { return _refNum; }
            set
            {
                if (_refNum != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("RefNum");
                    _refNum = value;
                }
            }
        }

        private string _refYear;

        [Column(Name = "RefYear", DbType = "NVarChar(2)", UpdateCheck = UpdateCheck.Never)]
        public string RefYear
        {
            get { return _refYear; }
            set
            {
                if (_refYear != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("RefYear");
                    _refYear = value;
                }
            }
        }

        private string _lowerLimit;

        [Column(Name = "LowerLimit", DbType = "NVarChar(3)", UpdateCheck = UpdateCheck.Never)]
        public string LowerLimit
        {
            get { return _lowerLimit; }
            set
            {
                if (_lowerLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("LowerLimit");
                    _lowerLimit = value;
                }
            }
        }

        private string _higherLimit;

        [Column(Name = "HigherLimit", DbType = "NVarChar(3)", UpdateCheck = UpdateCheck.Never)]
        public string HigherLimit
        {
            get { return _higherLimit; }
            set
            {
                if (_higherLimit != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("HigherLimit");
                    _higherLimit = value;
                }
            }
        }

        private string _latitude;

        [Column(Name = "Latitude", DbType = "NVarChar(7)", UpdateCheck = UpdateCheck.Never)]
        public string Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Latitude");
                    _latitude = value;
                }
            }
        }

        private string _longtitude;

        [Column(Name = "Longtitude", DbType = "NVarChar(7)", UpdateCheck = UpdateCheck.Never)]
        public string Longtitude
        {
            get { return _longtitude; }
            set
            {
                if (_longtitude != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Longtitude");
                    _longtitude = value;
                }
            }
        }

        private string _radius;

        [Column(Name = "Radius", DbType = "NVarChar(3)", UpdateCheck = UpdateCheck.Never)]
        public string Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Radius");
                    _radius = value;
                }
            }
        }

        private string _firAero;

        [Column(Name = "FirAero", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string FirAero
        {
            get { return _firAero; }
            set
            {
                if (_firAero != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FirAero");
                    _firAero = value;
                }
            }
        }

        private string _firA2;

        [Column(Name = "FirA2", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string FirA2
        {
            get { return _firA2; }
            set
            {
                if (_firA2 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FirA2");
                    _firA2 = value;
                }
            }
        }

        private string _firA3;

        [Column(Name = "FirA3", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string FirA3
        {
            get { return _firA3; }
            set
            {
                if (_firA3 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FirA3");
                    _firA3 = value;
                }
            }
        }

        private string _firA4;

        [Column(Name = "FirA4", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string FirA4
        {
            get { return _firA4; }
            set
            {
                if (_firA4 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FirA4");
                    _firA4 = value;
                }
            }
        }

        private string _firA5;

        [Column(Name = "FirA5", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string FirA5
        {
            get { return _firA5; }
            set
            {
                if (_firA5 != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FirA5");
                    _firA5 = value;
                }
            }
        }

        private string _fromDate;

        [Column(Name = "FromDate", DbType = "NVarChar(10) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string FromDate
        {
            get { return _fromDate; }
            set
            {
                if (_fromDate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FromDate");
                    _fromDate = value;
                }
            }
        }


        [Column(Name = "FromRDate", DbType = "DateTime NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public DateTime? FromRDate
        {
            get { return FromDate != null ? (DateTime?)DateTime.Now : null; }
            set { if (FromDate == null) FromDate = value.ToString(); }
        }

        private string _toDate;

        [Column(Name = "ToDate", DbType = "NVarChar(10)", UpdateCheck = UpdateCheck.Never)]
        public string ToDate
        {
            get { return _toDate; }
            set
            {
                if (_toDate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ToDate");
                    _toDate = value;
                }
            }
        }

        private string _toRDate;

        [Column(Name = "ToRDate", DbType = "DateTime", UpdateCheck = UpdateCheck.Never)]
        public string ToRDate
        {
            get { return _toRDate; }
            set
            {
                if (_toRDate != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("ToRDate");
                    _toRDate = value;
                }
            }
        }

        private string _permEst;

        [Column(Name = "PermEst", DbType = "NVarChar(4)", UpdateCheck = UpdateCheck.Never)]
        public string PermEst
        {
            get { return _permEst; }
            set
            {
                if (_permEst != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("PermEst");
                    _permEst = value;
                }
            }
        }

        private string _dFreeText;

        [Column(Name = "DfreeTxt", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string DFreeText
        {
            get { return _dFreeText; }
            set
            {
                if (_dFreeText != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("DFreeText");
                    _dFreeText = value;
                }
            }
        }

        private string _eFreeText;

        [Column(Name = "EfreeTxt", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string EFreeText
        {
            get { return _eFreeText; }
            set
            {
                if (_eFreeText != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("EFreeText");
                    _eFreeText = value;
                }
            }
        }

        private string _fFreeText;

        [Column(Name = "FfreeTxt", DbType = "NVarChar(32)", UpdateCheck = UpdateCheck.Never)]
        public string FFreeText
        {
            get { return _fFreeText; }
            set
            {
                if (_fFreeText != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FFreeText");
                    _fFreeText = value;
                }
            }
        }

        private string _gFreeText;

        [Column(Name = "GfreeTxt", DbType = "NVarChar(32)", UpdateCheck = UpdateCheck.Never)]
        public string GFreeText
        {
            get { return _gFreeText; }
            set
            {
                if (_gFreeText != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("GFreeText");
                    _gFreeText = value;
                }
            }
        }

        private string _sendTime;

        [Column(Name = "SendTime", DbType = "NVarChar(12)", UpdateCheck = UpdateCheck.Never)]
        public string SendTime
        {
            get { return _sendTime; }
            set
            {
                if (_sendTime != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("SendTime");
                    _sendTime = value;
                }
            }
        }

        [Column(Name = "Origin_Id", DbType = "Int", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> Origin_Id;

        private EntityRef<Origin> _Origin;


        [System.Data.Linq.Mapping.Association(Name = "NTM_ORIGIN_NTM_NOTAM", Storage = "_Origin", ThisKey = "Origin_Id", OtherKey = "Id", IsForeignKey = true)]
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

                    //this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Origin.Entity = null;
                        //origin doesnot have notam
                        //previousValue.notem.Remove(this);
                    }
                    this._Origin.Entity = value;
                    if ((value != null))
                    {
                        //origin doesnot have notam
                        //value.FIRs.Add(this);
                        this.Origin_Id = value.Id;
                    }
                    else
                    {
                        this.Origin_Id = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Origin");
                }
            }
        }

        [Column(Name = "Fir_Id", DbType = "Int", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> Fir_Id;

        private EntityRef<FIR> _FIR;
        [System.Data.Linq.Mapping.Association(Name = "NTM_FIR_NTM_NOTAM", Storage = "_FIR", ThisKey = "Fir_Id", OtherKey = "Id", IsForeignKey = true)]
        public FIR FIR
        {
            get
            {
                return this._FIR.Entity;
            }
            set
            {
                FIR previousValue = this._FIR.Entity;
                if (((previousValue != value)
                            || (this._FIR.HasLoadedOrAssignedValue == false)))
                {

                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FIR");
                    if ((previousValue != null))
                    {
                        this._FIR.Entity = null;
                        //previousValue.NOTAMs.Remove(this);
                    }
                    this._FIR.Entity = value;
                    if ((value != null))
                    {
                        //value.NOTAMs.Add(this);
                        this.Fir_Id = value.Id;
                    }
                    else
                    {
                        this.Fir_Id = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("FIR");
                }
            }
        }

        [Column(Name = "Code_Id", DbType = "Int", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> Code_Id;

        private EntityRef<NotamCode> _NotamCode;
        [System.Data.Linq.Mapping.Association(Name = "NTM_FIR_NTM_CODES", Storage = "_NotamCode", ThisKey = "Code_Id", OtherKey = "Id", IsForeignKey = true)]
        public NotamCode NotamCode
        {
            get
            {
                return this._NotamCode.Entity;
            }
            set
            {
                NotamCode previousValue = this._NotamCode.Entity;
                if (((previousValue != value)
                            || (this._NotamCode.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("NotamCode");
                    if ((previousValue != null))
                    {
                        this._NotamCode.Entity = null;
                        //previousValue.NotamCodes.Remove(this);
                    }
                    this._NotamCode.Entity = value;
                    if ((value != null))
                    {
                        //value.NotamCodes.Add(this);
                        this.Code_Id = value.Id;
                    }
                    else
                    {
                        this.Code_Id = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("NotamCode");
                }
            }
        }

        public String ArchiveReason { get; set; }

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

        public IntlNotam Clone()
        {
            IntlNotam n = new IntlNotam();
            n.Code_Id = this.Code_Id;
            n.Id = this.Id;
            n.Number = this.Number;
            n.Origin = this.Origin;
            n.DFreeText = this.DFreeText;
            n.EFreeText = this.EFreeText;
            n.FFreeText = this.FFreeText;
            n.FIR = this.FIR;
            n.Fir_Id = this.Fir_Id;
            n.FirA2 = this.FirA2;
            n.FirA3 = this.FirA3;
            n.FirA4 = this.FirA4;
            n.FirA5 = this.FirA5;
            n.FirAero = this.FirAero;
            n.FromDate = this.FromDate;
            n.FromRDate = this.FromRDate;
            n.GFreeText = this.GFreeText;
            n.HigherLimit = this.HigherLimit;
            n.Latitude = this.Latitude;
            n.Longtitude = this.Longtitude;
            n.LowerLimit = this.LowerLimit;
            n.NotamCode = this.NotamCode;
            n.NotamType = this.NotamType;
            n.Origin_Id = this.Origin_Id;
            n.PermEst = this.PermEst;
            n.Radius = this.Radius;
            n.RefId = this.RefId;
            n.RefNum = this.RefNum;
            n.RefType = this.RefType;
            n.RefYear = this.RefType;
            n.SendTime = this.SendTime;
            n.Status = this.Status;
            n.ToDate = this.ToDate;
            n.ToRDate = this.ToRDate;
            n.Type = this.Type;
            n.Year = this.Year;
            return n;

        }


    }
}
