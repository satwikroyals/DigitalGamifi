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
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;


namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    public class SurveyController : Controller
    {
        SurveyManager objsm = new SurveyManager();
        [ActionName("ViewSurveys")]
        public ActionResult ViewSurveys()
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
        [ActionName("CreateSurvey")]
        public ActionResult CreateSurvey()
        {
            Int32 id = 0;
            if (Request.Params["id"] != null)
            {
                id = Convert.ToInt32(Request.Params["id"]);
                ViewBag.surveyid = id;
            }
            else
            {
                ViewBag.Surveyid = 0;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSurvey(SurveyEntity sEntity, string[] Attributes, string[] attributes2)
        {
            Int32 adminid = 0;
            Int32 businessid = 0;
            HttpCookie busicookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (busicookie != null)
            {
                adminid = Convert.ToInt32(busicookie[Globalsettings.CookieBusiAdminId]);
                businessid = Convert.ToInt32(busicookie[Globalsettings.CookieBusinessId]);
            }
            sEntity.AdminId = adminid;
            sEntity.BusinessId = businessid;
            HttpPostedFileBase Surveyimg = Request.Files["Surveyimg"];
            HttpPostedFileBase firstprize = Request.Files["FirstPrizeImagefile"];
            HttpPostedFileBase secondprize = Request.Files["SecondPrizeImagefile"];
            StatusResponse se = new StatusResponse();

            sEntity.QRCode = "QR.jpg";
            if (Surveyimg != null && Surveyimg.ContentLength > 0)
            {
                sEntity.Surveyimage = Guid.NewGuid().ToString() + Path.GetExtension(Surveyimg.FileName).ToLower();
            }
            if (firstprize != null && firstprize.ContentLength > 0)
            {
                sEntity.FirstPrizeImage = Guid.NewGuid().ToString() + Path.GetExtension(firstprize.FileName).ToLower();
            }
            if (secondprize != null && secondprize.ContentLength > 0)
            {
                sEntity.SecondPrizeImage = Guid.NewGuid().ToString() + Path.GetExtension(secondprize.FileName).ToLower();
            }
            string AttriId = "";
            string AttriId2 = "";
            string attrivalue = "";
            string attricomma = "";
            if (Attributes != null)
            {
                foreach (string attri in Attributes)
                {
                    attrivalue = Request.Form["attributes_" + attri];
                    attricomma = attrivalue.Replace(",", ";");
                    if (attrivalue != "")
                    {
                        AttriId += attri + "[" + attricomma + "]" + "";
                    }
                }
            }
            sEntity.Attributes1 = AttriId;
            if (attributes2 != null)
            {
                foreach (string attri in attributes2)
                {
                    attrivalue = Request.Form["attributes2_" + attri];
                    attricomma = attrivalue.Replace(",", ";");
                    if (attrivalue != "")
                    {
                        AttriId2 += attri + "[" + attricomma + "]" + "";
                    }
                }
            }
            sEntity.Attributes2 = AttriId2;
            se = objsm.AddSurvey(sEntity);
            if (Surveyimg != null && se.StatusCode > 0)
            {
                if (Surveyimg != null && Surveyimg.ContentLength > 0)
                {
                    string filename = sEntity.Surveyimage.ToString();
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/surveyimages/" + se.StatusCode.ToString() + "/"));
                    string folder = Server.MapPath("~/ApplicationFiles/surveyimages/" + se.StatusCode.ToString() + "/");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    Surveyimg.SaveAs(folder + filename);
                }
                if (se.StatusCode > 0 && firstprize != null && firstprize.ContentLength > 0)
                {
                    string filename = sEntity.FirstPrizeImage.ToString();
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/surveyprizes/" + se.StatusCode.ToString() + "/"));
                    string folder = Server.MapPath("~/ApplicationFiles/surveyprizes/" + se.StatusCode.ToString() + "/");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    firstprize.SaveAs(folder + filename);
                }
                if (se.StatusCode > 0 && secondprize != null && secondprize.ContentLength > 0)
                {
                    string filename = sEntity.SecondPrizeImage.ToString();
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/surveyprizes/" + se.StatusCode.ToString() + "/"));
                    string folder = Server.MapPath("~/ApplicationFiles/surveyprizes/" + se.StatusCode.ToString() + "/");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    secondprize.SaveAs(folder + filename);
                }
                if (sEntity.SurveyId == 0)
                {
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/surveyimages/" + se.StatusCode.ToString() + "/"));
                    string QRCodeUrl = Globalsettings.GetSurveyQrCodeUrl(se.StatusCode);

                    QRCodeEncoder encoder = new QRCodeEncoder();
                    encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    encoder.QRCodeScale = 10;
                    Bitmap img = encoder.Encode(QRCodeUrl);
                    Graphics g = Graphics.FromImage(img);
                    img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
                }
            }
            if (se.StatusCode > 0)
            {
                if (Convert.ToString(Request.Form["txtdataqa"]) != "")
                {
                    sEntity.QandAValues = Convert.ToString(Request.Form["txtdataqa"]);
                    sEntity.SurveyId = se.StatusCode;
                    objsm.AddUpdateSurveyQandA(sEntity);
                }
                //if (Convert.ToString(Request.Form["txtremovedataq"]) != "")
                //{
                //    sEntity.SurveyquestionIds = Convert.ToString(Request.Form["txtremovedataq"]);
                //    objsm.DeleteQuestions(sEntity);
                //}
                //if(Convert.ToString(Request.Form["txtremovedataa"])!="")
                //{
                //    sEntity.SurveyanswerIds = Convert.ToString(Request.Form["txtremovedataa"]);
                //    objsm.DeleteAnswers(sEntity);
                //}
            }
            else
            {
                ModelState.AddModelError("Failed", se.StatusMessage);
                return View(sEntity);
            }
            return RedirectToAction("ViewSurveys", "Survey", new { area = "Business" });
        }
	}
}