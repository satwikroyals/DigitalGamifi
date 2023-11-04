using SendGrid;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using DIGITAL_GAMIFY.Entities;

namespace DIGITAL_GAMIFY.Code
{
    public class Globalsettings
    {
        //private static string _websiteurl = Settings.websiteurl;
        public static string _websiteurl
        {
            get
            {
				HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static string GetWebSiteUrl()
        {
            return ConfigurationManager.AppSettings["Appurl"].ToString();
        }

        public static string GetWebSiteUrlApi()
        {
            return _websiteurl + "api/";
            //return ConfigurationManager.AppSettings["AppurlApi"].ToString();
        }

        public static string GetBusinessQrCodeUrl(Int32 bid)
        {
            return _websiteurl + "business/customer/games?id=" + bid.ToString();
        }

        public static string GetSwipeandWinQrCodeUrl(Int32 oid,Int32 gameid)
        {
            return GetWebSiteUrl() + "business/" + oid.ToString() + "/" + gameid.ToString();
        }

        public static string GetSurveyQrCodeUrl(Int32 sid)
        {
            return _websiteurl + "Guest/Survey?id=" + sid.ToString();
        }
        public static string GetQuizQrCodeUrl(Int32 qid)
        {
            return _websiteurl + "Guest/Quiz?id=" + qid.ToString();
        }
        public static string GetSweepstakesQrCodeUrl(Int32 qid)
        {
            return _websiteurl + "Guest/Sweepstakes?id=" + qid.ToString();
        }
        public static string GetSmartQuizQrCodeUrl(Int32 qid)
        {
            return _websiteurl + "Guest/SmartQuiz?id=" + qid.ToString();
        }

        public static string GetSwipeandwinQrCodeUrl(Int32 gid)
        {
            return _websiteurl + "Guest/GameDetails?gid=" + gid.ToString();
        }


        public static string GetErrorMessagePrefix()
        {
            return "Some Problem while ";
        }

        public static string appcsspath(string file)
        {
            return _websiteurl + "Content/css/" + file;
        }

        public static string appjspath(string file)
        {
            return _websiteurl + "Content/js/" + file;
        }

        public static string appimagespath(string file)
        {
            return _websiteurl + "Content/images/" + file;
        }

        public static string SetFont(string text)
        {
            return string.IsNullOrEmpty(text) == true ? "" : System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(text);
        }

        /// <summary>
        /// To set dateformat.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Dateformat(mmm d, yyyy)</returns>
        public static string SetDateFormat(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d MMM, yyyy}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }

        /// <summary>
        /// To set datetimeformat.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Datetimeformat(mmm d, yyyy HH:mm)</returns>
        public static string SetDateTimeFormat(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d MMM, yyyy h:mm:ss tt}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }
        
        /// <summary>
        /// generate 4 digit random number
        /// </summary>
        /// <returns></returns>
        public static string RandomNumber()
        {
            Random r = new Random();
            string s = Convert.ToString(r.Next(1000, 10000));
            return s;
        }
        // Generate a random string with a given size 
        /// <summary>
        /// Generate a random string with a given size with datetime milleseconds..
        /// </summary>
        /// <param name="append"></param>
        /// <returns></returns>
        public static string RandomString(string append)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < 2; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            append += builder.ToString().ToLower();
            string unixTimestamp = Convert.ToString((DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).Replace(".", "");
            return append + unixTimestamp;
        }
        public static string GetCountryTeleCode()
        {
            return ConfigurationManager.AppSettings["TeleCode"].ToString();
        }
        public static string GetSMSServer()
        {
            return ConfigurationManager.AppSettings["SMSServer"].ToString();
        }

        public static string GetSMSApiServer()
        {
            return ConfigurationManager.AppSettings["SMSApiServer"].ToString();
        }

        public static string GetSMSUsername()
        {
            return ConfigurationManager.AppSettings["SMSUsername"].ToString();
        }

        public static string GetSMSPassword()
        {
            return ConfigurationManager.AppSettings["SMSPassword"].ToString();
        }

