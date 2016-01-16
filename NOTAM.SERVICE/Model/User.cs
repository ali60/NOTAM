using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using NOTAM.Service;

namespace NOTAM.SERVICE
{

    public enum UserRole
    {
        Anonymous=0,
        Administrator=1,
        User=2
    }

    [Table(Name = "dbo.NTM_USERS")]
    public class User: NotamBase
    {
        public User(string username, UserRole  role):base()
        {
            Username = username;
            Role = role;
        }

        public User()
        {
            
        }

        #region Creation

        public static User CreateNewUser()
        {
            return new User();
        }

      #endregion // Creation


        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Username", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
        public string Username
        {
            get;
            set;
        }


        [Column(Name = "Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string HashPassword
        {
            get;
            set;
        }


        public string Password
        {
            get;
            set;
        }
        [Column(Name = "Role", DbType = "SmallInt", CanBeNull = true)]
        public UserRole Role
        {
            get;
            set;
        }

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
            
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

           

            return error;
        }
        #endregion
    }
}
