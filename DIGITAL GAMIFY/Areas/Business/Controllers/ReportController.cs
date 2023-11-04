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
    public class ReportController : Controller
    {
        public ActionResult Customers()
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
        public ActionResult GameResult()
        {
            return View();
        }
        public ActionResult GuestCheckIn()
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
        public ActionResult SwipeGamePrizes()
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
        public ActionResult SurveyPrizes()
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
        public ActionResult SmartQuizPrizes()
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
        public ActionResult QuizPrizes()
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
        public ActionResult SurveyResults()
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
        public ActionResult Export()
        {
            return View();
        }
        public ActionResult SweepstakesResult()
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
	}
}