        public static string GetSMSHeader()
        {
            return ConfigurationManager.AppSettings["SMSHeader"].ToString();
        }
        public static string sendbulksms(string fpath, string fname, string smsheader)
        {
            string result = "";
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("ACTION", "send");
            nvc.Add("USERNAME", GetSMSUsername());
            nvc.Add("PASSWORD", GetSMSPassword());
            nvc.Add("ORIGINATOR", smsheader);
            nvc.Add("FILE_LIST", fname);
            nvc.Add("FILE_HASH", GetFileMD5(fpath));
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(GetSMSApiServer());
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs = wr.GetRequestStream();
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            string file = fpath;
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "FILE_LIST", fname, "application/zip");
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[100000000];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                result = reader2.ReadToEnd();
            }
            catch
            {


                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return result;
        }
        protected static string GetFileMD5(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static string Iospushfile(string folder)
        {
            return Convert.ToString(ConfigurationManager.AppSettings["IosPushRoot"]) + (folder == "" ? "gamesnatcherz" : folder) + "/simplepush.php";
        }

        public static void AndroidPushNotifications(string[] deviceid, string message, string title, string navurl, string appkey="")
        {

            appkey = appkey == "" ? ConfigurationManager.AppSettings["AndroidPushKey"] : appkey;

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            //tRequest.ContentType = "application/x-www-form-urlencoded";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", appkey));
            String collaspeKey = Guid.NewGuid().ToString("n");

            var result = string.Join("\",\"", deviceid);
            try
            {
                var postmsg = new
                {
                    registration_ids = deviceid,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = message,
                        title = title.Replace(":", ""),
                        navurl = string.IsNullOrEmpty(navurl) ? "" : navurl
                    },
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(postmsg);
                //string testdata = string.Format("registration_id={0}&data.contentTitle={1}&data.message={2}&data.collapse_Key={3}", deviceid[0], title, message, collaspeKey);
                //string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                Stream dataStreamResponce = tResponse.GetResponseStream();

                //dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStreamResponce);
                String sResponseFromFirebaseServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch
            {
            }
        }

        public static void IPhonePushNotifications(string[] deviceids,  string message, string title, string navurl,string pushfolder="")
        {
            try
            {
                foreach (string deviceid in deviceids)
                {
                    WebRequest request = null;
                    if (navurl == "")
                    {
                        request = WebRequest.Create(Iospushfile(pushfolder) + "?did=" + deviceid + "&mess=" + message);
                    }
                    else
                    {
                        request = WebRequest.Create(Iospushfile(pushfolder) + "?did=" + deviceid + "&mess=" + message + "&url=" + navurl);
                    }
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
            }
            catch { }        
        }

        public static string AdminCookiename
        {
            get { return "gsadm"; }
        }
        public static string CookieAdminId
        {
            get { return "agsdmid"; }
        }

        public static string CookieAdminun
        {
            get { return "gsadmun"; }
        }

        public static string BusinessCookiename
        {
            get { return "gsbusi"; }
        }

        public static string CookieBusiAdminId
        {
            get { return "gsbusiadmid"; }
        }

        public static string CookieBusinessId
        {
            get { return "gsbusiid"; }
        }

        public static string CookieBusinessun
        {
            get { return "gsbusiun"; }
        }

        public static string CookieBusinessLogo
        {
            get { return "gsbusilogo"; }
        }

        public static string ReadCookie(string cookieName, string keyName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return null;
        }

        public bool IsAdminLoggedin()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.AdminCookiename] != null)
                {
                    return true;
                }
                else
                {
                    DoAdminLogOut();
                    return false;
                }
            }
            else
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.AdminCookiename] != null)
                {
                    DoAdminLogOut();
                }
                return false;
            }
        }

        /// <summary>
        /// Logout customer.
        /// </summary>
        internal void DoAdminLogOut()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.AdminCookiename] != null)
                {
                    HttpCookie oCookie = (HttpCookie)HttpContext.Current.Request.Cookies[Globalsettings.AdminCookiename];
                    oCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(oCookie);

                }
                FormsAuthentication.SignOut();
            }
            catch { }
        }

        public bool IsBusinessLoggedin()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.BusinessCookiename] != null)
                {
                    return true;
                }
                else
                {
                    DoBusinessLogOut();
                    return false;
                }
            }
            else
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.BusinessCookiename] != null)
                {
                    DoBusinessLogOut();
                }
                return false;
            }
        }

        internal void DoBusinessLogOut()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[Globalsettings.BusinessCookiename] != null)
                {
                    HttpCookie oCookie = (HttpCookie)HttpContext.Current.Request.Cookies[Globalsettings.BusinessCookiename];
                    oCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(oCookie);

                }
                FormsAuthentication.SignOut();
            }
            catch { }
        }

        public static string GetProjectName()
        {
            return "DIGITAL GAMIFY";
        }

        public static string GetCompanyName()
        {
            return "DIGITAL GAMIFY";
        }

        public static string GetCopyRight()
        {
            return "Copyright ©2021 " + Globalsettings.GetCompanyName() + ". All Rights Reserved";
        }

        public static string adminthemeimagepath(string img)
        {
            string imgpath = HttpContext.Current.Server.MapPath("~/content/images/" + img);
            if (File.Exists(imgpath))
            {
                return "~/content/images/" + img;
            }
            else { return ""; }
        }

        public static string admincsspath(string file)
        {
            return _websiteurl + "Content/css/" + file;
        }

        public static string adminjspath(string file)
        {
            return _websiteurl + "Content/js/" + file;
        }

        public static string adminimagespath(string file)
        {
            return _websiteurl + "Content/images/" + file;
        }

        public static string publicvendorfolderpath(string file)
        {
            return _websiteurl + "Content/public/vendor/" + file;
        }

        public static string publiccsspath(string file)
        {
            return _websiteurl + "Content/public/css/" + file;
        }

        public static string publicimagespath(string file)
        {
            return _websiteurl + "Content/public/img/" + file;
        }

        public static string publicjspath(string file)
        {
            return _websiteurl + "Content/public/js/" + file;
        }

        /// <summary>
        /// Gets Google maps api key.
        /// </summary>      
        /// <returns>Google maps api key</returns>
        public static string GetGoogleMapsApiKey()
        {
            //  return "AIzaSyBf1cgIlwE7ywZS1afASVyxO6n0t5erGKc";
            return ConfigurationManager.AppSettings["GooglemapApiKey"].ToString();
        }
        public static string GetGamesnatcherzEmail(Int16 prizeznum,int Type,int last)
        {
            WebClient webClient = new WebClient();
            string path = "";
            if (prizeznum == 1 || prizeznum == 2)
            {
                path = Settings.GetApplicationFilesPath("newemailtemplates", "Gamesnatcherz", "index-" + Type.ToString()+".html");
            }
            else if (prizeznum == 3 && last == 3) {
                path = Settings.GetApplicationFilesPath("newemailtemplates", "Gamesnatcherz", "index.html");
            }
            else if (prizeznum == 3 && Type == 1)
            {
                path = Settings.GetApplicationFilesPath("newemailtemplates", "Gamesnatcherz", "index-" + Type.ToString() + ".html");
            }
            else if (prizeznum == 3 && Type == 4)
            {
                path = Settings.GetApplicationFilesPath("newemailtemplates", "Gamesnatcherz", "index-" + prizeznum.ToString() + "-" + Type.ToString() + ".html");
            }
            else
            {
                path = Settings.GetApplicationFilesPath("newemailtemplates", "Gamesnatcherz", "index-" + prizeznum.ToString() + "-" + Type.ToString()+".html");
            }
            Stream stream = webClient.OpenRead(path);
            StreamReader reader = new StreamReader(stream);
            string readFile = reader.ReadToEnd();
            string StrContent = "";
            StrContent = readFile;
            return StrContent;
        }

        public static bool SendEmail(string to, string subject, string bodyText, string fromName, string bodyHtml, string file = null)
        {
            bool issend = false;

            string uname = SendGridEmailCredentials()["uname"].ToString();
            string pwd = SendGridEmailCredentials()["pwd"].ToString();
            string key = SendGridEmailCredentials()["key"].ToString();
            string from = "noreply@gamesnatcherz.com";
            //string fromName = "Gamesnatcherz";
            var message = new SendGridMessage();

            message.Subject = subject;
            message.From = new MailAddress(from, fromName);
            if (bodyHtml != null)
            {
                message.Html = bodyHtml;
            }
            if (bodyText != null)
            {
                message.Text = bodyText;
            }
            if (string.IsNullOrEmpty(to))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(file))
            {
                message.AddAttachment(file);
            }
            message.AddTo(to);
            var transportInstance = new Web(key);
            message.EnableBypassListManagement();
            transportInstance.DeliverAsync(message);
            return true;

        }

        public static bool SendBulkEmail(List<UsersLists> ul,string subject, string bodyText, string bodyHtml, string file = null)
        {
            bool issend = false;

            string uname = SendGridEmailCredentials()["uname"].ToString();
            string pwd = SendGridEmailCredentials()["pwd"].ToString();
            string key = SendGridEmailCredentials()["key"].ToString();
            string from = "noreply@gamesnatcherz.com";
            string fromName = "Gamesnatcherz";
            var message = new SendGridMessage();

            message.Subject = subject;
            message.From = new MailAddress(from, fromName);
            if (bodyHtml != null)
            {
                message.Html = bodyHtml;
            }
            if (bodyText != null)
            {
                message.Text = bodyText;
            }
            //if (string.IsNullOrEmpty(to))
            //{
            //    return false;
            //}
            if (!string.IsNullOrEmpty(file))
            {
                message.AddAttachment(file);
            }
            foreach (UsersLists user in ul)
            {
                message.AddBcc(new MailAddress(user.rcpt, user.name));
            }
            message.AddTo("developer11.kansolve@gmail.com");
            var transportInstance = new Web(key);
            message.EnableBypassListManagement();
            transportInstance.DeliverAsync(message);
            return true;

        }
        public static Dictionary<string, string> SendGridEmailCredentials()
        {
            Dictionary<string, string> cred = new Dictionary<string, string>();

            cred.Add("uname", ConfigurationManager.AppSettings["SendGridEmailUname"].ToString());
            cred.Add("pwd", ConfigurationManager.AppSettings["SendGridEmailPassword"].ToString());
            cred.Add("key", ConfigurationManager.AppSettings["SendGridKey"].ToString());

            return cred;
        }
    }
}