using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE.Model
{
    [Table(Name = "dbo.NTM_ARCHIVE")]
    public class NotamArchive: NotamBase, IDataErrorInfo
    {
        public const string ToDateExpired = "Archived in {0} because to date Expired";
        public const string ReplacedBy = "Archived in {0} because replaced by {1}";
        public const string CancledBy = "Archived in {0} because cancled by {1}";
        public const string ForcedBy = "Archived in {0} because Force archive command";
        public const string IsCancel = "Cancel NOTAM";
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "Reason", DbType = "NVarChar(100)")]
        public string Reason { get; set; }


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
            }

            return error;
        }

        string ValidateProperty()
        {
            return null;

        }

        #endregion // Validation
        }

    
}
