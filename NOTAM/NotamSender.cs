using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NOTAM.Service;
using NOTAM.SERVICE.Model;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using NOTAM.Properties;
using NOTAM.Service;
using NOTAM.SERVICE;
using NOTAM.SERVICE.Model;
using System.Windows.Controls;


namespace NOTAM.SERVICE
{
    class NotamSender
    {
        private String openPar = "(";
        private String closePar = ")";
        private String slash = "/";
        private String nullChar = "\0";
        Notam _notam;
#region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public NotamSender(Notam notam)
        {
            _notam = notam;
        }

#endregion

        string GenerateRQOMessage(string strAFTN)
        {
            var ntmBuilder = new StringBuilder();

            #region First Line
            for (int i = 0; i < 120; i++)
                ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG ");
            if (!string.IsNullOrEmpty(strAFTN))
            {
                ntmBuilder.Append(strAFTN.Replace("\r\n", ""));
                ntmBuilder.Append(Environment.NewLine);
            }
            else
                ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region Second Line
            string filling = DateTime.Now.ToString("HHmmss");
            ntmBuilder.Append(filling + " " +
                              _notam.Origin.Code + Environment.NewLine);
            #endregion
            //RQR
            ntmBuilder.Append("RQR " + _notam.FIR.Code +" "+_notam.Type+_notam.Number+_notam.Year
                              + Environment.NewLine);

            #region 3rd line
            string notamNum = _notam.Number ;
            if (_notam.NotamType == "N")
                ntmBuilder.Append(openPar + _notam.Type + notamNum + slash + _notam.Year + " " +
                                  "NOTAM" + _notam.NotamType + Environment.NewLine);
            else
                ntmBuilder.Append(openPar + _notam.Type + notamNum + slash + _notam.Year + " " +
                                  "NOTAM" + _notam.NotamType + " " + _notam.RefType + _notam.RefNum + "/" + _notam.RefYear + Environment.NewLine);

            #endregion

            #region Q line
            ntmBuilder.Append("Q" + closePar + _notam.FIR.Code);

            if (_notam.NotamCode != null)
            {
                ntmBuilder.Append(slash + "Q" + _notam.NotamCode.ToString());
                if (!string.IsNullOrEmpty(_notam.NotamCode.Traffic))
                    ntmBuilder.Append(slash + _notam.NotamCode.Traffic);
                if (!string.IsNullOrEmpty(_notam.NotamCode.Purpose))
                    ntmBuilder.Append(slash + _notam.NotamCode.Purpose);
                if (!string.IsNullOrEmpty(_notam.NotamCode.Scope))
                    ntmBuilder.Append(slash + _notam.NotamCode.Scope);
            }
            if (!string.IsNullOrEmpty(_notam.LowerLimit))
                ntmBuilder.Append(slash + _notam.LowerLimit);
            if (!string.IsNullOrEmpty(_notam.HigherLimit))
                ntmBuilder.Append(slash + _notam.HigherLimit);
            ntmBuilder.Append(slash);
            if (!string.IsNullOrEmpty(_notam.Latitude))
                ntmBuilder.Append(_notam.Latitude);
            if (!string.IsNullOrEmpty(_notam.Longtitude))
                ntmBuilder.Append(slash + _notam.Longtitude);
            if (!string.IsNullOrEmpty(_notam.Radius))
                ntmBuilder.Append(slash + _notam.Radius);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            ntmBuilder.Append(" " + closePar);

            return ntmBuilder.ToString();

        }

