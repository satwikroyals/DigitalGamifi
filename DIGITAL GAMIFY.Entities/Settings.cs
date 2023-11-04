using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class Settings
    {
         public static string DbConnection
         {
             get
             {
                 return ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;               
             }
         }

        /// <summary>
        /// Gets content path from web.config file.
        /// </summary>      
        /// <returns>Returns admin conent path</returns>
        public static string GetContentPath()
        {
            // return ConfigurationManager.AppSettings["Appurl"] ?? "";
            string url = HttpContext.Current.Request.Url.Authority + "/";
            if (url.IndexOf("http") == -1)
            {
                url = "http://" + url;
            }
            return url;

        }
              /// <summary>
        /// WebsiteUrl
        /// </summary>
        public static string websiteurl
        {
            get
            {
                string url = HttpContext.Current.Request.Url.Authority + "/";
                if (url.IndexOf("http") == -1)
                {
                    url = "http://" + url;
                }
                return url;
            }

        }
        /// <summary>
        /// Database provider
        /// </summary>
        public static string ProviederName
        {
            get { return "MsSql"; }
        }

         public static string SetFont(string Text)
         {
             return System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Text);
         }

        /// <summary>
        /// To set datetimeformat.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Datetimeformat(mmm d yyyy, hh:mm tt)</returns>
        public static string SetDateTimeFormat(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d MMM yyyy, hh:mm tt}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }
        /// <summary>
        /// set name formate i.e 1st char caps
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SetNameFormat(string name)
        {
            return string.IsNullOrEmpty(name) == true ? "" : System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(name);
        }
        /// <summary>
        /// convert text in to capital
        /// </summary>
        /// <param name="text"></param>
        /// <returns>capital string</returns>
        public static string ConvertIntoAllCaps(string text)
        {
            return string.IsNullOrEmpty(text) == true ? "" : text.ToUpper();
        }
        /// <summary>
        /// To set dateformat.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Datetimeformat(mmm d yyyy)</returns>
        public static string SetDateFormate(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d MMM yyyy}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }
        /// <summary>
        /// to set price formate
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>double</returns>
        public static double SetPriceFormate(double value)
        {
            string price = "";
            price = string.Format("{0:#.##}", value);
            return Convert.ToDouble(price);
        }
        /// <summary>
        /// Get the time differences in hours
        /// </summary>
        /// <param name="dt">Datetimeformat(mmm d yyyy, hh:mm tt) as string</param>
        /// <returns></returns>
        public static double GetDiffenceDatetimeWithTodayDatetime(string dt)
        {
            DateTime currentdatetime = DateTime.Now;
            string current = SetDateTimeFormat(currentdatetime);
            TimeSpan diff = Convert.ToDateTime(current) - Convert.ToDateTime(dt);
            return diff.TotalHours;
        }
        
        /// <summary>
        /// Get Images Path
        /// </summary>
        /// <returns>string</returns>
        public static string GetImagesPath()
        {
            return ("~" + ConfigurationManager.AppSettings["Images"].ToString());
        }
        public static string getSmartQuizImage(Int32 id, string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/smartquizimages/" + id.ToString() + "/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/smartquizimages/" + id.ToString() + "/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string getSmartuizAnswerImage(Int32 id, string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/smartquizimages/" + id.ToString() + "/answers/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/smartquizimages/" + id.ToString() + "/answers/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string getQuizImage(Int32 id, string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/quizimages/" + id.ToString() + "/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/quizimages/" + id.ToString() + "/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string getSweepstakeImage(Int32 id, string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/sweepstakes/" + id.ToString() + "/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/sweepstakes/" + id.ToString() + "/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string GetCommunicationImage(string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/emailcommunicationsimages/Images/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/emailcommunicationsimages/Images/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string ConvertsMilliSecondsToHoursFormat(Int64 millisecnds)
        {
            string res = "";
            int sign = millisecnds < 0 ? -1 : 1;
            millisecnds = millisecnds * sign;
            Int64 seconds = millisecnds / 1000;
            millisecnds = millisecnds % 1000;
            Int64 days = 0;
            Int64 minitues;
            // Int64 seconds;
            Int64 hours;
            //set minitues
            minitues = seconds / 60;
            seconds = seconds % 60;  //seconds

            hours = minitues / 60;  //set hours
            minitues = minitues % 60; //set minitues
            days = hours / 24; //set days
            hours = hours % 24;
            // res = (hours < 10 ? "0" : "") + hours + ":" + (minitues < 10 ? "0" : "") + minitues + ":" + (seconds < 10 ? "0" : "") + seconds;
            //res = (hours < 10 ? "0" : "") + hours + ":" + (minitues < 10 ? "0" : "") + minitues + ":" + (seconds < 10 ? "0" : "") + seconds;
            if (hours > 0)
            {
                res += hours.ToString() + (hours == 1 ? " hour" : " hours") + " & ";
            }
            if (minitues > 0)
            {
                res += minitues.ToString() + (minitues == 1 ? " min" : " mins") + " & ";
            }
            if (seconds > 0)
            {
                res += seconds.ToString() + "." + millisecnds + (seconds == 1 ? " secs" : " secs");
            }
            return res;
            //  return sign == -1 ? "- " + res : res;
        }

        /// <summary>
        /// converts seconds to hours foramate(hh:mm:ss)
        /// </summary>
        /// <param name="seconds">seconds</param>
        /// <returns>string in x hrs y min z sec formate</returns>
        public static string ConvertSecondsToHoursFormat(Int64 seconds)
        {
            string res = "";
            int sign = seconds < 0 ? -1 : 1;
            seconds = seconds * sign;
            Int64 days = 0;
            Int64 minitues;
            // Int64 seconds;
            Int64 hours;
            //set minitues
            minitues = seconds / 60;
            seconds = seconds % 60;  //seconds

            hours = minitues / 60;  //set hours
            minitues = minitues % 60; //set minitues
            days = hours / 24; //set days
            hours = hours % 24;
            // res = (hours < 10 ? "0" : "") + hours + ":" + (minitues < 10 ? "0" : "") + minitues + ":" + (seconds < 10 ? "0" : "") + seconds;
            //res = (hours < 10 ? "0" : "") + hours + ":" + (minitues < 10 ? "0" : "") + minitues + ":" + (seconds < 10 ? "0" : "") + seconds;
            if (hours > 0)
            {
                res += hours.ToString() + (hours == 1 ? "h" : "h") + " ";
            }
            if (minitues > 0)
            {
                res += minitues.ToString() + (minitues == 1 ? "m" : "m") + " ";
            }
            if (seconds > 0)
            {
                res += seconds.ToString() + (seconds == 1 ? "s" : "s");
            }
            return res;
            //  return sign == -1 ? "- " + res : res;
        }
        public static string GetTravelAgencyLogoPath(Int32 agencyId, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TravelagencyImagesPath"] + agencyId.ToString() + "/" + filename);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["TravelagencyImagesPathUrl"] + agencyId.ToString() + "/" + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "nologo.png"; }
        }
        public static string GetPackageImagePath(Int32 packageId, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PackageImagesPath"] + filename);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["PackageImagesPathUrl"] + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noimage.png"; }
        }

        public static string GetSwipeandWin3rdPrizeImagePath(Int32 businessId, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BusinessImagesPath"] + businessId.ToString() + "/" + filename);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["BusinessImagesPathUrl"] + businessId.ToString() + "/" + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noimage.png"; }
        }

        public static string GetSwipeandWin4thPrizeImagePath()
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] +"fourthprize.png");
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["SwipeandWinImagesPathUrl"] + "fourthprize.png";
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noimage.png"; }
        }

        public static string GetSwipeandWinPrizesImagePath(Int32 gameId,string fileName)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] + gameId.ToString() + "/" + fileName);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["SwipeandWinImagesPathUrl"] + gameId.ToString() + "/" + fileName;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noimage.png"; }
        }

        public static string GetSwipeandWinQRImagePath(Int32 gameId, string fileName)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] + gameId.ToString() + "/" + fileName);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["SwipeandWinImagesPathUrl"] + gameId.ToString() + "/" + fileName;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noimage.png"; }
        }

        public static string GetNotificationImagePath(Int64 nid, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["NotificationImagesPath"] + nid.ToString() + "/" + filename);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["NotificationImagesPathUrl"] + nid.ToString() + "/" + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "nologo.png"; }
        }

        public static string GetBusinessLogoPath(Int32 businessId, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BusinessImagesPath"] + businessId.ToString() + "/" + filename);
            if (File.Exists(imgpath))
            {                
                return ConfigurationManager.AppSettings["BusinessImagesPathUrl"] + businessId.ToString() + "/" + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "nologo.png"; }
        }

        public static string GetBusinessQRPath(Int32 businessId, string filename)
        {
            string imgpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BusinessImagesPath"] + businessId.ToString() + "/" + filename);
            if (File.Exists(imgpath))
            {
                return ConfigurationManager.AppSettings["BusinessImagesPathUrl"] + businessId.ToString() + "/" + filename;
            }
            else { return ConfigurationManager.AppSettings["ImagesPathUrl"] + "noqr.png"; }
        }

        public static string GetApplicationFilesPath(string rootfolder,string subpath,string filename)
        {
            if (filename == null || filename == "")
            {
                return "";//Settings.GetContentPath() + "ApplicationFiles/" + rootfolder + "/" + subpath + "/" + filename;
            }
            else
            {
                string path = Settings.GetContentPath()+"ApplicationFiles/" + rootfolder + "/" + subpath + "/" + filename;
                string filepath = HttpContext.Current.Server.MapPath("~/ApplicationFiles/"+ rootfolder + "/" + subpath + "/") + filename;
                if (File.Exists(path: filepath))
                {
                    return path;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string getsurveyimage(Int32 id, string imgname)
        {
            if (string.IsNullOrEmpty(imgname))
            {
                return "";
            }
            else
            {
                string image = HttpContext.Current.Server.MapPath("~/ApplicationFiles/surveyimages/" + id.ToString() + "/" + imgname);
                try
                {
                    if (File.Exists(image))
                    {
                        return websiteurl + "/ApplicationFiles/surveyimages/" + id.ToString() + "/" + imgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {

                    return "";
                }

            }
        }
        public static string GetQuizUrl(Int32 qid)
        {
            return websiteurl + "Guest/Quiz?id=" + qid.ToString();
        }
        public static string GetSweepstakesUrl(Int32 qid)
        {
            return websiteurl + "Guest/Sweepstakes?id=" + qid.ToString();
        }
        public static string GetSmartQuizUrl(Int32 qid)
        {
            return websiteurl + "Guest/SmartQuiz?id=" + qid.ToString();
        }
        public static string GetSurveyUrl(Int32 qid)
        {
            return websiteurl + "Guest/Survey?id=" + qid.ToString();
        }
        public static string GetSwipeandWinUrl(Int32 qid)
        {
            return websiteurl + "Guest/GameDetails?gid=" + qid.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppc">Primary Prize Count</param>
        /// <param name="spc">Secondary Prize Count</param>
        /// <param name="cpc">Consolation Prize Count</param>
        /// <param name="pwc">Primary Prize Win Count</param>
        /// <param name="swc">Secondary Prize Win Count</param>
        /// <param name="outletcount">Consolation Prize Win Count</param>
        /// <returns></returns>
        public static int getPrizeNumber(int ppc, int spc,int cpc,int pwc, int swc, int cwc)
        {
            Int64 Tpc = ppc + spc+cpc; // Total Prize Count
            Int64 Twc = pwc + swc+cwc; // Total Win Count
            Random r = new Random();
            int randomnum = r.Next(1, 3); // Select Random Prize Number
            if (Twc == 0)
            {
                return 3;
            }
            if (ppc < 4)
            {
                randomnum = 3;
            }
            int prizeid = 3; // Initialize By Default Unsusseccfull
            // 70% third prize , 20% second prize,10% first prize
            //Int64 primarywinpercentage=pwc/totalwincount*100;
            //Int64 secondprizewinpercentage=swc/totalwincount*100;
            //decimal pwratio = Convert.ToDecimal(pwc) / Convert.ToDecimal(totalwincount)*100;
            //decimal swratio = Convert.ToDecimal(swc) / Convert.ToDecimal(totalwincount)*100;
            //decimal cwratio = Convert.ToDecimal(totalwincount-(pwc+swc)) / Convert.ToDecimal(totalwincount)*100;

            decimal ppratio = Convert.ToDecimal(ppc) / Convert.ToDecimal(Tpc);
            decimal pwratio = Convert.ToDecimal(pwc) / Convert.ToDecimal(Twc);
            decimal spratio = Convert.ToDecimal(spc) / Convert.ToDecimal(Tpc);
            decimal swratio = Convert.ToDecimal(swc) / Convert.ToDecimal(Twc);
            decimal cpratio = Convert.ToDecimal(cpc) / Convert.ToDecimal(Tpc);
            decimal cwratio = Convert.ToDecimal(cwc) / Convert.ToDecimal(Twc);
            switch (randomnum)
            {
                case 1: if (pwratio < ppratio)
                    {
                        prizeid = 1;
                    }
                    else if (cwratio < cpratio)
                    {
                        prizeid = 3;
                    }
                    else if (swratio < spratio)
                    {
                        prizeid = 2;
                    }
                    else
                    {
                        prizeid = 0;
                    }
                    break;
                case 2: if (swratio < spratio)
                    {
                        prizeid = 2;
                    }
                    else if (cwratio < cpratio)
                    {
                        prizeid = 3;
                    }
                    else if (pwratio < ppratio)
                    {
                        prizeid = 1;
                    }
                    else
                    {
                        prizeid = 0;
                    }
                    break;
                case 3: if (cwratio < cpratio)
                    {
                        prizeid = 3;
                    }
                    else if (swratio < spratio)
                    {
                        prizeid = 2;
                    }
                    else if (pwratio < ppratio)
                    {
                        prizeid = 1;
                    }
                    else
                    {
                        prizeid = 0;
                    }
                    break;
                default: prizeid = 0;
                    break;
            }
            return prizeid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppc">Primary Prize Count</param>
        /// <param name="spc">Secondary Prize Count</param>
        /// <param name="cpc">Consolation Prize Count</param>
        /// <param name="pwc">Primary Prize Win Count</param>
        /// <param name="swc">Secondary Prize Win Count</param>
        /// <param name="outletcount">Consolation Prize Win Count</param>
        /// <returns></returns>
        public static int getGamePrizeNumber(int ppc, int spc, int pwc, int swc)
        {
            Int64 Tpc = ppc + spc; // Total Prize Count
            Int64 Twc = pwc + swc; // Total Win Count
            Random r = new Random();
            int randomnum = r.Next(1, 2); // Select Random Prize Number
            if (Twc == 0)
            {
                return 2;
            }
            if (ppc < 4)
            {
                randomnum = 2;
            }
            int prizeid = 3; // Initialize By Default Unsusseccfull
            // 70% third prize , 20% second prize,10% first prize
            //Int64 primarywinpercentage=pwc/totalwincount*100;
            //Int64 secondprizewinpercentage=swc/totalwincount*100;
            //decimal pwratio = Convert.ToDecimal(pwc) / Convert.ToDecimal(totalwincount)*100;
            //decimal swratio = Convert.ToDecimal(swc) / Convert.ToDecimal(totalwincount)*100;
            //decimal cwratio = Convert.ToDecimal(totalwincount-(pwc+swc)) / Convert.ToDecimal(totalwincount)*100;

            decimal ppratio = Convert.ToDecimal(ppc) / Convert.ToDecimal(Tpc);
            decimal pwratio = Convert.ToDecimal(pwc) / Convert.ToDecimal(Twc);
            decimal spratio = Convert.ToDecimal(spc) / Convert.ToDecimal(Tpc);
            decimal swratio = Convert.ToDecimal(swc) / Convert.ToDecimal(Twc);
            switch (randomnum)
            {
                case 1: if (pwratio < ppratio)
                    {
                        prizeid = 1;
                    }
                    else if (swratio < spratio)
                    {
                        prizeid = 2;
                    }
                    else
                    {
                        prizeid = 3;
                    }
                    break;
                case 2: if (swratio < spratio)
                    {
                        prizeid = 2;
                    }
                    else if (pwratio < ppratio)
                    {
                        prizeid = 1;
                    }
                    else
                    {
                        prizeid = 3;
                    }
                    break;
                default: prizeid = 3;
                    break;
            }
            return prizeid;
        }
        public static string RandomNumber()
        {
            Random r = new Random();
            string s = Convert.ToString(r.Next(1000, 10000));
            return s;
        }
        public static string SetStatus(int s)
        {
            switch (s)
            {
                case 1: return "Active";
                case 0: return "Inactive";
                default: return "Inactive";
            }
        }

    }

     public class SearchDdlEntities
     {
         public Int32 id { get; set; }
         public string text { get; set; }
     }

     public class PagingEntities
     {
         public Int32 Pi { get; set; }
         public Int32 Ps { get; set; }
     }

     public class DdlValidator : ValidationAttribute
     {
         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
         {
             if (value != null)
             {
                 string ddlvalue = value.ToString();

                 if (ddlvalue != "0")
                 {
                     return ValidationResult.Success;
                 }
                 else
                 {
                     return new ValidationResult(ErrorMessage);
                 }
             }
             else
             {
                 return new ValidationResult(ErrorMessage);
             }
         }
     }

     public class StatusEntity
     {
         public Int64 Id { get; set; }
         public string Message { get; set; }
         public string Pin { get; set; }
     } 

}
