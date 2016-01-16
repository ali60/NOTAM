using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;

namespace NOTAM.SERVICE
{
    public class IntlNotamService
    {
        #region Fields

        List<IntlNotam> _Notams;
        List<IntlNotam> _ArchNotams;
        List<IntlNotam> _AllNotams;
        private List<NotamArchive> _NotamArchives;

        public NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public IntlNotamService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _Notams = LoadNotams(dataContext);
            _ArchNotams = LoadArchiveNotams(dataContext);
            _NotamArchives = LoadNotamArchives(dataContext);
            _AllNotams = new List<IntlNotam>();
            _AllNotams.AddRange(_Notams);
            _AllNotams.AddRange(_ArchNotams);

        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<IntlNotam>> NotamAdded;
        public event EventHandler<EntityAddedEventArgs<IntlNotam>> NotamDeleted;

        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(IntlNotam notam)
        {

            if (notam == null)
                throw new ArgumentNullException("notam");

            if (!_Notams.Contains(notam))
            {
                _Notams.Add(notam);
                _dataContext.IntlNotams.InsertOnSubmit(notam);

                #region Refrence IntlNotam
                if (notam.RefId > 0)
                {
                    NotamArchive archive = null;
                    var refNotam = _Notams.Where(n => n.Id.Equals(notam.RefId)).First();
                    if (refNotam != null)
                        refNotam.Status = "A";
                    if (notam.NotamType.Equals("R"))
                        archive = new NotamArchive()
                        {
                            Id = refNotam.Id,
                            Reason =
                                String.Format(NotamArchive.ReplacedBy, DateTime.Now.ToString())
                        };
                    else if (notam.NotamType.Equals("C"))
                        archive = new NotamArchive()
                        {
                            Id = refNotam.Id,
                            Reason =
                                String.Format(NotamArchive.CancledBy, DateTime.Now.ToString())
                        };

                    if (archive != null)
                    {
                        _NotamArchives.Add(archive);
                        _dataContext.NotamArchives.InsertOnSubmit(archive);
                    }
                }
                #endregion

                _dataContext.SubmitChanges();
                if (this.NotamAdded != null)
                    this.NotamAdded(this, new EntityAddedEventArgs<IntlNotam>(notam));

            }
        }

        public void Reload()
        {
            int cout = _dataContext.IntlNotams.Where(n => n.Status.Equals("D")).Count();
            if (cout != _Notams.Count)
                _Notams = LoadNotams(_dataContext);
            cout = _dataContext.IntlNotams.Where(n => n.Status.Equals("A")).Count();
            if (cout != _ArchNotams.Count)
                _ArchNotams = LoadArchiveNotams(_dataContext);
        }
        
        public void CreateBlanceForRCNotams()
        {
            var CRNotams =
                _AllNotams.Where(n => (n.NotamType == "C" || n.NotamType == "R")).ToList();
            if (CRNotams.Count == 0)
                return;
            List<IntlNotam> list = new List<IntlNotam>();
            list.AddRange(CRNotams);
            foreach (var item in list)
            {
                IntlNotam notam = item;              
                notam = item;
                notam.Status = "A";
                notam.Id = 0;
                notam.NotamType = "N";
                notam.Number = item.RefNum;
                notam.Year = item.RefYear;
                notam.Type = item.RefType;
                notam.EFreeText = "THIS BLANC NOTAM CREATED BECAUSE CANCELED OR REPLACED BY NOTAM : " + item.Number;
                item.RefId = 0;
                if (!ContainsNotam(item))
                    Insert(notam);

            }

        }
        
