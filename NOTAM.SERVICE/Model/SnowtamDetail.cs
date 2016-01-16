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


    [Table(Name = "dbo.NTM_SNOWTAM_DETAIL")]
    public class SnowtamDetail : NotamBase, IDataErrorInfo
    {

        #region Creation

        public static SnowtamDetail CreateNewNotamDetail(Snowtam snowtam)
        {
            return new SnowtamDetail(snowtam);
        }


        public SnowtamDetail()
        {

        }

        public SnowtamDetail(Snowtam snowtam)
            : this()
        {
            Snowtam = snowtam;

        }


        #endregion // Creation

        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }


        private string _descriptionAFTN;

        [Column(Name = "DescriptionAFTN", DbType = "NVarChar(255)")]
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
        [Column(Name = "DescriptionRemark", DbType = "NVarChar(255)")]
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
                if (_fileUrl != value)
                {
                    this.SendPropertyChanging();
                    this.SendPropertyChanged("FileUrl");
                    _fileUrl = value;
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

        [Column(Name = "Snowtam_Id", DbType = "Int")]
        public System.Nullable<int> Snowtam_Id;

        private EntityRef<Snowtam> _Snowtam;


        [System.Data.Linq.Mapping.Association(Name = "NTM_SNOWTAM_DETAIL_NTM_SNOWTAM", Storage = "_Snowtam", ThisKey = "Snowtam_Id", OtherKey = "Id", IsForeignKey = true)]
        public Snowtam Snowtam
        {
            get
            {
                return this._Snowtam.Entity;
            }
            set
            {
                Snowtam previousValue = this._Snowtam.Entity;
                if (((previousValue != value)
                            || (this._Snowtam.HasLoadedOrAssignedValue == false)))
                {
                    //this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Snowtam.Entity = null;
                        //origin does not have notam
                        //previousValue.notem.Remove(this);
                    }
                    this._Snowtam.Entity = value;
                    if ((value != null))
                    {
                        //origin does not have notam
                        //value.FIRs.Add(this);
                        this.Snowtam_Id = value.Id;
                    }
                    else
                    {
                        this.Snowtam_Id = default(Nullable<int>);
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
