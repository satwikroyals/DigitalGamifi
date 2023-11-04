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
    [Authorize]
    public class RestaurantsController : Controller
    {

        [ActionName("View")]
        public ActionResult ViewRestaurants()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            BusinessEntity b = new BusinessEntity();
            if (!string.IsNullOrEmpty(Request.QueryString["_b"]))
            {
                BusinessManager bal = new BusinessManager();
                b = bal.GetBusinessById(Convert.ToInt32(Request.QueryString["_b"]));
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
                p.Logo = Path.GetFileName(LogoFile.FileName.Replace(" ",""));
            }

            if (PrizeImageFile != null && PrizeImageFile.ContentLength > 0)
            {
                p.PrizeImage = "thirdprize" + Path.GetExtension(PrizeImageFile.FileName.Replace(" ","")); // @System.DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace("/", "").Replace("\\", "").Replace(":", "") + Path.GetExtension(PrizeImageFile.FileName); 
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
                    string filename = Path.GetFileName(LogoFile.FileName.Replace(" ",""));
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
            return RedirectToAction("View", "Restaurants");
        }

    }
}