using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.BAL;
using System.IO;
using System.Text.RegularExpressions;
using DIGITAL_GAMIFY.Code;
using System.Text;
//using Ionic.Zip;
using SendGrid;
using System.Net.Mail;
using System.Net;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    public class CommunicationController : Controller
    {
        CommunicationManager _cm = new CommunicationManager();
        [ActionName("Communications")]
        public ActionResult Communications()
        {
            Int32 businessid = 0;
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }

            ViewBag.id = businessid;
            return View();
        }
        [ActionName("CreateCommunication")]
        public ActionResult CreateCommunication()
        {
            Int32 businessid = 0;
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }

            ViewBag.id = businessid;
            return View();
        }

        [HttpPost]
        public ActionResult AddCommunication(HttpPostedFileBase QrFile, HttpPostedFileBase ImageFile, FormCollection fprm)
        {
            Int64 communicationid = Convert.ToInt64(fprm["CommunicationId"]);
            Int16 commtypeid = Convert.ToInt16(fprm["CommunicationTypeId"]);
            string selectedids = Convert.ToString(fprm["chkedcustomers"]);
            StringBuilder txtsmsfiledata = new StringBuilder();
            int iscommercial = 1;
            string recipientids = "";
            Int32? resess = 0;
            string tomail;
            string commmessage = Convert.ToString(fprm["EmailText"]);
            try
            {
                HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
                Int32 bid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
                List<UsersLists> listOfUsers = new List<UsersLists>();
                CommunicationEntity ce = new CommunicationEntity();
                string mailnavurl = Globalsettings._websiteurl;
                string smsnavurl = Globalsettings._websiteurl;
                BusinessManager _bm = new BusinessManager();
                BusinessEntity busdetial = _bm.GetBusinessById(bid);
                //HttpPostedFileBase file = Request.Files["ImageFile"];
                //HttpPostedFileBase Qrfile = Request.Files["QrFile"];
                string filename = "";
                string qrfilename = "";
                if (ImageFile != null)
                {
                    filename = Guid.NewGuid().ToString() + ".jpg";
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/emailcommunicationsimages/Images/"));
                    string folder = Server.MapPath("~/ApplicationFiles/emailcommunicationsimages/Images/");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    ImageFile.SaveAs(folder + filename);
                }
                if (QrFile != null) 
                {
                    qrfilename = Guid.NewGuid().ToString() + ".jpg";
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/emailcommunicationsimages/QrCode/"));
                    string folder = Server.MapPath("~/ApplicationFiles/emailcommunicationsimages/QrCode/");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    QrFile.SaveAs(folder + qrfilename);
                }
                ce.EmailImage = filename;
                ce.Qrcode = qrfilename;
                ce.EmailHeading = Convert.ToString(fprm["EmailHeading"]);
                ce.OpeningTextHeader = Convert.ToString(fprm["OpeningTextHeader"]);
                ce.PageTitle = Convert.ToString(fprm["Title"]);
                ce.CommunicationTypeId = commtypeid;
                ce.BusinessId = bid;
                ce.FromId = bid;
                ce.IsLiveStream = 0;
                ce.LiveStream = "";
                // ce.CreatedDate = System.DateTime.Now;
                ce.CommunicationId = 0;
                ce.FromType = 2;
                ce.Message = commmessage;
                ce.TemplateId = 0;
                ce.ReceipentCount = selectedids.TrimEnd(',').TrimStart(',').Split(',').Length;
                ce.ToIds = selectedids;
                ce.ToType = 4;
                ce.SessionId = "Email Communication";
                StatusResponse se = _cm.InsertCommunication(ce);
                List<CustomerEntity> dtmembers = null;
                dtmembers = _cm.GetCommunicationSelectedMembers(selectedids);
                if (dtmembers != null && dtmembers.Count > 0)
                {
                    foreach (CustomerEntity dr in dtmembers)
                    {
                        recipientids = recipientids + "," + Convert.ToString(dr.CustomerId);
                        tomail = Convert.ToString(dr.Email);
                        if (tomail != "")
                        {
                            listOfUsers.Add(new UsersLists { ci = Convert.ToString(dr.CustomerId), rcpt = Convert.ToString(dr.Email), name = Convert.ToString(dr.FirstName + " " + dr.LastName), navurl = mailnavurl, ressession = Convert.ToString(se.StatusCode) });
                        }
                    }
                }
                if (commtypeid == 1)
                {
                    //WebClient webClient = new WebClient();
                    //string path = Globalsettings._websiteurl + "/ApplicationFiles/emailcomminications/emailtemplate-2.html";
                    //string myString = "";
                    //Stream stream = webClient.OpenRead(path);
                    //StreamReader reader = new StreamReader(stream);
                    //string readFile = reader.ReadToEnd();
                    //myString = readFile;
                    //myString = myString.Replace("[businesslogo]", Globalsettings._websiteurl + "ApplicationFiles/emailcommunicationsimages/Images/" + ce.EmailImage);
                    //myString = myString.Replace("[businessname]", busdetial.BusinessName);
                    //myString = myString.Replace("[Qrcode]", Globalsettings._websiteurl + "ApplicationFiles/emailcommunicationsimages/QrCode/" + ce.Qrcode);
                    //myString = myString.Replace("[emailmsg]", ce.EmailImage);
                    SendEmail(listOfUsers, iscommercial, busdetial, 1, commmessage, ce.EmailImage, ce.Qrcode, ce.PageTitle);
                    //Globalsettings.SendBulkEmail(listOfUsers, ce.PageTitle, "", myString);
                }
            }
            catch (Exception ex) { }
            finally { }
            return RedirectToAction("Communications");
        }
        private void SendEmail(List<UsersLists> ul, int iscommercial, BusinessEntity be, int templateid, string emailmsg, string bannerimgpath, string qrcode, string pgtitle)
        {
            var message = new SendGridMessage();
            string myString = "";
            StreamReader reader = null;
            string applicationurl = Globalsettings._websiteurl;
            try
            {
                string templatefilepath = "";
                if (qrcode != "")
                {
                    templatefilepath = "~/ApplicationFiles/emailcomminications/emailtemplate-1.html";
                }
                else
                {
                    templatefilepath = "~/ApplicationFiles/emailcomminications/emailtemplate-2.html";
                }
                reader = new StreamReader(Server.MapPath(templatefilepath));
                string readFile = reader.ReadToEnd();
                myString = readFile;
            }
            catch { }
            finally { reader.Dispose(); }

            try
            {
                string qr="";
                if(qrcode!=""){
                    qr = Globalsettings._websiteurl + "ApplicationFiles/emailcommunicationsimages/QrCode/" + qrcode;
                }
                myString = myString.Replace("[emailimg]", Globalsettings._websiteurl + "ApplicationFiles/emailcommunicationsimages/Images/" + bannerimgpath);
                myString = myString.Replace("[businessname]", be.BusinessName);
                myString = myString.Replace("[businesslog]", be.LogoPath);
                myString = myString.Replace("[title]", pgtitle);
                myString = myString.Replace("[Qrcode]", qr);
                myString = myString.Replace("[emailmsg]", emailmsg);

                //string body = myString;
                //message.Subject = Convert.ToString(pgtitle);
                //message.From = new MailAddress(Convert.ToString(be.Email), be.BusinessName);
                //message.Html = body;
                //Int32? rcount = 0;
                //message.To = new MailAddress[] { new MailAddress(Convert.ToString(be.Email), be.BusinessName) };

                foreach (UsersLists user in ul)
                {
                    myString = myString.Replace("[cname]", user.name);
                    myString = myString.Replace("[cid]", user.ci);
                    myString = myString.Replace("[comid]", user.ressession);
                    //message.AddCc(new MailAddress(user.rcpt, user.name));
                    //rcount += 1;
                    Globalsettings.SendEmail(user.rcpt, pgtitle, "", be.BusinessName, myString);
                }
            }
            catch { }
            finally
            {
                try
                {
                    reader.Dispose();
                }
                catch { }
            }

            //string creduname = Globalsettings.SendGridEmailCredentials()["uname"].ToString();
            //string credpwd = Globalsettings.SendGridEmailCredentials()["pwd"].ToString();
            //string key = Globalsettings.SendGridEmailCredentials()["key"].ToString();

            //var transportInstance = new Web(key);
            //message.EnableBypassListManagement();
            //transportInstance.DeliverAsync(message);
        }
	}
}
























      