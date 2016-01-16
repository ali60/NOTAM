using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Linq;
using System.Text;
 

namespace NOTAM.Service
{
   [Table(Name = "dbo.NTM_ORIGIN")]
    public class Origin : NotamBase, IDataErrorInfo
    {
        #region Private Fields

        private const string SerialPro = "Serial";
        private const string CenterTypePro = "CenterType";
        private const string DeskTypePro = "DeskType";
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
       private EntitySet<FIR> _FIRs;
       
        #endregion
        #region Creation

        public static Origin CreateNewOrigin()
        {
            return new Origin();
        }

      
        public Origin()
        {
            _FIRs = new EntitySet<FIR>(new Action<FIR>(this.attach_NTM_FIRs), new Action<FIR>(this.detach_NTM_FIRs));
        }

        #endregion // Creation


        #region State Properties

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Always)]
        public  int Id { get; set; }

       private string _code;

       [Column(Name = "Code", DbType = "NVarChar(20) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public  string Code
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
       [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
       public string Name
       {
           get { return _name; }
           set
           {
               if (_name != value)
               {
                   this.SendPropertyChanging();
                   this.SendPropertyChanged("Name");
                   OnPropertyChanged("Name");
                   _name = value;
               }
           }
       }

       private string _serial;

       [Column(Name = "Serial", DbType = "NVarChar(20)", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public string Serial
       {
           get { return _serial; }
           set
           {
               if (_serial != value)
               {
                   this.SendPropertyChanging();
                   this.SendPropertyChanged("Serial");
                   OnPropertyChanged("Serial");
                   _serial = value;
               }

           }
       }

       private string _centerType;

       [Column(Name = "CenterType", DbType = "NVarChar(20)", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public string CenterType
       {
           get { return _centerType; }
           set
           {
               if (_centerType != value)
               {
                   this.SendPropertyChanging();
                   this.SendPropertyChanged("CenterType");
                   OnPropertyChanged("CenterType");
                   _centerType = value;
               }
           }
       }

       private string _deskType;

       [Column(Name = "DeskType", DbType = "NVarChar(50)", CanBeNull = true, UpdateCheck = UpdateCheck.Never  )]
        public string DeskType
       {
           get { return _deskType; }
           set
           {
               if (_deskType  != value)
               {
                   this.SendPropertyChanging();
                   this.SendPropertyChanged("DeskType");
                   OnPropertyChanged("DeskType");
                   _deskType = value;
               }
           }
       }


       [Association(Name = "NTM_ORIGIN_NTM_FIR", Storage = "_FIRs", ThisKey = "Id", OtherKey = "Origin_Id")]
       public EntitySet<FIR> FIRs
       {
           get
           {
               return this._FIRs;
           }
           set
           {
               this._FIRs.Assign(value);
           }
       }

      

       private void attach_NTM_FIRs(FIR  entity)
       {
           this.SendPropertyChanging();
           entity.Origin = this;
       }

       private void detach_NTM_FIRs(FIR  entity)
       {
           this.SendPropertyChanging();
           entity.Origin = null;
       }
        #endregion


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
            SerialPro , 
            CenterTypePro , 
            DeskTypePro ,
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case SerialPro:
                    error = this.ValidateProperty();
                    break;

                case DeskTypePro :
                    error = this.ValidateProperty();
                    break;

                case CenterTypePro:
                    error = this.ValidateProperty();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                    break;
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
          return base.Equals( obj);
       }

        #endregion
    }
}
