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

namespace DIGITAL_GAMIFY.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("Register")]
        public ActionResult Register()
        {
            BusinessEntity b = new BusinessEntity();
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

            p.AdminId = adminid == 0 ? 1 : adminid;
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
            return RedirectToAction("Register", "Home");
        }
    }
}