using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Documents;

namespace NOTAM.ViewModel
{
    public class SnowtamDetailViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        SnowtamDetail _snowtam;
        readonly SnowtamDetailService _snowtamDetailService;
        readonly SnowtamService _snowtamService;

        RelayCommand _saveCommand;
        RelayCommand _sendCommand;
        RelayCommand _printCommand;
        RelayCommand _openFileCommand;
        RelayCommand _sendDefaultCommand;
        RelayCommand _removeAftnCommand;
        RelayCommand _addAftnCommand;
        RelayCommand _addManualAftnCommand;
        RelayCommand _delManualAftnCommand;
        private String _notamNum;
        private String openPar = "(";
        private String closePar = ")";
        private String slash = "/";
        private String nullChar = "\0";
        WorkspaceViewModel NotamWorkspace;

        #endregion // Fields
        #region Constructor

        public SnowtamDetailViewModel(SnowtamDetail snowtam,SnowtamService snowtamService, SnowtamDetailService snowtamDetailService, ObservableCollection<WorkspaceViewModel> parent,WorkspaceViewModel ntmWorkspace)
        {
            parentWorkSpaces = parent;
            NotamWorkspace = ntmWorkspace;
            if (snowtam == null)
                throw new ArgumentNullException("notam");

            if (snowtamService == null)
                throw new ArgumentNullException("snowtamService");

            _snowtam = snowtam;
            _snowtamDetailService = snowtamDetailService;
            _snowtamService = snowtamService;


            this.validators = this.GetType()
               .GetProperties()
               .Where(p => this.GetValidations(p).Length != 0)
               .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }

        #endregion // Constructor

        #region Display Properties



        private String _DisplayName;
        public override string DisplayName
        {
            get
            {
                _DisplayName = _snowtam.Snowtam != null ? _snowtam.Snowtam.Number : Entity.NotamDetailViewModel_DisplayName;
                return _DisplayName + " Send Info";
            }
        }

        #endregion
        #region  Properties
        //public string NotamNum
        //{
        //    get
        //    {
        //        _notamNum = _snowtam.Snowtam!=null ? _snowtam.Snowtam.Number : Entity.NotamDetailViewModel_DisplayName;
        //        return _notamNum;
        //    }
        //    set { _notamNum = value; }

        //}