        string GenerateRQNMessage(string strAFTN)
        {
            var ntmBuilder = new StringBuilder();

            #region First Line
            if (!string.IsNullOrEmpty(strAFTN))
                for (int i = 0; i < 120; i++)
                    ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG ");
            if (!string.IsNullOrEmpty(strAFTN))
            {
                ntmBuilder.Append(strAFTN.Replace("\r\n", ""));
                ntmBuilder.Append(Environment.NewLine);
            }
            else
                ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region Second Line

            ntmBuilder.Append(_notam.SendTime.Substring(_notam.SendTime.Length - 6, 6) + " " +
                              _notam.Origin.Code + Environment.NewLine);
            #endregion

            ntmBuilder.Append("RQR " + _notam.FIR.Code + " " + _notam.Type + _notam.Number + _notam.Year
                  + Environment.NewLine);
            if (_notam.Status == "A")
            {
                ntmBuilder.Append("NOTAM DELETED" + Environment.NewLine);
                return ntmBuilder.ToString();
            }
            if (_notam.Status == "BL")
            {
                ntmBuilder.Append("NOTAM REQUESTED" + Environment.NewLine);
                return ntmBuilder.ToString();
            }
            if (_notam.Status == "N")
            {
                ntmBuilder.Append("NOTAM NOT ISSUED" + Environment.NewLine);
                return ntmBuilder.ToString();
            }
            ntmBuilder.Append("ORIGINAL NOTAM" + Environment.NewLine);


            #region 3rd line
            string notamNum = _notam.Number ?? "****";
            if (_notam.NotamType == "N")
                ntmBuilder.Append(openPar + _notam.Type + notamNum + slash + _notam.Year + " " +
                                  "NOTAM" + _notam.NotamType + Environment.NewLine);
            else
                ntmBuilder.Append(openPar + _notam.Type + notamNum + slash + _notam.Year + " " +
                                  "NOTAM" + _notam.NotamType + " " + _notam.RefType + _notam.RefNum + "/" + _notam.RefYear + Environment.NewLine);

            #endregion

            #region Q line
            ntmBuilder.Append("Q" + closePar + _notam.FIR.Code);

            if (_notam.NotamCode != null)
            {
                ntmBuilder.Append(slash + "Q" + _notam.NotamCode.ToString());
                if (!string.IsNullOrEmpty(_notam.NotamCode.Traffic))
                    ntmBuilder.Append(slash + _notam.NotamCode.Traffic);
                if (!string.IsNullOrEmpty(_notam.NotamCode.Purpose))
                    ntmBuilder.Append(slash + _notam.NotamCode.Purpose);
                if (!string.IsNullOrEmpty(_notam.NotamCode.Scope))
                    ntmBuilder.Append(slash + _notam.NotamCode.Scope);
            }
            if (!string.IsNullOrEmpty(_notam.LowerLimit))
                ntmBuilder.Append(slash + _notam.LowerLimit);
            if (!string.IsNullOrEmpty(_notam.HigherLimit))
                ntmBuilder.Append(slash + _notam.HigherLimit);
            ntmBuilder.Append(slash);
            if (!string.IsNullOrEmpty(_notam.Latitude))
                ntmBuilder.Append(_notam.Latitude);
            if (!string.IsNullOrEmpty(_notam.Longtitude))
                ntmBuilder.Append(slash + _notam.Longtitude);
            if (!string.IsNullOrEmpty(_notam.Radius))
                ntmBuilder.Append(slash + _notam.Radius);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region A B C line

            ntmBuilder.Append("A" + closePar + _notam.FirAero);
            if (!string.IsNullOrEmpty(_notam.FirA2))
                ntmBuilder.Append(slash + _notam.FirA2);
            if (!string.IsNullOrEmpty(_notam.FirA3))
                ntmBuilder.Append(slash + _notam.FirA3);
            if (!string.IsNullOrEmpty(_notam.FirA4))
                ntmBuilder.Append(slash + _notam.FirA4);
            if (!string.IsNullOrEmpty(_notam.FirA5))
                ntmBuilder.Append(slash + _notam.FirA5);

            ntmBuilder.Append(" B" + closePar + _notam.FromDate);

            if (!string.IsNullOrEmpty(_notam.ToDate))
                ntmBuilder.Append(" C" + closePar + _notam.ToDate);

            if (!string.IsNullOrEmpty(_notam.PermEst))
                ntmBuilder.Append(" " + _notam.PermEst);
            ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region D E line

            if (!string.IsNullOrEmpty(_notam.DFreeText))
                ntmBuilder.Append("D" + closePar + _notam.DFreeText + Environment.NewLine);


            if (!string.IsNullOrEmpty(_notam.EFreeText))
                ntmBuilder.Append("E" + closePar + _notam.EFreeText);
            #endregion

            #region F G line

            if (!string.IsNullOrEmpty(_notam.FFreeText))
                ntmBuilder.Append(Environment.NewLine + "F" + closePar + _notam.FFreeText);


            if (!string.IsNullOrEmpty(_notam.GFreeText))
                ntmBuilder.Append(" G" + closePar + _notam.GFreeText + Environment.NewLine);
            #endregion

            ntmBuilder.Append(" " + closePar);

            return ntmBuilder.ToString();

        }

