using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;

namespace NOTAM.SERVICE
{
    public class NotamService
    {
        #region Fields

        List<Notam> _Notams;
        List<Notam> _AllNotams;
        List<Notam> _ArchNotams;
        List<Notam> _HoldNotams;

        private List<NotamArchive> _NotamArchives;

        public NotamDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public NotamService(NotamDataContext dataContext)
        {
            _dataContext = dataContext;
            _Notams = LoadNotams(dataContext);
            _ArchNotams = LoadArchiveNotams(dataContext);
            _HoldNotams = LoadHoldNotams(dataContext);
            _NotamArchives = LoadNotamArchives(dataContext);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a origin is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<Notam>> NotamAdded;
        public event EventHandler<EntityAddedEventArgs<Notam>> NotamDeleted;
        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Reload()
        {
            int cout = _dataContext.Notams.Where(n => n.Status.Equals("D")).Count();
            if(cout != _Notams.Count)
                _Notams = LoadNotams(_dataContext);
            cout = _dataContext.Notams.Where(n => n.Status.Equals("A")).Count();
            if (cout != _ArchNotams.Count)
                _ArchNotams = LoadArchiveNotams(_dataContext);
            cout = _dataContext.Notams.Where(n => n.Status.Equals("H")).Count();
            if (cout != _HoldNotams.Count)
                _HoldNotams = LoadHoldNotams(_dataContext);
            cout = _dataContext.NotamArchives.Count();
            if (cout != _NotamArchives.Count)
                _NotamArchives = LoadNotamArchives(_dataContext);
        }
        public void Insert(Notam notam)
        {
            
            if (notam == null)
                throw new ArgumentNullException("notam");
            List<Notam> listNotams=_Notams;
            switch (notam.Status[0])
            {
                case 'H':
                    listNotams = _HoldNotams;
                    break;
                case 'A':
                    listNotams = _ArchNotams;
                    break;
            }
            if (!listNotams.Contains(notam))
            {
                listNotams.Add(notam);
                _dataContext.Notams.InsertOnSubmit(notam);
            }
            _dataContext.SubmitChanges();
            if (this.NotamAdded != null)
                this.NotamAdded(this, new EntityAddedEventArgs<Notam>(notam));
              
            
        }

        public  void Archive()
        {
            //add0519
            var archNotams =
                _Notams.Where(
                    n => n.Status.Equals("D") && !string.IsNullOrEmpty(n.ToDate) && string.IsNullOrEmpty(n.PermEst) 
                        && long.Parse(n.ToDate) < long.Parse(DateTime.Now.ToString("yyMMddHHmm"))).ToList();
            foreach (var item in archNotams)
            {
                item.Status = "A";

                #region Notam Archive

                var archive = new NotamArchive()
                                  {
                                      Id = item.Id,
                                      Reason =
                                          String.Format(NotamArchive.ToDateExpired, DateTime.Now.ToString())
                                  };


                _NotamArchives.Add(archive);
                _dataContext.NotamArchives.InsertOnSubmit(archive);
                _dataContext.SubmitChanges();

                #endregion
            }

            //cancel notam
            archNotams =
                _Notams.Where(
                    n => n.Status.Equals("D") && n.NotamType=="C").ToList();
            foreach (var item in archNotams)
            {
                item.Status = "A";

                #region Notam Archive

                var archive = new NotamArchive()
                {
                    Id = item.Id,
                    Reason =
                        String.Format(NotamArchive.IsCancel)
                };


                _NotamArchives.Add(archive);
                _dataContext.NotamArchives.InsertOnSubmit(archive);
                _dataContext.SubmitChanges();

                #endregion
            }
        }

        public void Hold(Notam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");

            if (!_Notams.Contains(notam))
            {
                _Notams.Add(notam);
                _dataContext.Notams.InsertOnSubmit(notam);
                _dataContext.SubmitChanges();
                if (this.NotamAdded != null)
                    this.NotamAdded(this, new EntityAddedEventArgs<Notam>(notam));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(Notam notam)
        {
            //_dataContext.Origins.OnSubmit(origin);
            _dataContext.SubmitChanges();
            Reload();

        }

        public void UpdateWithNumber(Notam notam)
        {
            if (string.IsNullOrEmpty(notam.Number))
            {
                var curNumber = (from n in _dataContext.Notams
                                 where (n.Type.Equals(notam.Type) && n.Year.Equals(DateTime.Now.Year.ToString().Substring(2)) && !n.Status.Equals("H"))
                                 orderby n.Number descending
                                 select n.Number).FirstOrDefault();
                if (string.IsNullOrEmpty(curNumber))
                    curNumber = "0";

                int iNextNum = (Int32.Parse(curNumber) + 1);
                notam.Number = string.Format("{0:0000}", iNextNum );
            }
            notam.Status = "D";
            Update(notam);
            if (notam.RefId > 0)
            {
                NotamArchive archive = null;
                var refNotam = _Notams.Where(n => n.Id.Equals(notam.RefId)).FirstOrDefault();
                if(refNotam==null)
                    return;
                refNotam.Status = "A";
                if (notam.NotamType.Equals("R"))
                    archive = new NotamArchive()
                    {
                        Id = refNotam.Id,
                        Reason =
                            String.Format(NotamArchive.ReplacedBy, DateTime.Now.ToString(), notam.Number)
                    };
                else if (notam.NotamType.Equals("C"))
                    archive = new NotamArchive()
                    {
                        Id = refNotam.Id,
                        Reason =
                            String.Format(NotamArchive.CancledBy, DateTime.Now.ToString(), notam.Number)
                    };

                if (archive != null)
                {
                    _NotamArchives.Add(archive);
                    _dataContext.NotamArchives.InsertOnSubmit(archive);
                    Archive();
                }
            }
            _dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(Notam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");
            List<Notam> listNotams = _Notams;
            switch (notam.Status[0])
            {
                case 'H':
                    listNotams = _HoldNotams;
                    break;
                case 'A':
                    listNotams = _ArchNotams;
                    break;
            }
            if (listNotams.Contains(notam))
            {
                listNotams.Remove(notam);
                _dataContext.Notams.DeleteOnSubmit(notam);
                _dataContext.SubmitChanges();
            }

            if (this.NotamDeleted != null)
                this.NotamDeleted(this, new EntityAddedEventArgs<Notam>(notam));
        }

        public void Archive(Notam notam)
        {
            NotamArchive archive = null;

            if (notam.Status.Equals("D"))
            {
                notam.Status = "A";
                archive = new NotamArchive()
                {
                    Id = notam.Id,
                    Reason =
                        String.Format(NotamArchive.ForcedBy, DateTime.Now.ToString())
                };
                _NotamArchives.Add(archive);
                _dataContext.NotamArchives.InsertOnSubmit(archive);

                _dataContext.SubmitChanges();
                _Notams = LoadNotams(_dataContext);
                _ArchNotams = LoadArchiveNotams(_dataContext);
            }
        }
        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsNotam(Notam notam)
        {
            if (notam == null)
                throw new ArgumentNullException("notam");

            return _Notams.Contains(notam) || _HoldNotams.Contains(notam);
        }

        public Notam GetByNumber(String notamNum,string refType,string refYear,string Status="D")
        {
            Notam result=null;
            if (_dataContext != null)
            {
                result = _dataContext.Notams.Where(n => n.Number.Equals(notamNum))
                    .Where(n => (string.IsNullOrEmpty(Status) || Status.Equals(n.Status)))
                    .Where(n => (string.IsNullOrEmpty(refType) || refType.Equals(n.Type)))
                    .Where(n => (string.IsNullOrEmpty(refYear) || refYear.Equals(n.Year)))
                    .FirstOrDefault();
            }
            return result;
        }

        public bool CheckUnique(String notamNum, string Type,string Year)
        {
            Notam result = null;
            if (_dataContext != null)
            {
                result = _dataContext.Notams.Where(n => n.Number.Equals(notamNum) && n.Status.Equals("D"))
                    .Where(n => (Type.Equals(n.Type)) && (Year.Equals(n.Year))).FirstOrDefault();
            }
            return (result==null);
        }

        public List<Notam> GetFilterNotams(NotamFilter notamFilter)
        {
            List<Notam> result = new List<Notam>();
            Reload();
            List<Notam> sourceNotams = _Notams;
            if (notamFilter.notamStatus == "A")
                sourceNotams = _ArchNotams;
            else if (notamFilter.notamStatus == "H")
                sourceNotams = _HoldNotams;
            else if (notamFilter.notamStatus == "AD")
                sourceNotams = GetAllNotams();
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
                            .Where(n => (String.IsNullOrEmpty(notamFilter.PermEstFilter) || notamFilter.PermEstFilter.Equals("PERM") || (notamFilter.PermEstFilter.Equals("EST") && !(string.IsNullOrEmpty(n.ToDate)) 
                                    && n.PermEst.Equals(notamFilter.PermEstFilter) && long.Parse(n.ToDate) <= long.Parse(DateTime.Now.ToString("yyMMddHHmm")))))
                            .ToList();
                    if (!string.IsNullOrEmpty( notamFilter.UserFilter))
                    {
                        NotamDetailService notamDetailService = new NotamDetailService(_dataContext);
                        result= notamDetailService.FilterByUser(result, notamFilter.UserFilter);
                    }
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
        public List<Notam> GetNotams()
        {
            return new List<Notam>(_Notams);
        }
        public List<Notam> GetArchiveNotams()
        {
            return new List<Notam>(_ArchNotams);
        }
        public List<Notam> GetHoldNotams()
        {
            return new List<Notam>(_HoldNotams);
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
        public List<Aerodom> GetAllAerodomsForFIR(FIR fir)
        {
            var aeroService = new AerodomService(_dataContext);
            List<Aerodom> aeroList = aeroService.GetAerodoms();
            return aeroList.Where(x => x.FIR == fir).OrderBy(x => x.Code).ToList();
        }
        public Aerodom GetAeroItem(string strAero)
        {
            var aeroService = new AerodomService(_dataContext);
            return aeroService.FindAeroByName(strAero);
        }

        public List<Aerodom> GetAllAerodoms()
        {
            var aeroService = new AerodomService(_dataContext);
            List<Aerodom> aeroList = aeroService.GetAerodoms();
            return aeroList;
        }

        #endregion // Public Interface

        #region Private Helpers

        public List<Notam> LoadNotams(NotamDataContext dataContext)
        {
            var result = new List<Notam>();
            if (dataContext != null)
            {
                var notams = dataContext.Notams.Where(n=>n.Status.Equals("D")).OrderBy(x=>x.Type).ThenByDescending(y=>y.Year).ThenByDescending(z=>z.Number);
                result.AddRange(notams);
            }
            return result;

        }
        //public List<Notam> LoadArchiveNotams(NotamDataContext dataContext)
        //{
        //    var result = new List<Notam>();
        //    if (dataContext != null)
        //    {
        //        var notams = dataContext.Notams.Where(n => n.Status.Equals("A")).OrderBy(x=>x.Type).ThenByDescending(y=>y.Year).ThenByDescending(z=>z.Number);
        //        foreach (var item in notams)
        //        {
        //            Notam item1 = item;
        //            item.ArchiveReason =
        //                dataContext.NotamArchives.Where(n => n.Id == item1.Id).Select(n => n.Reason).SingleOrDefault();
        //        }

              
        //        result.AddRange(notams);
        //    }
        //    return result;

        //}

        public List<Notam> LoadArchiveNotams(NotamDataContext dataContext)
        {
            var result = new List<Notam>();
            if (dataContext != null)
            {
                var notams = dataContext.Notams.Where(n => n.Status.Equals("A")).OrderBy(x => x.Type).ThenByDescending(y => y.Year).ThenByDescending(z => z.Number);
                var AllArchives = new List<NotamArchive>();
                AllArchives.AddRange(dataContext.NotamArchives);
                foreach (var item in notams)
                {
                    Notam item1 = item;
                    item.ArchiveReason = AllArchives.Where(n => n.Id == item1.Id).Select(n => n.Reason).SingleOrDefault();
                }


                result.AddRange(notams);
            }
            return result;

        }

        public List<Notam> GetAllNotams()
        {
            var AllNotams = new List<Notam>();
            AllNotams.AddRange(_Notams);
            AllNotams.AddRange(_ArchNotams);
            return AllNotams.OrderBy(x=>x.Type).ThenByDescending(y=>y.Year).ThenByDescending(y=>y.Number).ToList();
        }

        public List<Notam> LoadHoldNotams(NotamDataContext dataContext)
        {
            var result = new List<Notam>();
            if (dataContext != null)
            {
                var notams = dataContext.Notams.Where(n => n.Status.Equals("H")).OrderBy(x=>x.Type).ThenByDescending(y=>y.Year).ThenByDescending(y=>y.Id);
                result.AddRange(notams);
            }
            return result;

        }
        public List<NotamArchive> LoadNotamArchives(NotamDataContext dataContext)
        {
            var result = new List<NotamArchive>();
            if (dataContext != null)
            {
                var notams = dataContext.NotamArchives;
                result.AddRange(notams);
            }
            return result;
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