        public void Archive()
        {
           // CreateBlanceForRCNotams();
            var archNotams =
                _Notams.Where(
                    n => n.Status.Equals("D") && !string.IsNullOrEmpty(n.ToDate) && string.IsNullOrEmpty(n.PermEst) 
                        && n.ToDate.Length==8 && long.Parse(n.ToDate) < long.Parse(DateTime.Now.ToString("yyMMddHHmm"))).ToList();
            foreach (var item in archNotams)
            {
                item.Status = "A";
                #region Notam Archive

//                 var archive = new NotamArchive()
//                 {
//                     Id = item.Id,
//                     Reason =
//                         String.Format(NotamArchive.ToDateExpired, DateTime.Now.ToString())
//                 };
// 
// 
//                 _NotamArchives.Add(archive);
//                 _dataContext.NotamArchives.InsertOnSubmit(archive);
//                 _dataContext.SubmitChanges();

                #endregion
            }

            //cancel notam
            archNotams =
                _Notams.Where(
                    n => n.Status.Equals("D") && n.NotamType == "C").ToList();
            foreach (var item in archNotams)
            {
                item.Status = "A";

                #region Notam Archive

//                 var archive = new NotamArchive()
//                 {
//                     Id = item.Id,
//                     Reason =
//                         String.Format(NotamArchive.IsCancel)
//                 };
// 
// 
//                 _NotamArchives.Add(archive);
//                 _dataContext.NotamArchives.InsertOnSubmit(archive);
//                 _dataContext.SubmitChanges();

                #endregion
            }
            _dataContext.SubmitChanges();
            Reload();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(IntlNotam notam)
        {
            //_dataContext.Origins.OnSubmit(origin);
            _dataContext.SubmitChanges();

        }

        public void UpdateWithNumber(IntlNotam notam)
        {
            if (string.IsNullOrEmpty(notam.Number))
            {
                var curNumber = (from n in _Notams
                                 where (n.Type.Equals(notam.Type))
                                 orderby n.Number descending
                                 select n.Number).FirstOrDefault();
                if (string.IsNullOrEmpty(curNumber))
                    curNumber = "0";

                notam.Number = (Int32.Parse(curNumber) + 1).ToString();
            }
            Update(notam);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>


        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsNotam(IntlNotam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");
            var notamInList = _AllNotams.Where(n => n.Number.Equals(notam.Number) && (n.Type.Equals(notam.Type)) && (n.Year.Equals(notam.Year))
                                                && (string.IsNullOrEmpty(notam.FirAero) || (!string.IsNullOrEmpty(n.FirAero) && n.FirAero.Equals(notam.FirAero)))
                                                ).ToList();
            return notamInList.Count>0;
        }

        public IntlNotam GetByNumber(String notamNum)
        {
            IntlNotam result = null;
            if (_dataContext != null)
            {
                result = _dataContext.IntlNotams.Where(n => n.Number.Equals(notamNum) && n.Status.Equals("D")).FirstOrDefault();
            }
            return result;
        }

        public List<IntlNotam> GetFilterNotams(NotamFilter notamFilter)
        {
            List<IntlNotam> result = new List<IntlNotam>();
//            Reload();
            List<IntlNotam> sourceNotams = _Notams;
            if (notamFilter.notamStatus == "A")
                sourceNotams = _ArchNotams;
            else if (notamFilter.notamStatus == "AD")
                sourceNotams = _AllNotams;
            if (_dataContext != null)
            {
                try
                {
                    result = sourceNotams.Where(
                        n => (String.IsNullOrEmpty(notamFilter.TypeFilter) || n.Type.Equals(notamFilter.TypeFilter))).Where(
                            n =>
                            (String.IsNullOrEmpty(notamFilter.NumberFilter) || (!String.IsNullOrEmpty(n.Number) && n.Number.Equals(notamFilter.NumberFilter)))).Where(
                        n => (String.IsNullOrEmpty(notamFilter.YearFilter) || n.Year.Equals(notamFilter.YearFilter))).Where(
                            n =>
                            (String.IsNullOrEmpty(notamFilter.NotamCodeFilter) || (n.NotamCode != null && (n.NotamCode.ToString() == (notamFilter.NotamCodeFilter))))).
                            Where(
                            n =>
                            (String.IsNullOrEmpty(notamFilter.NotamAeroFilter) || (!String.IsNullOrEmpty(n.FirAero) && (n.FirAero).Equals(notamFilter.NotamAeroFilter))))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.FromNumberFilter) || long.Parse(n.Number) >= long.Parse(notamFilter.FromNumberFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.ToNumberFilter) || long.Parse(n.Number) <= long.Parse(notamFilter.ToNumberFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.FromDateFilter) || long.Parse(n.FromDate) >= long.Parse(notamFilter.FromDateFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.ToDateFilter) || (!String.IsNullOrEmpty(n.ToDate) && long.Parse(n.ToDate) <= long.Parse(notamFilter.ToDateFilter))))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.ItemEFilter) || (!String.IsNullOrEmpty(n.EFreeText)) && n.EFreeText.Contains(notamFilter.ItemEFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.NotamFirFilter) || (n.FIR != null) && n.FIR.Code.Equals(notamFilter.NotamFirFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.TrafficFilter) || (n.NotamCode != null && !String.IsNullOrEmpty(n.NotamCode.Traffic)) && n.NotamCode.Traffic.Equals(notamFilter.TrafficFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.PurposeFilter) || (n.NotamCode != null && !String.IsNullOrEmpty(n.NotamCode.Purpose)) && n.NotamCode.Purpose.Equals(notamFilter.PurposeFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.ScopeFilter) || (n.NotamCode != null && !String.IsNullOrEmpty(n.NotamCode.Scope)) && n.NotamCode.Scope.Equals(notamFilter.ScopeFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.SendTimeFilter) || (!String.IsNullOrEmpty(n.SendTime) && long.Parse(n.SendTime) >= long.Parse(notamFilter.SendTimeFilter))))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.SendTimeEndFilter) || (!String.IsNullOrEmpty(n.SendTime) && long.Parse(n.SendTime) <= long.Parse(notamFilter.SendTimeEndFilter))))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.PermEstFilter) || (!String.IsNullOrEmpty(n.PermEst)) && n.PermEst.Equals(notamFilter.PermEstFilter)))
                            .Where(n => (String.IsNullOrEmpty(notamFilter.PermEstFilter) || notamFilter.PermEstFilter.Equals("EST") && !(string.IsNullOrEmpty(n.ToDate))
                                    && n.PermEst.Equals(notamFilter.PermEstFilter) && long.Parse(n.ToDate) <= long.Parse(DateTime.Now.ToString("yyMMddHHmm"))))
                            .ToList();
                }
                catch (System.Exception ex)
                {
                    string s = ex.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<IntlNotam> GetNotams()
        {
            return new List<IntlNotam>(_Notams);
        }
        public List<IntlNotam> GetArchiveNotams()
        {
            return new List<IntlNotam>(_ArchNotams);
        }

        public static  Notam ConvertToNotam(IntlNotam nt)
        {
            Notam n=new Notam();
            n.Code_Id = nt.Code_Id;
            n.Id = nt.Id;
            n.Number = nt.Number;
            n.Origin = nt.Origin;
            n.DFreeText = nt.DFreeText;
            n.EFreeText = nt.EFreeText;
            n.FFreeText = nt.FFreeText;
            n.FIR = nt.FIR;
            n.Fir_Id = nt.Fir_Id;
            n.FirA2 = nt.FirA2;
            n.FirA3 = nt.FirA3;
            n.FirA4 = nt.FirA4;
            n.FirA5 = nt.FirA5;
            n.FirAero = nt.FirAero;
            n.FromDate = nt.FromDate;
            n.FromRDate = nt.FromRDate;
            n.GFreeText = nt.GFreeText;
            n.HigherLimit = nt.HigherLimit;
            n.Latitude = nt.Latitude;
            n.Longtitude = nt.Longtitude;
            n.LowerLimit = nt.LowerLimit;
            n.NotamCode = nt.NotamCode;
            n.NotamType = nt.NotamType;
            n.Origin_Id = nt.Origin_Id;
            n.PermEst = nt.PermEst;
            n.Radius = nt.Radius;
            n.RefId = nt.RefId;
            n.RefNum = nt.RefNum;
            n.RefType = nt.RefType;
            n.RefYear = nt.RefType;
            n.SendTime = nt.SendTime;
            n.Status = nt.Status;
            n.ToDate = nt.ToDate;
            n.ToRDate = nt.ToRDate;
            n.Type = nt.Type;
            n.Year = nt.Year;
            return n;
        }
        public static List<Notam> ConvertList(List<IntlNotam> IntlNotamList)
        {
            List<Notam> newList = new List<Notam>();
            foreach (IntlNotam n in IntlNotamList)
                newList.Add(ConvertToNotam(n));
            return newList;

        }
        public List<NotamArchive> GetNotamArchives()
        {
            return new List<NotamArchive>(_NotamArchives);
        }
        public List<Origin> GetAllOrigins()
        {
            var originService = new OriginService(_dataContext);
            return originService.GetOrigins();
        }
        public List<FIR> GetAllFIRs()
        {
            var firService = new FIRService(_dataContext);
            return firService.GetFIRs();
        }
        public List<NotamCode> GetAllNotamCodes()
        {
            var notamCodeService = new NotamCodeService(_dataContext);
            return notamCodeService.GetNotamCodes();
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<IntlNotam> LoadNotams(NotamDataContext dataContext)
        {
            var result = new List<IntlNotam>();
            if (dataContext != null)
            {
                var notams = dataContext.IntlNotams.Where(n => n.Status.Equals("D"));
                result.AddRange(notams);
            }
            return result;

        }
        static List<IntlNotam> LoadArchiveNotams(NotamDataContext dataContext)
        {
            var result = new List<IntlNotam>();
            if (dataContext != null)
            {
                var notams = dataContext.IntlNotams.Where(n => n.Status.Equals("A"));
//                 foreach (var item in notams)
//                 {
//                     IntlNotam item1 = item;
//                     item.ArchiveReason =
//                         dataContext.NotamArchives.Where(n => n.Id == item1.Id).Select(n => n.Reason).SingleOrDefault();
//                 }


                result.AddRange(notams);
            }
            return result;

        }

        static List<IntlNotam> LoadHoldNotams(NotamDataContext dataContext)
        {
            var result = new List<IntlNotam>();
            if (dataContext != null)
            {
                var notams = dataContext.IntlNotams.Where(n => n.Status.Equals("H"));
                result.AddRange(notams);
            }
            return result;

        }
        static List<NotamArchive> LoadNotamArchives(NotamDataContext dataContext)
        {
            var result = new List<NotamArchive>();
            if (dataContext != null)
            {
                var notams = dataContext.NotamArchives;
                result.AddRange(notams);
            }
            return result;
        }
        public List<Aerodom> GetAllAerodomsForFIR(FIR fir)
        {
            var aeroService = new AerodomService(_dataContext);
            List<Aerodom> aeroList = aeroService.GetAerodoms();
            return aeroList.Where(x => x.FIR == fir).ToList();
        }
        public void Delete(IntlNotam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");
            List<IntlNotam> listNotams = _Notams;
            switch (notam.Status[0])
            {
                case 'A':
                    listNotams = _ArchNotams;
                    break;
            }
            if (listNotams.Contains(notam))
            {
                listNotams.Remove(notam);
                _dataContext.IntlNotams.DeleteOnSubmit(notam);
                _dataContext.SubmitChanges();
            }

            if (this.NotamDeleted != null)
                this.NotamDeleted(this, new EntityAddedEventArgs<IntlNotam>(notam));
        }

        public void Archive(IntlNotam notam)
        {

            if (notam.Status.Equals("D"))
            {
                notam.Status = "A";
//                 archive = new NotamArchive()
//                 {
//                     Id = notam.Id,
//                     Reason =
//                         String.Format(NotamArchive.ForcedBy, DateTime.Now.ToString())
//                 };
//                 _NotamArchives.Add(archive);
//                _dataContext.NotamArchives.InsertOnSubmit(archive);

                _dataContext.SubmitChanges();
                _Notams = LoadNotams(_dataContext);
                _ArchNotams = LoadArchiveNotams(_dataContext);
            }
        }
        public Aerodom GetAeroItem(string strAero)
        {
            var aeroService = new AerodomService(_dataContext);
            return aeroService.FindAeroByName(strAero);
        }



        static Stream GetResourceStream(string resourceFile)
        {
            return null;
            //Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            //StreamResourceInfo info = Application.GetResourceStream(uri);
            //if (info == null || info.Stream == null)
            //    throw new ApplicationException("Missing resource file: " + resourceFile);

            //return info.Stream;
        }

        #endregion // Private Helpers


    }
}
