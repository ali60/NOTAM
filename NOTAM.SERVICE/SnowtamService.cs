using NOTAM.Service;
using NOTAM.SERVICE.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace NOTAM.SERVICE
{
    public class SnowtamService
    {
        private List<Snowtam> _Snowtams;
        private List<Snowtam> _ArchiveSnowtams;
        private List<Snowtam> _AllSnowtams;
        public NotamDataContext _dataContext;
        public event EventHandler<EntityAddedEventArgs<Snowtam>> SnowtamAdded;
        public SnowtamService(NotamDataContext dataContext)
        {
            this._dataContext = dataContext;
            this._Snowtams = this.LoadSnowtams(dataContext);
            this._ArchiveSnowtams = SnowtamService.LoadArchiveSnowtams(dataContext);
            this._AllSnowtams = new List<Snowtam>();
            this._AllSnowtams.AddRange(this._Snowtams);
            this._AllSnowtams.AddRange(this._ArchiveSnowtams);
        }
        public void Insert(Snowtam snowtam)
        {
            if (snowtam == null)
            {
                throw new ArgumentNullException("snowtam");
            }
            if (!this._dataContext.Snowtams.Contains(snowtam))
            {
                this._Snowtams.Add(snowtam);
                this._dataContext.Snowtams.InsertOnSubmit(snowtam);
                this._dataContext.SubmitChanges();
                if (this.SnowtamAdded != null)
                {
                    this.SnowtamAdded(this, new EntityAddedEventArgs<Snowtam>(snowtam));
                }
            }
        }
        public void Update(Snowtam snowtam)
        {
            this._dataContext.SubmitChanges();
        }
        public void UpdateWithNumber(Snowtam snowtam)
        {
            if (string.IsNullOrEmpty(snowtam.Number))
            {
                List<Snowtam> list = new List<Snowtam>();
                list.AddRange(this._Snowtams);
                list.AddRange(this._ArchiveSnowtams);
                List<Snowtam> source = (
                    from x in list
                    where !string.IsNullOrEmpty(x.Sendtime) && x.Sendtime.Substring(0, 2).Equals(DateTime.Now.Year.ToString().Substring(2, 2))
                    select x).ToList<Snowtam>();
                string text = (
                    from n in source
                    orderby n.Number descending
                    select n.Number).FirstOrDefault<string>();
                if (string.IsNullOrEmpty(text))
                {
                    text = "0";
                }
                int num = int.Parse(text) + 1;
                snowtam.Number = string.Format("{0:0000}", num);
                snowtam.Status = "D";
            }
            this.Insert(snowtam);
        }
        public void Delete(Snowtam snowtam)
        {
            if (this._AllSnowtams.Contains(snowtam))
            {
                this._AllSnowtams.Remove(snowtam);
                this._dataContext.Snowtams.DeleteOnSubmit(snowtam);
                this._dataContext.SubmitChanges();
            }
        }
        public bool ContainsSnowtam(Snowtam snowtam)
        {
            if (snowtam == null)
            {
                throw new ArgumentNullException("snowtam");
            }
            return this._Snowtams.Contains(snowtam);
        }
        public Snowtam GetLastThisDayeSnowtam()
        {
            this.Reload();
            Snowtam result = null;
            if (this._dataContext != null)
            {
                result = (
                    from n in this._AllSnowtams
                    where !string.IsNullOrEmpty(n.Sendtime) && n.Sendtime.Substring(0, 6).Equals(DateTime.Now.ToString("yyMMdd"))
                    select n).FirstOrDefault<Snowtam>();
            }
            return result;
        }
        public List<Aerodom> GetAllAerodomsForFIRStr(string sfir)
        {
            var aeroService = new AerodomService(_dataContext);
            List<Aerodom> aeroList = aeroService.GetAerodoms();
            var firService = new FIRService(_dataContext);
            FIR fir = firService.GetFIRs().Where(x => x.Code.Equals(sfir)).FirstOrDefault();
            return aeroList.Where(x => x.FIR == fir).OrderBy(x => x.Code).ToList();
        }
        public Aerodom GetAeroItem(string strAero)
        {
            var aeroService = new AerodomService(_dataContext);
            return aeroService.FindAeroByName(strAero);
        }

        public List<Snowtam> GetFilterSnowtams(NotamFilter notamFilter)
        {
            List<Snowtam> result = null;
            if (this._dataContext != null)
            {
            }
            return result;
        }
        public List<Snowtam> GetSnowtams()
        {
            return new List<Snowtam>(this._Snowtams);
        }
        public List<Snowtam> GetArchiveSnowtams()
        {
            return new List<Snowtam>(this._ArchiveSnowtams);
        }
        private static List<Snowtam> LoadArchiveSnowtams(NotamDataContext dataContext)
        {
            List<Snowtam> list = new List<Snowtam>();
            if (dataContext != null)
            {
                IQueryable<Snowtam> collection =
                    from n in dataContext.Snowtams
                    where n.Status.Equals("A")
                    select n;
                list.AddRange(collection);
            }
            return list;
        }
        public List<Origin> GetAllOrigins()
        {
            OriginService originService = new OriginService(this._dataContext);
            return originService.GetOrigins();
        }
        public List<FIR> GetAllFIRs()
        {
            FIRService fIRService = new FIRService(this._dataContext);
            return fIRService.GetFIRs();
        }
        public List<Snowtam> LoadSnowtams(NotamDataContext dataContext)
        {
            List<Snowtam> list = new List<Snowtam>();
            if (dataContext != null)
            {
                IQueryable<Snowtam> collection =
                    from n in dataContext.Snowtams
                    where n.Status.Equals("D")
                    select n;
                list.AddRange(collection);
            }
            return list;
        }
        public void Reload()
        {
            int num = (
                from n in this._dataContext.Snowtams
                where n.Status.Equals("D")
                select n).Count<Snowtam>();
            if (num != this._Snowtams.Count)
            {
                this._Snowtams = this.LoadSnowtams(this._dataContext);
            }
            num = (
                from n in this._dataContext.Snowtams
                where n.Status.Equals("A")
                select n).Count<Snowtam>();
            if (num != this._ArchiveSnowtams.Count)
            {
                this._ArchiveSnowtams = SnowtamService.LoadArchiveSnowtams(this._dataContext);
            }
            this._AllSnowtams = new List<Snowtam>();
            this._AllSnowtams.AddRange(this._Snowtams);
            this._AllSnowtams.AddRange(this._ArchiveSnowtams);
        }
        public void Archive()
        {
            List<Snowtam> list = _Snowtams.Where(x => !string.IsNullOrEmpty(x.Obsrvdate) && x.Obsrvdate.Length==8 &&
                                                   DateTime.ParseExact(x.Sendtime.Substring(0,2) + x.Obsrvdate, "yyMMddHHmm", CultureInfo.InvariantCulture).AddDays(1.0) < DateTime.Now)
                                                   .ToList<Snowtam>();
                
//                 (
//                 from n in this._Snowtams
//                 where DateTime.ParseExact(n.Obsrvdate, "yyMMddHHmm", CultureInfo.InvariantCulture).AddDays(1.0) < DateTime.Now
//                 select n).ToList<Snowtam>();
            foreach (Snowtam current in list)
            {
                current.Status = "A";
            }
            this._dataContext.SubmitChanges();
        }
        private static Stream GetResourceStream(string resourceFile)
        {
            return null;
        }
    }
}
