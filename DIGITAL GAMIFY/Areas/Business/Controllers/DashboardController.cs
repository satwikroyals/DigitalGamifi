using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            DashboardEntity de = new DashboardEntity();
            BusinessManager bm = new BusinessManager();
            Globalsettings _global = new Globalsettings();
            Int32 businessid = 0;
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }
            if (!_global.IsBusinessLoggedin())
            {
                return RedirectToAction(actionName: "Login", controllerName: "Business");
            }
            else
            {
                de = bm.GetBusinessDashboard(businessid);
                return View(de);
            }
        }
        public ActionResult Profile()
        {
            BusinessEntity b = new BusinessEntity();
            Int32 businessid = 0;
            HttpCookie businesscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (businesscookie != null)
            {
                businessid = Convert.ToInt32(businesscookie[Globalsettings.CookieBusinessId]);
            }
            if (businessid!=0)
            {
                BusinessManager bal = new BusinessManager();
                b = bal.GetBusinessById(Convert.ToInt32(businessid));
            }
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Add(HttpPostedFileBase LogoFile, HttpPostedFileBase PrizeImageFile, BusinessEntity p)
        {
            //if (ModelState.IsValid)
            //{
            Int32 adminid = 0;
            HttpCookie admcookie = Request.Cookies[Globalsettings.AdminCookiename];
            if (admcookie != null)
            {
                adminid = Convert.ToInt32(admcookie[Globalsettings.CookieAdminId]);
            }

            p.AdminId = adminid;
            if (LogoFile != null && LogoFile.ContentLength > 0)
            {
                p.Logo = Path.GetFileName(LogoFile.FileName.Replace(" ", ""));
            }

            if (PrizeImageFile != null && PrizeImageFile.ContentLength > 0)
            {
                p.PrizeImage = "thirdprize" + Path.GetExtension(PrizeImageFile.FileName.Replace(" ", "")); // @System.DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace("/", "").Replace("\\", "").Replace(":", "") + Path.GetExtension(PrizeImageFile.FileName); 
            }


            BusinessManager bal = new BusinessManager();
            var res = bal.AddBusiness(p);

            DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["BusinessImagesPath"] + res.BusinessId.ToString() + "/"));
            string folder = Server.MapPath(ConfigurationManager.AppSettings["BusinessImagesPath"] + res.BusinessId.ToString() + "/");
            if (!dir.Exists)
            {
                dir.Create();
            }

            string QRCodeUrl = Globalsettings.GetBusinessQrCodeUrl(res.BusinessId);

            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            encoder.QRCodeScale = 10;
            Bitmap img = encoder.Encode(QRCodeUrl);
            Graphics g = Graphics.FromImage(img);
            img.Save(dir + "QR.jpg", ImageFormat.Jpeg);


            if (LogoFile != null && LogoFile.ContentLength > 0)
            {
                if (res.BusinessId > 0)
                {
                    string filename = Path.GetFileName(LogoFile.FileName.Replace(" ", ""));
                    LogoFile.SaveAs(folder + filename);
                }
            }

            if (PrizeImageFile != null && PrizeImageFile.ContentLength > 0)
            {
                if (res.BusinessId > 0)
                {
                    PrizeImageFile.SaveAs(folder + p.PrizeImage);
                }
            }

            //}
            return RedirectToAction("Profile", "Dashboard");
        }
    }
}