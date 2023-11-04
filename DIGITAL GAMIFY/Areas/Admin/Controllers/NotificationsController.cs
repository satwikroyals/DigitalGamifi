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

namespace DIGITAL_GAMIFY.Areas.Admin.Controllers
{
    public class NotificationsController : Controller
    {
        [ActionName("View")]
        public ActionResult ViewNotifications()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Add()
        {
            NotificationEntity n = new NotificationEntity();
            if (!string.IsNullOrEmpty(Request.QueryString["_n"]))
            {
                NotificationManager bal = new NotificationManager();
                n = bal.GetNotificationById(Convert.ToInt64(Request.QueryString["_n"]));
            }
            return View(n);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Add(NotificationEntity p)
        {
            //if (ModelState.IsValid)
            //{
            Int32 adminid = 0;
            HttpCookie admcookie = Request.Cookies[Globalsettings.AdminCookiename];
            if (admcookie != null)
            {
                adminid = Convert.ToInt32(admcookie[Globalsettings.CookieAdminId]);
            }
            NotificationManager bal = new NotificationManager();
            var res = bal.AddNotification(p);

            //}
            return RedirectToAction("View", "Notifications");
        }



    }
}