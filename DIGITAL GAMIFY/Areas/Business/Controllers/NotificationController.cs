using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;
using DIGITAL_GAMIFY.Entities;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    public class NotificationController : Controller
    {
        [ActionName("ViewNotifications")]
        public ActionResult ViewNotifications()
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
        [ActionName("CreateNotification")]
        public ActionResult CreateNotification()
        {
            Int32 businessid = 0;
            NotificationManager bal = new NotificationManager();
            NotificationEntity ne = new NotificationEntity();
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["_n"]))
            {
                ne = bal.GetNotificationById(Convert.ToInt32(Request.QueryString["_n"]));
            }  
            ViewBag.id = businessid;
            return View(ne);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken()]
        public ActionResult Add(HttpPostedFileBase ImageFile,NotificationEntity p,string[] CustomerId)
        {
            //if (ModelState.IsValid)
            //{
            Int32 adminid = 0;
            Int32 businessid = 0;
            string Cid = "";
            HttpCookie buscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (buscookie != null)
            {
                adminid = Convert.ToInt32(buscookie[Globalsettings.CookieBusiAdminId]);
                businessid = Convert.ToInt32(buscookie[Globalsettings.CookieBusinessId]);
            }  
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                p.Image = Path.GetFileName(ImageFile.FileName.Replace(" ",""));
            }
            p.BusinessId = businessid;
            p.AdminId = adminid;
             if (CustomerId != null)
             {
                 foreach (string Customer in CustomerId)
                 {
                     Cid += Customer + ",";
                 }
             }
             p.CustomerIds = Cid;

            NotificationManager bal = new NotificationManager();
            var res = bal.AddNotification(p);

            DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["NotificationImagesPath"] + res.NotificationId.ToString() + "/"));
            string folder = Server.MapPath(ConfigurationManager.AppSettings["NotificationImagesPath"] + res.NotificationId.ToString() + "/");
            if (!dir.Exists)
            {
                dir.Create();
            }

            if (res.NotificationId > 0)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    ImageFile.SaveAs(folder + p.Image);
                }
            }

            //}
            return RedirectToAction("ViewNotifications", "Notification");
        }
        public ActionResult PushNotifications()
        {
            return View();
        }
        public ActionResult CreatePushNotification()
        {
            Int32 businessid = 0;
            NotificationManager bal = new NotificationManager();
            PushNotificationEntity ne = new PushNotificationEntity();
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["_n"]))
            {
                ne = bal.GetPushNotificationById(Convert.ToInt32(Request.QueryString["_n"]));
            }
            ViewBag.id = businessid;
            return View(ne);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken()]
        public ActionResult CreatePushNotification(PushNotificationEntity p, string[] CustomerId)
        {
            //if (ModelState.IsValid)
            //{
            Int32 adminid = 0;
            Int32 businessid = 0;
            string Cid = "";
            HttpCookie buscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (buscookie != null)
            {
                adminid = Convert.ToInt32(buscookie[Globalsettings.CookieBusiAdminId]);
                businessid = Convert.ToInt32(buscookie[Globalsettings.CookieBusinessId]);
            }           
            p.BusinessId = businessid;
            p.AdminId = adminid;
            if (CustomerId != null)
            {
                foreach (string Customer in CustomerId)
                {
                    Cid += Customer + ",";
                }
            }
            p.CustomerIds = Cid;

            List<CustomerDeviceEntity> devices =null;

            CustomerManager cm = new CustomerManager();
           // p.CustomerIds = "10,22";
            devices=cm.GetCustomerDevicesByCustomerIds(p.CustomerIds);
            if (devices != null)
            {                
                List<CustomerDeviceEntity> androiddevicesList = devices.Where(a => a.DeviceType == 1).ToList();
                if(androiddevicesList!=null)
                {
                    string[] androiddevices = androiddevicesList.Where(a => a.DeviceId != null && a.DeviceId != "").Select(a => a.DeviceId).ToArray();
                    Globalsettings.AndroidPushNotifications(androiddevices, p.Message, p.Title, p.Url);
                }
                List<CustomerDeviceEntity> iosdevicesList = devices.Where(a => a.DeviceType == 2).ToList();
                if (iosdevicesList != null)
                {
                    string[] iosdevices = iosdevicesList.Where(a => a.DeviceId != null && a.DeviceId != "").Select(a => a.DeviceId).ToArray();
                    Globalsettings.IPhonePushNotifications(iosdevices,p.Message, p.Title, p.Url);
                }

                NotificationManager bal = new NotificationManager();
                var res = bal.AddPushNotification(p);

            }

            

            //}
            return RedirectToAction("PushNotifications", "Notification");
        }
	}
}