using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace NOTAM.ViewModel
{
    public class SnowtamViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        private Snowtam _snowtam;
        private readonly SnowtamService _snowtamService;
        private bool _isSelected;
        private RelayCommand _sendCommand;
        private RelayCommand _viewCommand;
        private RelayCommand _localizeCommand;
        private RelayCommand _detailCommand;
        private RelayCommand _helpCommand;
        private List<Origin> _originOptions;
        private ICommand _handleDoubleClick;
        private string strLastError = "";
        public Snowtam Snowtam
        {
            get
            {
                return _snowtam;
            }
        }
        public string Filling
        {
            get
            {
                return this._snowtam.Sendtime;
            }
            set
            {
                if (!(value == this._snowtam.Sendtime))
                {
                    this._snowtam.Sendtime = value;
                    base.OnPropertyChanged("Filling");
                }
            }
        }
        public Origin Originat
        {
            get
            {
                return this._snowtam.Origin;
            }
            set
            {
                if (value != this._snowtam.Origin && value != null)
                {
                    this._snowtam.Origin = value;
                    base.OnPropertyChanged("Originat");
                }
            }
        }
        public List<Origin> OriginOptions
        {
            get
            {
                if (this._originOptions == null)
                {
                    this._originOptions = this._snowtamService.GetAllOrigins();
                }
                return this._originOptions;
            }
        }
        [Required]
        public string SnowtamNum
        {
            get
            {
                return this._snowtam.Number;
            }
            set
            {
                if (!(value == this._snowtam.Number))
                {
                    this._snowtam.Number = value;
                    base.OnPropertyChanged("SnowtamNum");
                }
            }
        }
        public string Opgroup
        {
            get
            {
                return this._snowtam.Opgroup;
            }
            set
            {
                if (!(value == this._snowtam.Opgroup))
                {
                    this._snowtam.Opgroup = value;
                    base.OnPropertyChanged("Opgroup");
                }
            }
        }
        public string Location
        {
            get
            {
                return this._snowtam.Location;
            }
            set
            {
                if (!(value == this._snowtam.Location))
                {
                    this._snowtam.Location = value;
                    this._snowtam.aerodome = value;
                    base.OnPropertyChanged("Location");
                    base.OnPropertyChanged("Aerodome");
                }
            }
        }
        public string DateTimeT
        {
            get
            {
                return this._snowtam.Obsrvdate;
            }
            set
            {
                if (!(value == this._snowtam.Obsrvdate))
                {
                    this._snowtam.Obsrvdate = value;
                    base.OnPropertyChanged("DateTimeT");
                }
            }
        }
        public string Group
        {
            get
            {
                return this._snowtam.Opgroup;
            }
            set
            {
                if (value == this._snowtam.Opgroup)
                {
                }
            }
        }
        public string Aerodome
        {
            get
            {
                return this._snowtam.aerodome;
            }
            set
            {
                if (!(value == this._snowtam.aerodome))
                {
                    this._snowtam.aerodome = value;
                    if (!string.IsNullOrEmpty(this.Location))
                    {
                        this.Location = value;
                        this.OnPropertyChanged("Location");
                    }
                }
            }
        }

        private Aerodom _aerodom;
        public Aerodom SelectedAerodom
        {
            get
            {
                return _snowtamService.GetAeroItem(_snowtam.aerodome);
            }
            set
            {

                _aerodom = value;
                _snowtam.aerodome= value.Code;
                Location = value.Code;

                base.OnPropertyChanged("SelectedAerodom");
                base.OnPropertyChanged("Location");
            }
        }

        private List<Aerodom> _AerodomOptions=null;
        /// <summary>
        /// Returns a list of strings used to populate the Customer Type selector.
        /// </summary>
        public List<Aerodom> AerodomOptions
        {
            get
            {
                if (_AerodomOptions == null)
                {

                    _AerodomOptions = _snowtamService.GetAllAerodomsForFIRStr("OIIX");
                }
                return _AerodomOptions;
            }
        }
        
        public string DateTimeB
        {
            get
            {
                return this._snowtam.Obsrvfulldate;
            }
            set
            {
                if (!(value == this._snowtam.Obsrvfulldate))
                {
                    this._snowtam.Obsrvfulldate = value;
                }
            }
        }
        public string Runway
        {
            get
            {
                return this._snowtam.Runway;
            }
            set
            {
                if (value != this._snowtam.Runway)
                {
                    this._snowtam.Runway = value;
                    base.OnPropertyChanged("Runway");
                }
            }
        }
        public string RunwayLen
        {
            get
            {
                return this._snowtam.ClearedRunwayLen;
            }
            set
            {
                if (value == this._snowtam.ClearedRunwayLen)
                {
//                    this.FreeTextt = "THE      PART OF " + this._snowtam.ClearedRunwayLen + " UNCLEARED";
                    this.FreeTextt = "THE      PART OF    UNCLEARED";
                }
                else
                {
                    this._snowtam.ClearedRunwayLen = value;
                    base.OnPropertyChanged("RunwayLen");
                }
            }
        }
        public string RunwayWidth
        {
            get
            {
                return this._snowtam.ClearedRunwayWidth;
            }
            set
            {
                if (!(value == this._snowtam.ClearedRunwayWidth))
                {
                    this._snowtam.ClearedRunwayWidth = value;
                    base.OnPropertyChanged("RunwayWidth");
                }
            }
        }
        public string Depositon
        {
            get
            {
                return this._snowtam.Depositon;
            }
            set
            {
                if (!(value == this._snowtam.Depositon))
                {
                    this._snowtam.Depositon = value;
                    base.OnPropertyChanged("Depositon");
                }
            }
        }
        public string Depth
        {
            get
            {
                return this._snowtam.MeanDepth;
            }
            set
            {
                if (!(value == this._snowtam.MeanDepth))
                {
                    this._snowtam.MeanDepth = value;
                    base.OnPropertyChanged("Depth");
                }
            }
        }
        public string Friction
        {
            get
            {
                return this._snowtam.Friction;
            }
            set
            {
                if (!(value == this._snowtam.Friction))
                {
                    this._snowtam.Friction = value;
                    base.OnPropertyChanged("Friction");
                }
            }
        }
        public string SnowBank
        {
            get
            {
                return this._snowtam.CriticalSnowbank;
            }
            set
            {
                if (!(value == this._snowtam.CriticalSnowbank))
                {
                    this._snowtam.CriticalSnowbank = value;
                    base.OnPropertyChanged("SnowBank");
                }
            }
        }
        public string RunAwayLight
        {
            get
            {
                return this._snowtam.RunwayLight;
            }
            set
            {
                if (!(value == this._snowtam.RunwayLight))
                {
                    this._snowtam.RunwayLight = value;
                    base.OnPropertyChanged("RunAwayLight");
                }
            }
        }
        public string Further
        {
            get
            {
                return this._snowtam.FurtherClearance;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearance))
                {
                    this._snowtam.FurtherClearance = value;
                    base.OnPropertyChanged("Further");
                }
            }
        }
        public string FurtherClearance
        {
            get
            {
                return this._snowtam.FurtherClearanceExp;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearanceExp))
                {
                    this._snowtam.FurtherClearanceExp = value;
                    base.OnPropertyChanged("FurtherClearance");
                }
            }
        }
        public string Taxiway
        {
            get
            {
                return this._snowtam.Taxiway;
            }
            set
            {
                if (!(value == this._snowtam.Taxiway))
                {
                    this._snowtam.Taxiway = value;
                    base.OnPropertyChanged("Taxiway");
                }
            }
        }
        public string TaxiwaySnowbank
        {
            get
            {
                return this._snowtam.TaxiwaySnowbank;
            }
            set
            {
                if (!(value == this._snowtam.TaxiwaySnowbank))
                {
                    this._snowtam.TaxiwaySnowbank = value;
                    base.OnPropertyChanged("TaxiwaySnowbank");
                }
            }
        }
        public string ObsrvDate2
        {
            get
            {
                return this._snowtam.ObsrvDate2;
            }
            set
            {
                if (!(value == this._snowtam.ObsrvDate2))
                {
                    this._snowtam.ObsrvDate2 = value;
                    base.OnPropertyChanged("ObsrvDate2");
                }
            }
        }
        public string Runway2
        {
            get
            {
                return this._snowtam.Runway2;
            }
            set
            {
                if (!(value == this._snowtam.Runway2))
                {
                    this._snowtam.Runway2 = value;
                    base.OnPropertyChanged("Runway2");
                }
            }
        }
        public string ClearedRunwayLen2
        {
            get
            {
                return this._snowtam.ClearedRunwayLen2;
            }
            set
            {
                if (!(value == this._snowtam.ClearedRunwayLen2))
                {
                    this._snowtam.ClearedRunwayLen2 = value;
                    base.OnPropertyChanged("ClearedRunwayLen2");
                }
            }
        }
        public string ClearedRunwayWidth2
        {
            get
            {
                return this._snowtam.ClearedRunwayWidth2;
            }
            set
            {
                if (!(value == this._snowtam.ClearedRunwayWidth2))
                {
                    this._snowtam.ClearedRunwayWidth2 = value;
                    base.OnPropertyChanged("ClearedRunwayWidth2");
                }
            }
        }
        public string Depositon2
        {
            get
            {
                return this._snowtam.Depositon2;
            }
            set
            {
                if (!(value == this._snowtam.Depositon2))
                {
                    this._snowtam.Depositon2 = value;
                    base.OnPropertyChanged("Depositon2");
                }
            }
        }
        public string MeanDepth2
        {
            get
            {
                return this._snowtam.MeanDepth2;
            }
            set
            {
                if (!(value == this._snowtam.MeanDepth2))
                {
                    this._snowtam.MeanDepth2 = value;
                    base.OnPropertyChanged("MeanDepth2");
                }
            }
        }
        public string Friction2
        {
            get
            {
                return this._snowtam.Friction2;
            }
            set
            {
                if (!(value == this._snowtam.Friction2))
                {
                    this._snowtam.Friction2 = value;
                    base.OnPropertyChanged("Friction2");
                }
            }
        }
        public string CriticalSnowbank2
        {
            get
            {
                return this._snowtam.CriticalSnowbank2;
            }
            set
            {
                if (!(value == this._snowtam.CriticalSnowbank2))
                {
                    this._snowtam.CriticalSnowbank2 = value;
                    base.OnPropertyChanged("CriticalSnowbank2;");
                }
            }
        }
        public string RunwayLight2
        {
            get
            {
                return this._snowtam.RunwayLight2;
            }
            set
            {
                if (!(value == this._snowtam.RunwayLight2))
                {
                    this._snowtam.RunwayLight2 = value;
                }
            }
        }
        public string FurtherClearance2
        {
            get
            {
                return this._snowtam.FurtherClearance2;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearance2))
                {
                    this._snowtam.FurtherClearance2 = value;
                    base.OnPropertyChanged("FurtherClearance2");
                }
            }
        }
        public string FurtherClearanceexp2
        {
            get
            {
                return this._snowtam.FurtherClearanceexp2;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearanceexp2))
                {
                    this._snowtam.FurtherClearanceexp2 = value;
                    base.OnPropertyChanged("FurtherClearanceexp2");
                }
            }
        }
        public string Taxiway2
        {
            get
            {
                return this._snowtam.Taxiway2;
            }
            set
            {
                if (!(value == this._snowtam.Taxiway2))
                {
                    this._snowtam.Taxiway2 = value;
                    base.OnPropertyChanged("Taxiway2");
                }
            }
        }
        public string TaxiwaySnowbank2
        {
            get
            {
                return this._snowtam.TaxiwaySnowbank2;
            }
            set
            {
                if (!(value == this._snowtam.TaxiwaySnowbank2))
                {
                    this._snowtam.TaxiwaySnowbank2 = value;
                    base.OnPropertyChanged("TaxiwaySnowbank2");
                }
            }
        }
        public string Runway3
        {
            get
            {
                return this._snowtam.Runway3;
            }
            set
            {
                if (!(value == this._snowtam.Runway3))
                {
                    this._snowtam.Runway3 = value;
                    base.OnPropertyChanged("Runway3");
                }
            }
        }
        public string ObsrvDate3
        {
            get
            {
                return this._snowtam.ObsrvDate3;
            }
            set
            {
                if (!(value == this._snowtam.ObsrvDate3))
                {
                    this._snowtam.ObsrvDate3 = value;
                    base.OnPropertyChanged("ObsrvDate3");
                }
            }
        }
        public string ClearedRunwayLen3
        {
            get
            {
                return this._snowtam.ClearedRunwayLen3;
            }
            set
            {
                if (!(value == this._snowtam.ClearedRunwayLen3))
                {
                    this._snowtam.ClearedRunwayLen3 = value;
                    base.OnPropertyChanged("ClearedRunwayLen3");
                }
            }
        }
        public string ClearedRunwayWidth3
        {
            get
            {
                return this._snowtam.ClearedRunwayWidth3;
            }
            set
            {
                if (!(value == this._snowtam.ClearedRunwayWidth3))
                {
                    this._snowtam.ClearedRunwayWidth3 = value;
                    base.OnPropertyChanged("ClearedRunwayWidth3");
                }
            }
        }
        public string Depositon3
        {
            get
            {
                return this._snowtam.Depositon3;
            }
            set
            {
                if (!(value == this._snowtam.Depositon3))
                {
                    this._snowtam.Depositon3 = value;
                    base.OnPropertyChanged("Depositon3");
                }
            }
        }
        public string MeanDepth3
        {
            get
            {
                return this._snowtam.MeanDepth3;
            }
            set
            {
                if (!(value == this._snowtam.MeanDepth3))
                {
                    this._snowtam.MeanDepth3 = value;
                    base.OnPropertyChanged("MeanDepth3");
                }
            }
        }
        public string Friction3
        {
            get
            {
                return this._snowtam.Friction3;
            }
            set
            {
                if (!(value == this._snowtam.Friction3))
                {
                    this._snowtam.Friction3 = value;
                    base.OnPropertyChanged("Friction3");
                }
            }
        }
        public string CriticalSnowbank3
        {
            get
            {
                return this._snowtam.CriticalSnowbank3;
            }
            set
            {
                if (!(value == this._snowtam.CriticalSnowbank3))
                {
                    this._snowtam.CriticalSnowbank3 = value;
                    base.OnPropertyChanged("CriticalSnowbank3");
                }
            }
        }
        public string RunwayLight3
        {
            get
            {
                return this._snowtam.RunwayLight3;
            }
            set
            {
                if (!(value == this._snowtam.RunwayLight3))
                {
                    this._snowtam.RunwayLight3 = value;
                    base.OnPropertyChanged("RunwayLight3;");
                }
            }
        }
        public string FurtherClearance3
        {
            get
            {
                return this._snowtam.FurtherClearance3;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearance3))
                {
                    this._snowtam.FurtherClearance3 = value;
                    base.OnPropertyChanged("FurtherClearance3");
                }
            }
        }
        public string FurtherClearanceexp3
        {
            get
            {
                return this._snowtam.FurtherClearanceexp3;
            }
            set
            {
                if (!(value == this._snowtam.FurtherClearanceexp3))
                {
                    this._snowtam.FurtherClearanceexp3 = value;
                    base.OnPropertyChanged("FurtherClearanceexp3");
                }
            }
        }
        public string Taxiway3
        {
            get
            {
                return this._snowtam.Taxiway3;
            }
            set
            {
                if (!(value == this._snowtam.Taxiway3))
                {
                    this._snowtam.Taxiway3 = value;
                    base.OnPropertyChanged("Taxiway3");
                }
            }
        }
        public string TaxiwaySnowbank3
        {
            get
            {
                return this._snowtam.TaxiwaySnowbank3;
            }
            set
            {
                if (!(value == this._snowtam.TaxiwaySnowbank3))
                {
                    this._snowtam.TaxiwaySnowbank3 = value;
                    base.OnPropertyChanged("TaxiwaySnowbank3");
                }
            }
        }
        public string Apron
        {
            get
            {
                return this._snowtam.Apron;
            }
            set
            {
                if (!(value == this._snowtam.Apron))
                {
                    this._snowtam.Apron = value;
                    base.OnPropertyChanged("Apron");
                }
            }
        }
        public string NextObsrv
        {
            get
            {
                return this._snowtam.NextObsrv;
            }
            set
            {
                if (!(value == this._snowtam.NextObsrv))
                {
                    this._snowtam.NextObsrv = value;
                    base.OnPropertyChanged("NextObsrv");
                }
            }
        }
        public string FreeTextt
        {
            get
            {
                return this._snowtam.FreeTextt;
            }
            set
            {
                if (!(value == this._snowtam.FreeTextt))
                {
                    this._snowtam.FreeTextt = value;
                    base.OnPropertyChanged("FreeTextt");
                }
            }
        }
        public ICommand DetailCommand
        {
            get
            {
                if (this._detailCommand == null)
                {
                    this._detailCommand = new RelayCommand(delegate(object param)
                    {
                        this.ShowDetail();
                    }, (object param) => true);
                }
                return this._detailCommand;
            }
        }
        public override string DisplayName
        {
            get
            {
                string result;
                if (this.IsNewSnowtam)
                {
                    result = Entity.SnowtamViewModel_DisplayName;
                }
                else
                {
                    result = (this._snowtam.Number ?? "NEW SNOWTAM");
                }
                return result;
            }
        }
        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                if (value != this._isSelected)
                {
                    this._isSelected = value;
                    base.OnPropertyChanged("IsSelected");
                }
            }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(_errorMessage))
                    return "Error : " + _errorMessage;
                else
                    return "";
            }
            set
            {
                _errorMessage = value;
                base.OnPropertyChanged("ErrorMessage");
            }
        }
        public new ICommand HandleDoubleClick
        {
            get
            {
                if (this._handleDoubleClick == null)
                {
                    this._handleDoubleClick = new RelayCommand(delegate(object param)
                    {
                        this.ViewText();
                    }, (object param) => true);
                }
                return this._handleDoubleClick;
            }
            set
            {
                this._handleDoubleClick = value;
            }
        }

        public ICommand SendCommand
        {
            get
            {
                if (this._sendCommand == null)
                {
                    this._sendCommand = new RelayCommand(delegate(object param)
                    {
                        this.Send();
                    }, (object param) => (this.CanSave && ValidateType()));
                }
                return this._sendCommand;
            }
        }
        public ICommand LocalizeCommand
        {
            get
            {
                if (this._localizeCommand == null)
                {
                    this._localizeCommand = new RelayCommand(delegate(object param)
                    {
                        this.Localize();
                    }, (object param) => true);
                }
                return this._localizeCommand;
            }
        }
        public ICommand HelpCommand
        {
            get
            {
                if (this._helpCommand == null)
                {
                    this._helpCommand = new RelayCommand(delegate(object param)
                    {
                        this.HelpImg();
                    }, (object param) => true);
                }
                return this._helpCommand;
            }
        }
        private bool IsNewSnowtam
        {
            get
            {
                return !this._snowtamService.ContainsSnowtam(this._snowtam);
            }
        }
        private bool CanSave
        {
            get
            {
                return this.CheckItemC() && !string.IsNullOrEmpty(this.Filling) && !string.IsNullOrEmpty(this.Opgroup) &&this._snowtam.IsValid;
            }
        }
        string IDataErrorInfo.Error
        {
            get
            {
                return ((IDataErrorInfo)this._snowtam).Error;
            }
        }
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string result;
                if (propertyName == "CustomerType")
                {
                    result = this.ValidateCustomerType();
                }
                else
                {
                    result = ((IDataErrorInfo)this._snowtam)[propertyName];
                }
                CommandManager.InvalidateRequerySuggested();
                return result;
            }
        }
        public SnowtamViewModel(Snowtam snowtam, SnowtamService snowtamService, ObservableCollection<WorkspaceViewModel> parent)
            : this(snowtam, snowtamService)
        {
            this.parentWorkSpaces = parent;
        }
        public SnowtamViewModel(Snowtam snowtam, SnowtamService snowtamService)
        {
            if (snowtam == null)
            {
                throw new ArgumentNullException("snowtam");
            }
            if (snowtamService == null)
            {
                throw new ArgumentNullException("snowtamService");
            }
            this._snowtam = snowtam;
            this._snowtamService = snowtamService;
        }
        private void ViewText()
        {
        }
        private bool CheckdateTimeValidity(string StrDateTime)
        {
            bool result;
            if (string.IsNullOrEmpty(StrDateTime))
            {
                result = true;
            }
            else
            {
                if (StrDateTime.Length != 8 && StrDateTime.Length != 10)
                {
                    result = false;
                }
                else
                {
                    try
                    {

                        string s = DateTime.Now.Year.ToString().Substring(2) + StrDateTime;
                        if (StrDateTime.Length == 10)
                            s = StrDateTime;
                        DateTime dateTime = DateTime.ParseExact(s, "yyMMddHHmm", null);
                    }
                    catch (Exception var_2_5C)
                    {
                        result = false;
                        return result;
                    }
                    result = true;
                }
            }
            return result;
        }
        public void Save()
        {
            if (!this._snowtam.IsValid)
            {
                throw new InvalidOperationException(Entity.OriginViewModel_Exception_CannotSave);
            }
            if (this.IsNewSnowtam)
            {
                this._snowtamService.Insert(this._snowtam);
            }
            else
            {
                this._snowtamService.Update(this._snowtam);
            }
            base.OnPropertyChanged("DisplayName");
        }
        public void ShowDetail()
        {
            this.parentWorkSpaces.Add(this);
            base.SetActiveWorkspace(this);
        }
        public void Localize()
        {
            this.Filling = DateTime.Now.ToString("yyMMddHHmm");

            this.Originat = (
                from x in this.OriginOptions
                where x.Code.Equals("OIIIYNYX")
                select x).FirstOrDefault<Origin>();
            this.Opgroup = "OI";
        }
        public void HelpImg()
        {
            try
            {
                Process.Start("C:\\AISADMin\\snowtam.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Send()
        {
            if (!this.ValidateType())
            {
                MessageBox.Show(this.strLastError);
            }
            else
            {
                if (string.IsNullOrEmpty(this.DateTimeT))
                {
                    string text = string.IsNullOrEmpty(this.ObsrvDate2) ? "0" : this.ObsrvDate2;
                    string text2 = string.IsNullOrEmpty(this.ObsrvDate3) ? "0" : this.ObsrvDate3;
                    this.DateTimeT = ((int.Parse(text) > int.Parse(text2)) ? text : text2);
                    if (!string.IsNullOrEmpty(this.DateTimeB))
                    {
                        this.DateTimeT = ((int.Parse(this.DateTimeB) > int.Parse(this.DateTimeT)) ? this.DateTimeB : this.DateTimeT);
                    }
                }
                if (string.IsNullOrEmpty(this._snowtam.Number))
                {
//                    Snowtam lastThisDayeSnowtam = this._snowtamService.GetLastThisDayeSnowtam();
//                    if (lastThisDayeSnowtam == null)
                    {
                     //   if (!this._snowtamService.ContainsSnowtam(this._snowtam))
                       // {
                       //     this._snowtamService.UpdateWithNumber(this._snowtam);
                       // }
                    }
//                     else
//                     {
//                         this._snowtamService.Delete(lastThisDayeSnowtam);
//                         this._snowtamService.Reload();
//                         this._snowtamService.UpdateWithNumber(this._snowtam);
//                     }
                }
                else
                    this._snowtamService.Update(this._snowtam);
                base.OnPropertyChanged("SnowtamNum");
                base.OnPropertyChanged("DisplayName");
                SnowtamDetail snowtam = SnowtamDetail.CreateNewNotamDetail(this._snowtam);
                SnowtamDetailService snowtamDetailService = new SnowtamDetailService(this._snowtamService._dataContext);
                SnowtamDetail byNotamId = snowtamDetailService.GetByNotamId(this._snowtam.Id);
                if (byNotamId != null)
                {
                    snowtam = byNotamId;
                }
                SnowtamDetailViewModel snowtamDetailViewModel = new SnowtamDetailViewModel(snowtam, _snowtamService, snowtamDetailService, this.parentWorkSpaces, this);
                this.parentWorkSpaces.Add(snowtamDetailViewModel);
                base.SetActiveWorkspace(snowtamDetailViewModel);
            }
        }
        private bool CheckRunway(string str)
        {
            bool result;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length != 3 && str.Length != 2)
                {
                    this.strLastError = "RUNWAY Must be 3 or 2 characters";
                    result = false;
                    return result;
                }
                int iRunwayDigit = Int32.Parse(str.Substring(0, 2)); 
                if (iRunwayDigit<1 || iRunwayDigit>18)
                {
                    this.strLastError = "RUNWAY Must begin with 2 digits in 1-18 range";
                    result = false;
                    return result;
                }
                if (str.Length == 3 && (str[2] != 'L' && str[2] != 'R') )
                {
                    this.strLastError = "RUNWAY third character must be L/R";
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }
        private bool CheckItemFGH(string str)
        {
            bool result;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Count((char x) => x == '/') != 2)
                {
                    this.strLastError = "Item F,G,H must containt 2 /";
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }

        private bool CheckItemML(string strM,string strL)
        {
            bool result = true; ;
            if (!string.IsNullOrEmpty(strM) && string.IsNullOrEmpty(strL))
            {
                this.strLastError = "Item L is Empty";
                result = false;
            }
            if (string.IsNullOrEmpty(strM) && !string.IsNullOrEmpty(strL))
            {
                this.strLastError = "Item M is Empty";
                result = false;
            }
            return result;
        }
        
        private bool CheckItemK(string str)
        {
            bool result;
            if (!string.IsNullOrEmpty(str))
            {
                if (str != "YESL" && str != "YESR" && str!="YESLR")
                {
                    this.strLastError = "Invalid Item K,must be YESL or YESR or YESLR or empty";
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }
        private bool ValidateType()
        {
            bool result;
            if (string.IsNullOrEmpty(this.Filling) || string.IsNullOrEmpty(this.Opgroup))
            {
                result = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.DateTimeB) && !this.CheckdateTimeValidity(this.DateTimeB))
                {
                    this.strLastError = "Invalid Date and Time ";
                    result = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ObsrvDate2) && !this.CheckdateTimeValidity(this.ObsrvDate2))
                    {
                        this.strLastError = "Invalid Date and Time 2 ";
                        result = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.ObsrvDate3) && !this.CheckdateTimeValidity(this.ObsrvDate3))
                        {
                            this.strLastError = "Invalid Date and Time 3";
                            result = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.NextObsrv) && !this.CheckdateTimeValidity(DateTime.Now.Year.ToString().Substring(2) + this.NextObsrv))
                            {
                                this.strLastError = "Invalid Item S , format is MMddhhmm ";
                                result = false;
                            }
                            else
                            {
                                if (!this.CheckRunway(this.Runway) || !this.CheckRunway(this.Runway2) || !this.CheckRunway(this.Runway3))
                                {
                                    result = false;
                                }
                                else
                                {
                                    if (!this.CheckItemFGH(this.Depositon) || !this.CheckItemFGH(this.Depositon2) || !this.CheckItemFGH(this.Depositon3))
                                    {
                                        result = false;
                                    }
                                    else
                                    {
                                        if (!this.CheckItemFGH(this.MeanDepth2) || !this.CheckItemFGH(this.MeanDepth3))
                                        {
                                            result = false;
                                        }
                                        else
                                        {
                                            if (!this.CheckItemFGH(this.Friction2) || !this.CheckItemFGH(this.Friction3) || !this.CheckItemFGH(this.Friction))
                                            {
                                                result = false;
                                            }
                                            else
                                            {
                                                if (!CheckItemML(this.FurtherClearanceexp2, this.FurtherClearance2)
                                                    || !CheckItemML(this.FurtherClearanceexp3, this.FurtherClearance3)
                                                    || !CheckItemML(this.FurtherClearance, this.Further)
                                                    )
                                                {
                                                    result = false;
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(this.FurtherClearance2) && string.IsNullOrEmpty(this.FurtherClearance2))
                                                    {
                                                        this.strLastError = "Item M is Empty";
                                                        result = false;
                                                    }
                                                    else
                                                    {
                                                        if (!this.CheckItemK(this.RunwayLight2) || !this.CheckItemK(this.RunwayLight3))
                                                        {
                                                            result = false;
                                                        }
                                                        else
                                                        {
                                                            if (string.IsNullOrEmpty(DateTimeB) && string.IsNullOrEmpty(ObsrvDate2) && string.IsNullOrEmpty(ObsrvDate3))
                                                            {
                                                                this.strLastError = "Item B is Empty";
                                                                result = false;
                                                            }
                                                            else
                                                            {

                                                                if (string.IsNullOrEmpty(Runway) && string.IsNullOrEmpty(Runway2) && string.IsNullOrEmpty(Runway3))
                                                                {
                                                                    this.strLastError = "Item C is Empty";
                                                                    result = false;
                                                                }
                                                                else
                                                                {
                                                                    this.strLastError = "";
                                                                    result = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (ErrorMessage != strLastError)
                ErrorMessage = strLastError;
            return result;
        }
        private bool CheckItemC()
        {
            return !string.IsNullOrEmpty(this.Runway) || !string.IsNullOrEmpty(this.Runway2) || !string.IsNullOrEmpty(this.Runway3);
        }
        private string ValidateCustomerType()
        {
            return null;
        }
    }
}
