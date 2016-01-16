using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace NOTAM.Service
{
    [Table(Name = "dbo.NTM_DISTRIBUTIONS")]
    public class Aftn : NotamBase, IDataErrorInfo
    {
        #region Fields


        #endregion
        #region Creation

        public static Aftn CreateNewAftn()
        {
            return new Aftn();
        }


        public Aftn()
        {
        }

        #endregion // Creation

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get;
            set;
        }

        private string _series;

        [Column(Name = "Series", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string Series
        {
            get { return _series; }
            set
            {
                if (_series != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("Series");
                    _series = value;
                }
            }
        }

        private string _aftnList;

        [Column(Name = "AftnList", DbType = "Text NOT NULL", CanBeNull = true)]
       public string AftnList
        {
            get { return _aftnList; }
            set
            {
                if (_aftnList != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("AftnList");
                    _aftnList = value;
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }



        #endregion



    }
}