        string GenerateRQLMessage(string strAFTN, List<Notam> notamList)
        {
            var ntmBuilder = new StringBuilder();

            #region First Line
            if (!string.IsNullOrEmpty(strAFTN))
                for (int i = 0; i < 120; i++)
                    ntmBuilder.Append(nullChar);
            ntmBuilder.Append("GG ");
            if (!string.IsNullOrEmpty(strAFTN))
            {
                ntmBuilder.Append(strAFTN.Replace("\r\n", ""));
                ntmBuilder.Append(Environment.NewLine);
            }
            else
                ntmBuilder.Append(Environment.NewLine);
            #endregion

            #region Second Line

            ntmBuilder.Append(_notam.SendTime.Substring(_notam.SendTime.Length - 6, 6) + " " +
                              _notam.Origin.Code + Environment.NewLine);
            #endregion

            ntmBuilder.Append("RQR " + _notam.FIR.Code + " " + _notam.Type + Environment.NewLine);

            if (notamList == null || notamList.Count == 0)
            {
                ntmBuilder.Append("NO VALID NOTAM IN DATABASE" + Environment.NewLine);

            }
            else
            {
                ntmBuilder.Append(CreateCheckList(notamList));

            }

            ntmBuilder.Append(" " + closePar);

            return ntmBuilder.ToString();

        }


        public void SendRQNNotam(string AFTNHeader)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings.Get("NotamPath").ToString();
            string strSend = GenerateRQNMessage(AFTNHeader);
            var time = DateTime.Now.ToString("HHmmss")  +"00.00L";
            path += time;
            File.WriteAllText(path, strSend);
            string newName = path.Replace(".00L", ".00G");
            File.Move(path, newName);
            string strMsg = "RQN "+_notam.Type+_notam.Number+"/"+_notam.Year+" Sent successfully to "+AFTNHeader;
            MessageBox.Show(strMsg);
        }

        public void SendRQLNotam(string AFTNHeader, List<Notam> notamList)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings.Get("NotamPath").ToString();
            string strSend = GenerateRQLMessage(AFTNHeader,notamList);
            var time = DateTime.Now.ToString("HHmmss") + "00.00L";
            path += time;
            File.WriteAllText(path, strSend);
            string newName = path.Replace(".00L", ".00G");
            File.Move(path, newName);
            string strMsg = "RQL " + _notam.Type +  " Sent successfully to " + AFTNHeader;
            MessageBox.Show(strMsg);
        }


        public static  string CreateCheckList(List<Notam> notamList)
        {
            var queryYear = notamList.GroupBy(item => item.Year).Select(group =>
                                    new
                                    {
                                        Year = group.Key,
                                        nums = group.OrderBy(x => x.Number)
                                    }).OrderBy(group => group.Year);
            //                newTable.Rows.Add(newTable.Rows[1]);
            int icol=1;
            string strItemE = "";
            foreach (var yearItem in queryYear)
            {
                strItemE += "\r\nYEAR 20" + yearItem.Year + ": ";
                foreach (var nt in yearItem.nums)
                {
                    strItemE += nt.Number + " ";
                    icol++;
                    if (icol > 11)
                    {
                        icol = 1;
                        strItemE += "\r\n           ";

                    }
                }
                icol = 1;
            }
            return strItemE;


        }




    }
}
