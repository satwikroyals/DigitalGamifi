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
using System.Drawing.Drawing2D;
using System.Web.Helpers;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    [Authorize]
    public class SmartQuizController : Controller
    {
        private SmartQuizManager objsqm = new SmartQuizManager();
        [ActionName("ViewSmartQuiz")]
        public ActionResult ViewSmartQuiz()
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
        [ActionName("AddSmartQuiz")]
        public ActionResult AddSmartQuiz()
        {
            Int32 id = 0;
            SmartQuizQuestionAndAnswer sqa = new SmartQuizQuestionAndAnswer();
            if (Request.Params["id"] != null)
            {
                id = Convert.ToInt32(Request.Params["id"]);
                sqa = objsqm.getAdminSmartQuizById(id);
            }
            else
            {
                sqa.SmartQuizDetails = new SmartQuizEntity();
                sqa.Question = new List<SmartQuizQuestions>();
            }
            return View(sqa);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSmartQuiz(SmartQuizEntity sqEntity, string[] Attributes, string[] attributes2)
        {
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
            StatusResponse se = new StatusResponse();
            SmartQuizCustomerQuestion sqobj = new SmartQuizCustomerQuestion();
            SmartQuizAnswers sqaobj = new SmartQuizAnswers();
            HttpPostedFileBase smartimagefile = Request.Files["QuizImage"];
            HttpPostedFileBase firstprize = Request.Files["FirstPrizeImagefile"];
            HttpPostedFileBase secondprize = Request.Files["SecondPrizeImagefile"];
            sqEntity.QRCode = "QR.jpg";
            if (smartimagefile != null && smartimagefile.ContentLength > 0)
            {
                sqEntity.SmartQuizImage = Guid.NewGuid().ToString() + Path.GetExtension(smartimagefile.FileName).ToLower();
            }
            if (firstprize != null && firstprize.ContentLength > 0)
            {
                sqEntity.FirstPrizeImage = Guid.NewGuid().ToString() + Path.GetExtension(firstprize.FileName).ToLower(); 
            }
            if (secondprize != null && secondprize.ContentLength > 0)
            {
                sqEntity.SecondPrizeImage = Guid.NewGuid().ToString() + Path.GetExtension(secondprize.FileName).ToLower();
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
            sqEntity.Attributes1 = AttriId;
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
            sqEntity.Attributes2 = AttriId2;
            se = objsqm.AddSmartQuizGame(sqEntity);
            if (se.StatusCode > 0 && smartimagefile != null && smartimagefile.ContentLength > 0)
            {
                string filename = sqEntity.SmartQuizImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                smartimagefile.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0 && firstprize != null && firstprize.ContentLength > 0)
            {
                string filename = sqEntity.FirstPrizeImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizgameprizes/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/smartquizgameprizes/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                firstprize.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0 && secondprize != null && secondprize.ContentLength > 0)
            {
                string filename = sqEntity.SecondPrizeImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizgameprizes/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/smartquizgameprizes/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                secondprize.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0)
            {
                for (var i = 1; i <= 5; i++)
                {
                    string q = Request.Form["Question_" + i];
                    if (q != "")
                    {
                        StatusResponse seq = new StatusResponse();
                        sqobj.SmartQuizQuestionId = Convert.ToInt32(Request.Form["SmartQuizQuestionId_" + i.ToString()]);
                        sqobj.SmartQuizId = se.StatusCode;
                        sqobj.Question = Request.Form["Question_" + i];
                        sqobj.QuestionNum = i;
                        sqobj.CorrectAnswerId = Convert.ToInt16(Request.Form["CorrectAnswerId_" + i.ToString()]);
                        seq = objsqm.AddSmartQuizQuestions(sqobj);
                        for (var j = 1; j <= 3; j++)
                        {
                            HttpPostedFileBase ansimgfile = Request.Files["Answer_" + i + "_" + j];
                            if (ansimgfile.FileName != "")
                            {
                                StatusResponse sea = new StatusResponse();
                                sqaobj.SmartQuizAnswerId = Convert.ToInt32(Request.Form["SmartQuizAnswerId_" + i.ToString() + "_" + j.ToString()]);
                                sqaobj.QestionNumber = i;
                                sqaobj.SmartQuizId = se.StatusCode;
                                sqaobj.AnswerNumber = j;
                                if (ansimgfile.FileName != "")
                                {
                                    sqaobj.AnswerImage = seq.StatusCode.ToString() + "_image_" + Guid.NewGuid().ToString() + Path.GetExtension(ansimgfile.FileName); 
                                    //sqaobj.AnswerImage = Path.GetFileName(ansimgfile.FileName).Replace(" ", "").ToLower();
                                }
                                else
                                {
                                    sqaobj.AnswerImage = Request.Form["imganswer_" + i + "_" + j];
                                }
                                sea = objsqm.AddSmartQuizQuestionAnswers(sqaobj);

                                DirectoryInfo adir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/answers/"));
                                string root = Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/answers/");
                                if (!adir.Exists)
                                {
                                    adir.Create();
                                }
                                var files = ansimgfile;
                                var fileName = sqaobj.AnswerImage;
                                var path = root + fileName;
                                files.SaveAs(path);
                                // MemoryStream ms = new MemoryStream();
                                System.Web.Helpers.WebImage image = new System.Web.Helpers.WebImage(path);
                                image.Resize(80, 80,false,true);
                                image.Save(path);

                                //string ansfilename = sqaobj.AnswerImage;

                                //Stream strm = ansimgfile.InputStream;
                                //string filePathWithoutExt = Path.ChangeExtension(ansfilename, null);

                                //System.Drawing.Image oimg = System.Drawing.Image.FromStream(ansimgfile.InputStream);
                                //WebImage img = new WebImage(ansimgfile.FileName).Resize(200, 200, false, true);
                                //if (img.Width > 500)
                                //    img.Resize(250, 250);
                                //DirectoryInfo adir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/answers/"));
                                //string ansfolder = Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/answers/");
                                //if (!adir.Exists)
                                //{
                                //    adir.Create();
                                //}
                                //img.Save(ansfolder + filePathWithoutExt);
                            }
                        }
                    }
                }
                if (sqEntity.SmartQuizId == 0)
                {
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/smartquizimages/" + se.StatusCode.ToString() + "/"));
                    string QRCodeUrl = Globalsettings.GetSmartQuizQrCodeUrl(se.StatusCode);

                    QRCodeEncoder encoder = new QRCodeEncoder();
                    encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    encoder.QRCodeScale = 10;
                    Bitmap img = encoder.Encode(QRCodeUrl);
                    Graphics g = Graphics.FromImage(img);
                    img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
                }
            }
            return RedirectToAction("ViewSmartQuiz");
        }
	}
}