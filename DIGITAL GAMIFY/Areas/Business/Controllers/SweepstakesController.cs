using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.Code;
using System.IO;
using System.Text.RegularExpressions;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    public class SweepstakesController : Controller
    {
        SweepstakesManager objsqm = new SweepstakesManager();
        [ActionName("ViewSweepstakes")]
        public ActionResult ViewSweepstakes()
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
        [ActionName("AddSweepstakes")]
        public ActionResult AddSweepstakes()
        {
            Int32 id = 0;
            SweepstakesEntity sqa = new SweepstakesEntity();
            if (Request.Params["id"] != null)
            {
                id = Convert.ToInt32(Request.Params["id"]);
                sqa = objsqm.GetSweepstakesById(id);
            }
            return View(sqa);
        }
        [HttpPost]
        public ActionResult AddSweepstakes(SweepstakesEntity sqEntity)
        {
            StatusResponse st = new StatusResponse();
            Int32 adminid = 0;
            Int32 businessid = 0;
            HttpCookie busicookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (busicookie != null)
            {
                adminid = Convert.ToInt32(busicookie[Globalsettings.CookieBusiAdminId]);
                businessid = Convert.ToInt32(busicookie[Globalsettings.CookieBusinessId]);
            }
            sqEntity.AdminId = adminid;
            sqEntity.BusinessId = businessid;
            sqEntity.QRCode = "QR.jpg";
            HttpPostedFileBase gameimagefile = Request.Files["QuizImagefile"];
            if (gameimagefile != null && gameimagefile.ContentLength > 0)
            {
                sqEntity.GameImage = Guid.NewGuid().ToString() + Path.GetExtension(gameimagefile.FileName).ToLower();
            }
            st = objsqm.AddSweepstakes(sqEntity);
            if (st.StatusCode > 0 && gameimagefile != null && gameimagefile.ContentLength > 0)
            {
                string filename = sqEntity.GameImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/sweepstakes/" + st.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/sweepstakes/" + st.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                gameimagefile.SaveAs(folder + filename);
            }
            if (sqEntity.GameId == 0)
            {
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/sweepstakes/" + st.StatusCode.ToString() + "/"));
                string QRCodeUrl = Globalsettings.GetSweepstakesQrCodeUrl(st.StatusCode);

                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder.QRCodeScale = 10;
                Bitmap img = encoder.Encode(QRCodeUrl);
                Graphics g = Graphics.FromImage(img);
                img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
            }
            return RedirectToAction("ViewSweepstakes");
        }
	}
}