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
    public class QuizController : Controller
    {
        QuizManager objsqm = new QuizManager();
        [ActionName("ViewQuiz")]
        public ActionResult ViewQuiz()
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
        [ActionName("AddQuiz")]
        public ActionResult AddQuiz()
        {
            Int32 id = 0;
            QuizQuestionAndAnswer sqa = new QuizQuestionAndAnswer();
            if (Request.Params["id"] != null)
            {
                id = Convert.ToInt32(Request.Params["id"]);
                sqa = objsqm.getAdminQuizById(id);
            }
            else
            {
                sqa.QuizDetails = new QuizEntity();
                sqa.Question = new List<QuizQuestions>();
            }
            return View(sqa);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddQuiz(QuizEntity sqEntity, string[] Attributes, string[] attributes2)
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
            QuizCustomerQuestion sqobj = new QuizCustomerQuestion();
            QuizAnswers sqaobj = new QuizAnswers();
            sqEntity.QRCode = "QR.jpg";
            HttpPostedFileBase smartimagefile = Request.Files["QuizImagefile"];
            HttpPostedFileBase firstprize = Request.Files["FirstPrizeImagefile"];
            HttpPostedFileBase secondprize = Request.Files["SecondPrizeImagefile"];
            if (smartimagefile != null && smartimagefile.ContentLength > 0)
            {
                sqEntity.QuizImage = Guid.NewGuid().ToString() + Path.GetExtension(smartimagefile.FileName).ToLower();
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
            se = objsqm.AddQuizGame(sqEntity);
            if (se.StatusCode > 0 && smartimagefile != null && smartimagefile.ContentLength > 0)
            {
                string filename = sqEntity.QuizImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/quizimages/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/quizimages/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                smartimagefile.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0 && firstprize != null && firstprize.ContentLength > 0)
            {
                string filename = sqEntity.FirstPrizeImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/quizgameprizes/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/quizgameprizes/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                firstprize.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0 && secondprize != null && secondprize.ContentLength > 0)
            {
                string filename = sqEntity.SecondPrizeImage.ToString();
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/quizgameprizes/" + se.StatusCode.ToString() + "/"));
                string folder = Server.MapPath("~/ApplicationFiles/quizgameprizes/" + se.StatusCode.ToString() + "/");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                secondprize.SaveAs(folder + filename);
            }
            if (se.StatusCode > 0)
            {
                for (var i = 1; i <= 10; i++)
                {
                    string q = Request.Form["Question_" + i];
                    if (q != "")
                    {
                        StatusResponse seq = new StatusResponse();
                        sqobj.QuizQuestionId = Convert.ToInt32(Request.Form["QuizQuestionId_" + i.ToString()]);
                        sqobj.QuizId = se.StatusCode;
                        sqobj.Question = Request.Form["Question_" + i];
                        sqobj.QuestionNum = i;
                        sqobj.CorrectAnswerId = Convert.ToInt16(Request.Form["CorrectAnswerId_" + i.ToString()]);
                        seq = objsqm.AddSmartQuizQuestions(sqobj);
                        for (var j = 1; j <= 4; j++)
                        {
                            string ansimgfile = Request.Form["Answer_" + i + "_" + j];
                            if (ansimgfile != "")
                            {
                                StatusResponse sea = new StatusResponse();
                                sqaobj.QuizAnswerId = Convert.ToInt32(Request.Form["QuizAnswerId_" + i.ToString() + "_" + j.ToString()]);
                                sqaobj.QestionNumber = i;
                                sqaobj.QuizId = se.StatusCode;
                                sqaobj.AnswerNumber = j;
                                sqaobj.Answer = Request.Form["Answer_" + i + "_" + j];
                                sea = objsqm.AddQuizQuestionAnswers(sqaobj);
                            }
                        }
                    }
                }
                if (sqEntity.QuizId == 0)
                {
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/ApplicationFiles/quizimages/" + se.StatusCode.ToString() + "/"));
                    string QRCodeUrl = Globalsettings.GetQuizQrCodeUrl(se.StatusCode);

                    QRCodeEncoder encoder = new QRCodeEncoder();
                    encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    encoder.QRCodeScale = 10;
                    Bitmap img = encoder.Encode(QRCodeUrl);
                    Graphics g = Graphics.FromImage(img);
                    img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
                }
            }
            return RedirectToAction("ViewQuiz");
        }

        public ActionResult QuizResult()
        {
            return View();
        }
	}
}