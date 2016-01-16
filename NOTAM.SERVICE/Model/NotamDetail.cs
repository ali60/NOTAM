using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using NOTAM.Service;

namespace NOTAM.SERVICE.Model
{


     [Table(Name = "dbo.NTM_DETAIL")]
    public class NotamDetail: NotamBase,IDataErrorInfo
    {

                #region Creation

        public static NotamDetail CreateNewNotamDetail(Notam notam)
        {
            return new NotamDetail(notam );
        }


        public NotamDetail()
        {
            
        }

        public NotamDetail(Notam notam):this()
        {
            Notam = notam;

        }


        #endregion // Creation

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }


         private string _descriptionAFTN;

         [Column(Name = "DescriptionAFTN", DbType = "Text")]
        public string DescriptionAFTN
         {
             get { return _descriptionAFTN; }
             set
             {
                 if (_descriptionAFTN != value)
                 {
                     this.SendPropertyChanging();
                     this.SendPropertyChanged("DescriptionAFTN");
                     _descriptionAFTN = value;
                 }
             }
         }
         private string _descriptionRemark;
         [Column(Name = "DescriptionRemark", DbType = "Text")]
         public string DescriptionRemark
         {
             get { return _descriptionRemark; }
             set
             {
                 if (_descriptionRemark != value)
                 {
                     this.SendPropertyChanging();
                     this.SendPropertyChanged("DescriptionRemark");
                     _descriptionRemark = value;
                 }
             }
         }

         private string _fileUrl;

         [Column(Name = "FilePath", DbType = "NVarChar(255)")]
        public string FileUrl
         {
             get { return _fileUrl; }
             set
             {
                 if (_fileUrl  != value)
                 {
                     this.SendPropertyChanging();
                     this.SendPropertyChanged("FileUrl");
                     _fileUrl  = value;
                 }
             }
         }

         private Int16? _source;
         [Column(Name = "Source", DbType = "smallint")]
         public Int16? Source
         {
             get { return _source; }
             set
             {
                 if (_source != value)
                 {
                     this.SendPropertyChanging();
                     this.SendPropertyChanged("Source");
                     _source = value;
                 }
             }
         }

         private string _User;

         [Column(Name = "User", DbType = "NVarChar(10)")]
         public string User
         {
             get { return _User; }
             set
             {
                 if (_User != value)
                 {
                     this.SendPropertyChanging();
                     this.SendPropertyChanged("User");
                     _User = value;
                 }
             }
         }
         private string _type;


         [Column(Name = "Notam_Id", DbType = "Int")]
        public System.Nullable<int> Notam_Id;

        private EntityRef<Notam> _Notam;


        [System.Data.Linq.Mapping.Association(Name = "NTM_DETAIL_NTM_NOTAM", Storage = "_Notam", ThisKey = "Notam_Id", OtherKey = "Id", IsForeignKey = true)]
        public Notam Notam
        {
            get
            {
                return this._Notam.Entity;
            }
            set
            {
                Notam previousValue = this._Notam.Entity;
                if (((previousValue != value)
                            || (this._Notam.HasLoadedOrAssignedValue == false)))
                {
                    //this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Notam.Entity = null;
                        //origin doesnot have notam
                        //previousValue.notem.Remove(this);
                    }
                    this._Notam.Entity = value;
                    if ((value != null))
                    {
                        //origin doesnot have notam
                        //value.FIRs.Add(this);
                        this.Notam_Id = value.Id;
                    }
                    else
                    {
                        this.Notam_Id = default(Nullable<int>);
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


        #endregion // IDataErrorInfo Members
    }
}
