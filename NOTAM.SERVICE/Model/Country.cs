using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace NOTAM.Service
{
    [Table(Name = "dbo.NTM_COUNTRY")]
    public class Country : NotamBase, IDataErrorInfo
    {
        #region Fields


        #endregion
        #region Creation

        public static Country CreateNewNotamCodes()
        {
            return new Country();
        }


        public Country()
        {
        }

        #endregion // Creation

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get;
            set;
        }

        [Column(Name = "Code", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
        public string Code
        {
            get;
            set;
        }

        [Column(Name = "Name", DbType = "NVarChar(100) NOT NULL", CanBeNull = true)]
        public string Name
        {
            get;
            set;
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
