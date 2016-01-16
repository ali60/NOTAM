using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using NOTAM.ViewModel;

namespace NOTAM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static UserDataContext _dataDC = new UserDataContext();
        private static NotamDataContext _dataDC2 = new NotamDataContext();

       

        static App()
        {
            // This code is used to test the app when using other cultures.
            //
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("it-IT");


            // Ensure the current culture passed into bindings is the OS culture.
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            //
            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        protected override void OnExit(ExitEventArgs e)
        {

        }
        private void SendRQN(Dictionary<string, string> dic,NotamDataContext dcontext)
        {
            if (!dic.ContainsKey("FIR"))
            {
                MessageBox.Show("No FIR in RQN");
                return;
            }
            string strFir = dic["FIR"];
            if (strFir.Length != 4)
                return;
            Notam notam2=null;
            if (!strFir.Substring(0, 2).Equals("OI"))
            {
                FIR fir = dcontext.FIRs.Where(n => n.Code.Equals(strFir)).FirstOrDefault();
                IntlNotam innotam = (
                    from o in App._dataDC2.IntlNotams
                    where o.Number.Equals(dic["RQNN"]) && (o.Type.Equals(dic["RQNT"]) && o.Year.Equals(dic["RQNY"]) && o.FIR == fir)
                    select o).FirstOrDefault<IntlNotam>();
                if (innotam == null)
                {
                    innotam = new IntlNotam();
                    innotam.Type = dic["RQNT"];
                    innotam.Number = dic["RQNN"];
                    innotam.Year = dic["RQNY"];
                    innotam.FIR = dcontext.FIRs.Where(n => (n.Code.Length == 4) && n.Code.Substring(0, 2).Equals(strFir.Substring(0, 2))).FirstOrDefault();
                    innotam.SendTime = DateTime.Now.ToString("yyMMddHHmm");
                    innotam.Origin = (
                        from x in App._dataDC2.Origins
                        where x.Code.Equals("OIIIYNYX")
                        select x).FirstOrDefault<Origin>();
                    innotam.Status = "N";
                    IntlNotam notam3 = (
                        from o in App._dataDC2.IntlNotams
                        where o.Type.Equals(dic["RQNT"]) && o.Year.Equals(dic["RQNY"]) && o.FIR == fir
                        select o into x
                        orderby x.Number descending
                        select x).FirstOrDefault<IntlNotam>();
                    if (int.Parse(notam3.Number) > int.Parse(notam2.Number))
                    {
                        innotam.Status = "BL";
                    }
                }
                notam2 = IntlNotamService.ConvertToNotam(innotam);
            }
            else
            {
                notam2 = (
                    from o in App._dataDC2.Notams
                    where o.Number.Equals(dic["RQNN"]) && (o.Type.Equals(dic["RQNT"]) && o.Year.Equals(dic["RQNY"]))
                    select o).FirstOrDefault<Notam>();

                if (notam2 == null)
                {
                    notam2 = new Notam();
                    notam2.Type = dic["RQNT"];
                    notam2.Number = dic["RQNN"];
                    notam2.Year = dic["RQNY"];
                    notam2.FIR = dcontext.FIRs.Where(n =>  (n.Code.Length==4) && n.Code.Substring(0,2).Equals(strFir.Substring(0,2))).FirstOrDefault();
                    notam2.SendTime = DateTime.Now.ToString("yyMMddHHmm");
                    notam2.Origin = (
                        from x in App._dataDC2.Origins
                        where x.Code.Equals("OIIIYNYX")
                        select x).FirstOrDefault<Origin>();
                    notam2.Status = "N";
                    Notam notam3 = (
                        from o in App._dataDC2.Notams
                        where o.Type.Equals(dic["RQNT"]) && o.Year.Equals(dic["RQNY"])
                        select o into x
                        orderby x.Number descending
                        select x).FirstOrDefault<Notam>();
                    if (int.Parse(notam3.Number) > int.Parse(notam2.Number))
                    {
                        notam2.Status = "BL";
                    }
                }
            }
            NotamSender notamSender = new NotamSender(notam2);
            if (dic.ContainsKey("Originat"))
            {
                notamSender.SendRQNNotam(dic["Originat"]);
            }
            return;

        }
        private void SendRQL(Dictionary<string, string> dic)
        {
            if (dic["NotamType"].Contains("RQL"))
            {
                if (!dic.ContainsKey("FIR"))
                {
                    MessageBox.Show("No FIR in RQL");
                    return;
                }
                string strFir = dic["FIR"];
                if (strFir.Length != 4)
                    return;
                Notam nt = new Notam();
                nt.Type = dic["RQLT"];
                nt.FIR = App._dataDC2.FIRs.Where(n => (n.Code.Length == 4) && n.Code.Substring(0, 2).Equals(strFir.Substring(0, 2))).FirstOrDefault();
                nt.SendTime = DateTime.Now.ToString("yyMMddHHmm");
                nt.Origin = (
                    from x in App._dataDC2.Origins
                    where x.Code.Equals("OIIIYNYX")
                    select x).FirstOrDefault<Origin>();
                NotamSender notamSender = new NotamSender(nt);
                List<Notam> notamList = (
                    from o in App._dataDC2.Notams
                    where o.Type.Equals(nt.Type) && o.Status.Equals("D")
                    select o).ToList<Notam>();
                if (!strFir.Substring(0, 2).Equals("OI"))
                {
                    List<IntlNotam> innotamList = (
                        from o in App._dataDC2.IntlNotams
                        where o.Type.Equals(nt.Type) && o.Status.Equals("D") && o.FIR == nt.FIR
                        select o).ToList<IntlNotam>();
                    notamList = IntlNotamService.ConvertList(innotamList);

                }
                if (dic.ContainsKey("Originat"))
                {
                    notamSender.SendRQLNotam(dic["Originat"], notamList);
                }
                return;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            AuthenticationService authenticationService = new AuthenticationService(App._dataDC);
            if (e != null && e.Args != null && e.Args.Count<string>() > 0)
            {
                try
                {
                    Dictionary<string, string> dic = this.MakeDictionary(e.Args);
                    if (dic != null)
                    {
                        try
                        {
                            User user = authenticationService.AuthenticateUser(dic["User"], dic["Pass"]);
                            customPrincipal = (Thread.CurrentPrincipal as CustomPrincipal);
                            if (customPrincipal == null)
                            {
                                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
                            }
                            customPrincipal.Identity = new CustomIdentity(user.Username, user.Role);
                        }
                        catch (UnauthorizedAccessException exp)
                        {
                            MessageBox.Show("Login failed! Please provide some valid credentials.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR: {0}", ex.Message);
                        }
                        if (this.IsAuthenticated)
                        {
                            MainWindowViewModel viewModel = null;
                            MainWindow window = new MainWindow();
                            string notamType = null;
                            try
                            {
                            if (dic.ContainsKey("GetAddr"))
                            {
                                var aftnService = new AftnService(App._dataDC2);
                                var AftnList = aftnService.GetAftnList().OrderBy(t => t.Series).ToList();                                string strFinal="";                                foreach(var aftn in AftnList)                                {
                                    strFinal += aftn.Series + "=" + aftn.AftnList+"\n";                                }                                System.IO.File.WriteAllText("C:\\NOTDIR\\ADDR.TXT", strFinal);
                                Application.Current.Shutdown();
                                return;
                            }
                            if (dic["NotamType"] != null)
                            {
                                notamType = dic["NotamType"].Substring(dic["NotamType"].Length - 1);
                            }
                            if (dic["NotamType"].Contains("NOTAM"))
                            {
                                if (dic["Originat"].IndexOf("OI") != 0)
                                {
                                    IntlNotam notam = new IntlNotam
                                    {
                                        Number = dic.ContainsKey("Num") ? dic["Num"] : "",
                                        NotamType = notamType,
                                        SendTime = dic.ContainsKey("Filling") ? dic["Filling"] : "",
                                        Type = dic.ContainsKey("Type") ? dic["Type"] : "",
                                        Year = dic.ContainsKey("year") ? dic["year"] : "",
                                        RefType = dic.ContainsKey("RefT") ? dic["RefT"] : "",
                                        RefYear = dic.ContainsKey("RefY") ? dic["RefY"] : "",
                                        RefNum = dic.ContainsKey("RefN") ? dic["RefN"] : "",
                                        LowerLimit = dic.ContainsKey("Lower") ? dic["Lower"] : "",
                                        HigherLimit = dic.ContainsKey("Upper") ? dic["Upper"] : "",
                                        Latitude = dic.ContainsKey("Latitude") ? dic["Latitude"] : "",
                                        Longtitude = dic.ContainsKey("Longtitude") ? dic["Longtitude"] : "",
                                        Radius = dic.ContainsKey("Radius") ? dic["Radius"] : "",
                                        FirAero = dic.ContainsKey("FirAd") ? dic["FirAd"] : "",
                                        FromDate = dic.ContainsKey("FromDate") ? dic["FromDate"] : "",
                                        ToDate = dic.ContainsKey("ToDate") ? dic["ToDate"] : "",
                                        PermEst = dic.ContainsKey("EstPerm") ? dic["EstPerm"] : "",
                                        EFreeText = dic.ContainsKey("ItemE") ? dic["ItemE"] : "",
                                        DFreeText = dic.ContainsKey("ItemD") ? dic["ItemD"] : "",
                                        FFreeText = dic.ContainsKey("ItemF") ? dic["ItemF"] : "",
                                        GFreeText = dic.ContainsKey("ItemG") ? dic["ItemG"] : "",
                                        Origin = dic.ContainsKey("FIR") ? (
                                            from o in App._dataDC2.Origins
                                            where o.Code.Equals(dic["Originat"])
                                            select o).FirstOrDefault<Origin>() : null,
                                        FIR = dic.ContainsKey("FIR") ? 
                                            App._dataDC2.FIRs.Where(n =>  (n.Code.Length==4) && n.Code.Substring(0,2).Equals(dic["FIR"].Substring(0,2))).FirstOrDefault()
                                             : null,
                                        Status = "D",
                                        NotamCode = dic.ContainsKey("QCode") ? (
                                            from o in App._dataDC2.NotamCodes
                                            where (o.Subject + o.Condition).Equals(dic["QCode"])
                                            select o).FirstOrDefault<NotamCode>() : null
                                    };
                                    IntlNotamService intlNotamService = new IntlNotamService(App._dataDC2);
                                    if (!intlNotamService.ContainsNotam(notam))
                                    {
                                        intlNotamService.Insert(notam);
//                                      MessageBox.Show("International NOTAM Inserted To Database Successfully");
                                    }
                                    else
                                    {
//                                        MessageBox.Show("International NOTAM Already Inserted To Database");
                                    }
                                    Application.Current.Shutdown();
                                    return;
                                }
//                                 if (!dic.ContainsKey("FIR"))
//                                 {
//                                     dic.Add("FIR", "OIIX");
//                                 }
//                                 Notam notam2 = new Notam
//                                 {
//                                     Number = "",
//                                     NotamType = notamType,
//                                     SendTime = dic.ContainsKey("Filling") ? dic["Filling"] : "",
//                                     Type = dic.ContainsKey("Type") ? dic["Type"] : "",
//                                     Year = dic.ContainsKey("year") ? dic["year"] : "",
//                                     RefType = dic.ContainsKey("RefT") ? dic["RefT"] : "",
//                                     RefYear = dic.ContainsKey("RefY") ? dic["RefY"] : "",
//                                     RefNum = dic.ContainsKey("RefN") ? dic["RefN"] : "",
//                                     LowerLimit = dic.ContainsKey("Lower") ? dic["Lower"] : "",
//                                     HigherLimit = dic.ContainsKey("Upper") ? dic["Upper"] : "",
//                                     Latitude = dic.ContainsKey("Latitude") ? dic["Latitude"] : "",
//                                     Longtitude = dic.ContainsKey("Longtitude") ? dic["Longtitude"] : "",
//                                     Radius = dic.ContainsKey("Radius") ? dic["Radius"] : "",
//                                     FirAero = dic.ContainsKey("FirAd") ? dic["FirAd"] : "",
//                                     FromDate = dic.ContainsKey("FromDate") ? dic["FromDate"] : "",
//                                     ToDate = dic.ContainsKey("ToDate") ? dic["ToDate"] : "",
//                                     PermEst = dic.ContainsKey("EstPerm") ? dic["EstPerm"] : "",
//                                     EFreeText = dic.ContainsKey("ItemE") ? dic["ItemE"] : "",
//                                     DFreeText = dic.ContainsKey("ItemD") ? dic["ItemD"] : "",
//                                     FFreeText = dic.ContainsKey("ItemF") ? dic["ItemF"] : "",
//                                     GFreeText = dic.ContainsKey("ItemG") ? dic["ItemG"] : "",
//                                     Origin = (
//                                         from o in App._dataDC2.Origins
//                                         where o.Code.Equals(dic["Originat"])
//                                         select o).FirstOrDefault<Origin>(),
//                                     FIR = (
//                                         from o in App._dataDC2.FIRs
//                                         where o.Code.Equals(dic["FIR"])
//                                         select o).FirstOrDefault<FIR>(),
//                                     Status = "D",
//                                     NotamCode = dic.ContainsKey("QCode") ? (
//                                         from o in App._dataDC2.NotamCodes
//                                         where (o.Subject + o.Condition).Equals(dic["QCode"])
//                                         select o).FirstOrDefault<NotamCode>() : null
//                                 };
//                                 viewModel = new MainWindowViewModel(App._dataDC2, notam2);
                            }
                            else
                            {
                                if (dic["NotamType"].Contains("RQN"))
                                {
                                    SendRQN(dic, App._dataDC2);
                                }
                                else
                                {
                                    SendRQL(dic);
                                }
                                Application.Current.Shutdown();
                                return;
                            }
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                Application.Current.Shutdown();
                                return;
                            }
                            EventHandler handler = null;
                            handler = delegate
                            {
                                viewModel.RequestClose -= handler;
                                window.Close();
                            };
                            viewModel.RequestClose += handler;
                            window.DataContext = viewModel;
                            window.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (!IsAuthenticated)
            {
                //Show the login view
                AuthenticationViewModel viewModel = new AuthenticationViewModel(authenticationService);
                var loginWindow = new AuthenticationView(viewModel);
                loginWindow.Show();
            }

        }

        private Dictionary<String ,String > MakeDictionary(string[] args)
        {
            return args.Select(item => item.Split('=')).Where(parts => parts.Count() == 2).ToDictionary(parts => parts[0], parts => parts[1]);
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }     

    }
}