        public string DescriptionRemark
        {
            get { return _snowtam.DescriptionRemark; }
            set
            {
                if (value == _snowtam.DescriptionRemark)
                    return;
                if (!CanSet)
                    return;

                _snowtam.DescriptionRemark = value;

                base.OnPropertyChanged("DescriptionRemark");
            }
        }
        public string DescriptionAFTN
        {
            get { return _snowtam.DescriptionAFTN; }
            set
            {
                if (value == _snowtam.DescriptionAFTN)
                    return;
                if (!CanSet)
                    return;

                _snowtam.DescriptionAFTN = value;

                base.OnPropertyChanged("DescriptionAFTN");
            }
        }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "length must be")]
        public string FilePath
        {
            get { return _snowtam.FileUrl; }
            set
            {
                if (value == _snowtam.FileUrl)
                    return;

                if (!CanSet)
                    return;
                _snowtam.FileUrl = value;

                base.OnPropertyChanged("FilePath");
            }
        }


        [StringLength(3, MinimumLength = 3, ErrorMessage = "length must be 3 Chars")]
        public string User
        {
            get { return _snowtam.User; }
            set
            {
                if (!CanSet)
                    return;
                if (value == _snowtam.User)
                    return;

                _snowtam.User = value;

                base.OnPropertyChanged("User");
            }
        }
        private bool _IsAftnMsg = false;
        public bool IsAftnMsg
        {
            get
            {
                if (_snowtam.Source == 3 || _snowtam.Source == 2)
                    _IsAftnMsg = true;
                return _IsAftnMsg;
            }
            set
            {
                if (!CanSet)
                    return;
                _IsAftnMsg = value;
                _snowtam.Source = 0;
                if (_IsRemark)
                    _snowtam.Source = 1;
                if (_IsAftnMsg)
                    _snowtam.Source = 2;
                if (_IsAftnMsg && _IsRemark)
                    _snowtam.Source = 3;

                base.OnPropertyChanged("IsAftnMsg");
                base.OnPropertyChanged("IsRemark");
            }
        }

        private bool _IsRemark = false;
        public bool IsRemark
        {
            get
            {
                if (_snowtam.Source == 3 || _snowtam.Source == 1)
                    _IsRemark = true;
                return _IsRemark;
            }
            set
            {
                if (!CanSet)
                    return;
                _IsRemark = value;
                _snowtam.Source = 0;
                if (_IsRemark)
                    _snowtam.Source = 1;
                if (_IsAftnMsg)
                    _snowtam.Source = 2;
                if (_IsAftnMsg && _IsRemark)
                    _snowtam.Source = 3;


                base.OnPropertyChanged("IsRemark");
                base.OnPropertyChanged("IsAftnMsg");
            }
        }

        private string _notamText;

        public string NotamText
        {
            get
            {
                if (string.IsNullOrEmpty(_notamText))
                {
                    _notamText =  NotamSender.GeneratePreviewSnowtamText(_snowtam.Snowtam);
                }
                return _notamText;
            }
            set { _notamText = value; }
        }

        private List<Aftn> _aftnOptions;
        /// <summary>
        /// Returns a list of Aftns for selection
        /// </summary>
        public List<Aftn> AftnOptions
        {
            get
            {
                if (_aftnOptions == null)
                {
                    _aftnOptions = _snowtamDetailService.GetAllAftns();
                }
                return _aftnOptions;
            }
        }


        private ObservableCollection<Aftn> _selectedAftns;

        /// <summary>
        /// Returns a list of Aftns that are selected
        /// </summary>
        public ObservableCollection<Aftn> SelectedAftns
        {
            get { return _selectedAftns; }
            set
            {
                _selectedAftns = value;
                base.OnPropertyChanged("SelectedAftns");

            }
        }


        private ObservableCollection<String> _manualAftns;

        /// <summary>
        /// Returns a list of manual Aftns that are selected
        /// </summary>
        public ObservableCollection<String> ManualAftns
        {
            get { return _manualAftns; }
            set
            {
                _manualAftns = value;
                base.OnPropertyChanged("ManualAftns");

            }
        }

         private List<string> _removedManualAftns;

         /// <summary>
         /// current selected Aftn
         /// </summary>
         public List<string> RemovedManualAftns
         {
             get { return _removedManualAftns; }
             set
             {
                 _removedManualAftns = value;
                 base.OnPropertyChanged("RemovedManualAftns");
             }
         }
        private String _manualAftn;

        /// <summary>
        /// Returns a list of Aftns that are selected
        /// </summary>
        public String ManualAftn
        {
            get { return _manualAftn; }
            set
            {
                _manualAftn = value;
                base.OnPropertyChanged("ManualAftn");

            }
        }


        private Aftn _selectedAftn;

        /// <summary>
        /// current selected Aftn
        /// </summary>
        public Aftn SelectedAftn
        {
            get { return _selectedAftn; }
            set
            {
                _selectedAftn = value;
                base.OnPropertyChanged("SelectedAftn");
            }
        }



        private IList<Aftn> _removedAftns;

        /// <summary>
        /// current selected Aftn
        /// </summary>
          public IList<Aftn> RemovedAftns
        {
              get { return _removedAftns; }
            set
            {
                  _removedAftns = value;
                  base.OnPropertyChanged("RemovedAftns");
            }
        }
        // <summary>
        /// Returns a command that saves the Notam.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => CanSave 
                        );
                }
                return _saveCommand;
            }
        }
        bool CanSave
        {
            get
            {
                return !string.IsNullOrEmpty(this.User) && this.User.Length == 3;
            }
        }
        bool CanSendDefault
        {
            get
            {
                return !string.IsNullOrEmpty(GenerateDefaultAFTNHeader());
            }
        }
        bool CanSet
        {
            get
            {
                return  !_FreeStyle;
            } 
        }
        bool _FreeStyle = false;
        public bool FreeStyle
        {
            set { _FreeStyle = value; }
            get
            {
                return _FreeStyle;
            }
        }

        // <summary>
        /// Returns a command that saves the Notam.
        /// </summary>
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(
                        param => this.Send(),
                        param => this.CanSave && !this.IsNewNotam
                        );
                }
                return _sendCommand;
            }
        }

        public ICommand PrintCommand
        {
            get
            {
                if (_printCommand == null)
                {
                    _printCommand = new RelayCommand(
                        param => this.Print(),
                        param => true
                        );
                }
                return _printCommand;
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                if (_openFileCommand == null)
                {
                    _openFileCommand = new RelayCommand(
                        param => this.OpenFile(),
                        param => true
                        );
                }
                return _openFileCommand;
            }
        }
        public ICommand SendDefaultCommand
        {
            get
            {
                if (_sendDefaultCommand == null)
                {
                    _sendDefaultCommand = new RelayCommand(
                        param => this.SendDefault(),
                        param => this.CanSave && !this.IsNewNotam && CanSendDefault
                        );
                }
                return _sendDefaultCommand;
            }
        }

        public ICommand AddAftnCommand
        {
            get
            {
                if (_addAftnCommand == null)
                {
                    _addAftnCommand = new RelayCommand(
                        param => this.AddAftn(),
                        param => true
                        );
                }
                return _addAftnCommand;
            }
        }

         public ICommand RemoveManualAftnCommand
         {
             get
             {
                 if (_delManualAftnCommand == null)
                 {
                     _delManualAftnCommand = new RelayCommand(
                         param => this.RemoveManualAftn(),
                         param => true
                         );
                 }
                 return _delManualAftnCommand;
             }
         }
        public ICommand RemoveAftnCommand
        {
            get
            {
                if (_removeAftnCommand == null)
                {
                    _removeAftnCommand = new RelayCommand(
                        param => this.RemoveAftn(),
                        param => true
                        );
                }
                return _removeAftnCommand;
            }
        }
        public ICommand AddManualAftnCommand
        {
            get
            {
                if (_addManualAftnCommand == null)
                {
                    _addManualAftnCommand = new RelayCommand(
                        param => this.AddManualAftn(),
                        param => true
                        );
                }
                return _addManualAftnCommand;
            }
        }






        #endregion

        #region Public Methods
        private bool _bOnceAlert = true;
        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {

//             if (IsNewNotam)
//             {
//                 if (string.IsNullOrEmpty(_snowtam.Snowtam.Number))
            if (string.IsNullOrEmpty(this._snowtam.Snowtam.Number))
            {
                Snowtam lastThisDayeSnowtam = this._snowtamService.GetLastThisDayeSnowtam();
                if (lastThisDayeSnowtam == null)
                {
                    if (!this._snowtamService.ContainsSnowtam(this._snowtam.Snowtam))
                    {
                        this._snowtamService.UpdateWithNumber(_snowtam.Snowtam);
                    }
                }
                else
                {
                      this._snowtamService.Delete(lastThisDayeSnowtam);
                      this._snowtamService.Reload();
                      this._snowtamService.UpdateWithNumber(this._snowtam.Snowtam);
                }
            }
            else
                this._snowtamDetailService.Update(this._snowtam);
            if (!_snowtamDetailService.ContainsSnowtam(_snowtam))
            {
                    //_snowtamService.UpdateWithNumber(_snowtam.Snowtam);
                _notamText = null;
                base.OnPropertyChanged("NotamText");
                
                _snowtamDetailService.Insert(_snowtam);
            }
            else
            {
                _snowtamDetailService.Update(_snowtam);
            }
            NotamText = "";
            base.OnPropertyChanged("DisplayName");
            base.OnPropertyChanged("NotamText");
            MessageBox.Show("SNOTAM Saved Successfully");
        }


        public void Send()
        {
            if (SendNotam() == true)
            {
                parentWorkSpaces.Remove(NotamWorkspace);
                parentWorkSpaces.Remove(this);
            }
            
        }
        public void SendDefault()
        {
            if (SendNotam(true) == true)
            {
                parentWorkSpaces.Remove(NotamWorkspace);
                parentWorkSpaces.Remove(this);
            }

        }

        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                FlowDocument flowDocument = new FlowDocument();
                foreach (string line in NotamText.Split('\n'))
                {
                    Paragraph myParagraph = new Paragraph();
                    myParagraph.Margin = new Thickness(0);
                    myParagraph.Inlines.Add(new Run(line));
                    flowDocument.Blocks.Add(myParagraph);
                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                printDialog.PrintDocument(paginator, "Print");
            }
        }

        public void AddAftn()
        {
            if (SelectedAftn != null && (SelectedAftns == null || !SelectedAftns.Contains(SelectedAftn)))
            {
                if (SelectedAftns == null)
                    SelectedAftns = new ObservableCollection<Aftn>();
                SelectedAftns.Add(SelectedAftn);
            }
        }

        public void RemoveAftn()
        {
            if (RemovedAftns != null && RemovedAftns.Count > 0 && SelectedAftns != null)
                foreach (var item in RemovedAftns)
        {
                    if (SelectedAftns.Contains(item))
                        SelectedAftns.Remove(item);
                }
        }
        public void AddManualAftn()
        {
            if (!String.IsNullOrEmpty(ManualAftn) && (ManualAftns == null || !ManualAftns.Contains(ManualAftn)))
            {
                if (ManualAftn.IndexOf(" ") >= 0)
                {
                    MessageBox.Show("Invalid AFTN Address");
                    return;
                }
                if (ManualAftns == null)
                    ManualAftns = new ObservableCollection<String>();
                ManualAftns.Add(ManualAftn);
            }
        }

        public void RemoveManualAftn()
        {
            if (RemovedManualAftns != null && RemovedManualAftns.Count > 0 && RemovedManualAftns != null)
                foreach (var item in RemovedManualAftns)
                {
                    if (ManualAftns.Contains(item))
                        ManualAftns.Remove(item);
                }
            //             if (ManualAftns != null)
//                 ManualAftns.Clear();
        }


        public void OpenFile()
        {

            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".jpeg" };
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;
            }

        }

        #endregion // Public Methods

        #region IDataErrorInfo Members

        private readonly Dictionary<string, Func<SnowtamDetailViewModel, object>> propertyGetters;
        private readonly Dictionary<string, ValidationAttribute[]> validators;

        #region DataError

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        public string this[string propertyName]
        {
            get
            {
                if (this.propertyGetters.ContainsKey(propertyName))
                {
                    var propertyValue = this.propertyGetters[propertyName](this);
                    var errorMessages = this.validators[propertyName]
                        .Where(v => !v.IsValid(propertyValue))
                        .Select(v => v.ErrorMessage).ToArray();

                    return string.Join(Environment.NewLine, errorMessages);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error
        {
            get
            {
                var errors = from validator in this.validators
                             from attribute in validator.Value
                             where !attribute.IsValid(this.propertyGetters[validator.Key](this))
                             select attribute.ErrorMessage;

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        /// <summary>
        /// Gets the number of properties which have a validation attribute and are currently valid
        /// </summary>
        public int ValidPropertiesCount
        {
            get
            {
                var query = from validator in this.validators
                            where validator.Value.All(attribute => attribute.IsValid(this.propertyGetters[validator.Key](this)))
                            select validator;

                var count = query.Count(); //- this.validationExceptionCount;
                return count;
            }
        }

        /// <summary>
        /// Gets the number of properties which have a validation attribute
        /// </summary>
        public int TotalPropertiesWithValidationCount
        {
            get
            {
                return this.validators.Count();
            }
        }



        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        private Func<SnowtamDetailViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<SnowtamDetailViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        #endregion


        string IDataErrorInfo.Error
        {
            get { return (_snowtam as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                // error= ValidateProperty(columnName);

                if (propertyName == "CustomerType")
                {
                    // The IsCompany property of the Customer class 
                    // is Boolean, so it has no concept of being in
                    // an "unselected" state.  The CustomerViewModel
                    // class handles this mapping and validation.
                    error = this.ValidateType();
                }
                else
                {
                    error = (_snowtam as IDataErrorInfo)[propertyName];
                }

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateType()
        {
            return null;

            //if (this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
            //   this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
            //    return null;

            //return Strings.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion // IDataErrorInfo Members

        #region Private Methods

        bool IsNewNotam
        {
            get { return !_snowtamDetailService.ContainsSnowtam(_snowtam) && string.IsNullOrEmpty(_snowtam.Snowtam.Number);}
        }


        private string GenerateDefaultAFTNHeader()
        {
            if (_aftnOptions == null)
                return "";
            Aftn aftn = _aftnOptions.Where(a => a.Series.Equals("SA")).FirstOrDefault();
            if (aftn == null)
            {
                return "";
            }
            return aftn.AftnList;
        }
        private string GenerateAFTNHeader()
        {
            var aftnBuilder = new StringBuilder();
            int i = 0;
            if (SelectedAftns != null)
            {
                foreach (var item in SelectedAftns)
                {
                    //if(!firstDesRow)
                    aftnBuilder.Append(" ");
                    string[] strList = item.AftnList.Split(',');
                    foreach (string str in strList)
                    {
                        aftnBuilder.Append(str);
                        if (i == 7)
                        {
                            aftnBuilder.Append("\n");
                            i = 0;
                        }
                        else
                            aftnBuilder.Append(" ");

                        i++;
                    }
                }
            }

            if (ManualAftns != null)
            {
                foreach (var item in ManualAftns)
                {
                    //if(!firstDesRow)

                    aftnBuilder.Append(item);
                    if (i == 7)
                    {
                        aftnBuilder.Append(Environment.NewLine);
                        i = 0;
                    }
                    else
                        aftnBuilder.Append(" ");

                    i++;
                }
            }
            return aftnBuilder.ToString().TrimEnd(' ');
        }
        private int NoOfMessagesBySender()
        {
            int iCount = 0;
            if (SelectedAftns != null)
            {
                foreach (var item in SelectedAftns)
                {
                    //if(!firstDesRow)
                    string[] strList = item.AftnList.Split(',');
                    iCount += strList.Count();
                }
            }

            if (ManualAftns != null)
                iCount += ManualAftns.Count();
            return iCount % 21 == 0 ? iCount / 21 : (iCount / 21) + 1;
        }

        private Tuple<string, int> GenerateSendingNotamText(int iPart, int iLastIndex, string strAFTNList)
        {

            var ntmBuilder = new StringBuilder();

            #region First Line
            for (int i = 0; i < 120; i++)
                ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG");    
            ntmBuilder.Append(strAFTNList);
            //ntmBuilder.Append(Environment.NewLine);
            #endregion



            return Tuple.Create(ntmBuilder.ToString(), 0);
        }
        private bool SendNotam(bool bDefault=false)
        {
            if (bDefault)
            {
                string strDefault = GenerateDefaultAFTNHeader();
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Caution", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return false;
                SendNotam2AFTN(strDefault);
            }
            else
        {
            int iCount = NoOfMessagesBySender();
            if (iCount == 0)
            {
                MessageBox.Show("No AFTN selected");
                return false;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Caution", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return false;
            if (iCount > 1)
                MessageBox.Show("NOTAM WILL BE SENT IN " + iCount.ToString() + " MESSAGES");
            string[] strAllAFTN = GenerateAFTNHeader().Split('\n');
            int iRemaining = strAllAFTN.Count();


            for (int i = 0; i < iCount; i++)
            {
                if (iCount > 1)
                {
                    MessageBox.Show("SENDING NOTAM " + (i + 1).ToString() + " OF " + iCount.ToString() + " NOTAMS");
                    System.Threading.Thread.Sleep(1000);
                }
                string[] tempArray = strAllAFTN.Skip(i * 3).Take((iRemaining > 3) ? 3 : iRemaining).ToArray();
                string strAFTN = "";
                iRemaining -= tempArray.Count();
                foreach (string s in tempArray)
                {
                    strAFTN += (s + Environment.NewLine);
                }
                    strAFTN = strAFTN.TrimEnd('\n').TrimEnd('\r');
                SendNotam2AFTN(strAFTN);
            }
            }
            MessageBox.Show("Notam Message Sent Successfully");
            return true;
        }
        private void SendNotam2AFTN(string AFTNHeader)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings.Get("NotamPath").ToString();
            string strSend = NotamSender.GeneratePreviewSnowtamText(_snowtam.Snowtam,AFTNHeader);
            var time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() + "00.00G";
            path += time;
            File.WriteAllText(path, strSend);
        }

        #endregion



    }

}