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
    public class GuestController : Controller
    {
        CustomerManager cusm = new CustomerManager();
        public ActionResult Register()
        {
            ViewBag.url = Session["Url"];
            ViewBag.logo = Session["Logo"];
            ViewBag.bname = Session["Bname"];
            ViewBag.bid = Session["bid"];
            ViewBag.desc = Session["Desc"];
            ViewBag.Gimage = Session["Gimage"];
            ViewBag.Age = Session["Isage"];
            ViewBag.cond = Session["cond"];
            ViewBag.agecondition = Session["agecond"];
            ViewBag.sweep = Session["sweepstakes"];
            return View();
        }
        [HttpPost]
        public ActionResult RegisterGuest(CustomerEntity ce)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StatusEntity se = new StatusEntity();
                    se = cusm.RegisterGuest(ce);
                    ViewBag.id = se.Id;
                    return RedirectToAction("Register");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("Register"); 
        }
        public ActionResult GameDetails(Int32 gid)
        {
            SwipeandWinEntity swe = new SwipeandWinEntity();
            SwipeandWinManager objsm = new SwipeandWinManager();
            swe = objsm.GetSwipeandWinById(gid);
            Int32 cid = 0;
            if (Request.Params["cid"] != null)
            {
                cid = Convert.ToInt32(Request.Params["cid"]);
            }
            var url = Request.Url.AbsoluteUri;
            if (cid == 0)
            {
                Session["Url"] = url;
                Session["Logo"] = swe.LogoPath;
                Session["bid"] = swe.BusinessId;
                Session["Bname"] = swe.BusinessName;
                Session["Desc"] = swe.Description;
                Session["Gimage"] = swe.ImagePath;
                Session["Isage"] = swe.IsAgeRequire;
                Session["cond"] = swe.Conditions;
                Session["agecond"] = swe.AgeConditionstring;
                Session["sweepstakes"] = 0;
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.url = url;
                ViewBag.gid = gid;
                return View();
            }
           
        }
        public ActionResult Finish()
        {
            Int32 id = 0;
            if (Request.Params["id"] != null)
            {
                BusinessEntity be = new BusinessEntity();
                BusinessManager bm = new BusinessManager();
                id = Convert.ToInt32(Request.Params["id"]);
                be = bm.GetBusinessById(id);
                ViewBag.logo = be.BusinessLogoPath;
            }
            else
            {
                ViewBag.logo = "";
            }
            return View();
        }
        public ActionResult Survey(Int32 id)
        {
            SurveyManager sm = new SurveyManager();
            SurveyQuestionandAnswer sqm = new SurveyQuestionandAnswer();
            sqm = sm.GetAdminSurveybyId(id);
            Int32 cid = 0;
            if (Request.Params["cid"] != null)
            {
                cid = Convert.ToInt32(Request.Params["cid"]);
            }
            //ViewBag.bid = bid;
            var url = Request.Url.AbsoluteUri;
            if (cid == 0)
            {
                Session["Url"] = url;
                Session["Logo"] = sqm.SurveyDetails.LogoPath;
                Session["Bname"] = sqm.SurveyDetails.BusinessName;
                Session["bid"] = sqm.SurveyDetails.BusinessId;
                Session["Desc"] = sqm.SurveyDetails.ShortDescription;
                Session["Gimage"] = sqm.SurveyDetails.Surveyimagepath;
                Session["Isage"] = sqm.SurveyDetails.IsAgeRequire;
                Session["cond"] = sqm.SurveyDetails.Conditions;
                Session["agecond"] = sqm.SurveyDetails.AgeConditionstring;
                Session["sweepstakes"] = 0;
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.url = url;
                return View();
            }
        }
        public ActionResult Quiz(Int32 id)
        {
            QuizManager qm = new QuizManager();
            QuizQuestionAndAnswer qa = new QuizQuestionAndAnswer();
            qa = qm.getAdminQuizById(id);
            Int32 cid = 0;
            if (Request.Params["cid"] != null)
            {
                cid = Convert.ToInt32(Request.Params["cid"]);
            }
            //ViewBag.bid = bid;
            var url = Request.Url.AbsoluteUri;
            if (cid == 0)
            {
                Session["Url"] = url;
                Session["Logo"] = qa.QuizDetails.LogoPath;
                Session["Bname"] = qa.QuizDetails.BusinessName;
                Session["bid"] = qa.QuizDetails.BusinessId;
                Session["Desc"] = qa.QuizDetails.ShortDescription;
                Session["Gimage"] = qa.QuizDetails.QuizImagepath;
                Session["Isage"] = qa.QuizDetails.IsAgeRequire;
                Session["cond"] = qa.QuizDetails.Conditions;
                Session["agecond"] = qa.QuizDetails.AgeConditionstring;
                Session["sweepstakes"] = 0;
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.url = url;
                return View();
            }
        }
        public ActionResult SmartQuiz(Int32 id)
        {
            SmartQuizManager sqm = new SmartQuizManager();
            SmartQuizQuestionAndAnswer sqam = new SmartQuizQuestionAndAnswer();
            sqam = sqm.getAdminSmartQuizById(id);
            Int32 cid = 0;
            if (Request.Params["cid"] != null)
            {
                cid = Convert.ToInt32(Request.Params["cid"]);
            }
            //ViewBag.bid = bid;
            var url = Request.Url.AbsoluteUri;
            if (cid == 0)
            {
                Session["Url"] = url;
                Session["Logo"] = sqam.SmartQuizDetails.LogoPath;
                Session["Bname"] = sqam.SmartQuizDetails.BusinessName;
                Session["bid"] = sqam.SmartQuizDetails.BusinessId;
                Session["Desc"] = sqam.SmartQuizDetails.ShortDescription;
                Session["Gimage"] = sqam.SmartQuizDetails.SmartQuizImagepath;
                Session["Isage"] = sqam.SmartQuizDetails.IsAgeRequire;
                Session["cond"] = sqam.SmartQuizDetails.Conditions;
                Session["agecond"] = sqam.SmartQuizDetails.AgeConditionstring;
                Session["sweepstakes"] = 0;
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.url = url;
                return View();
            }
        }
        public ActionResult Sweepstakes(Int32 id)
        {
            SweepstakesManager sqm = new SweepstakesManager();
            SweepstakesEntity sqam = new SweepstakesEntity();
            sqam = sqm.GetSweepstakesById(id);
            Int32 cid = 0;
            if (Request.Params["cid"] != null)
            {
                cid = Convert.ToInt32(Request.Params["cid"]);
            }
            var url = Request.Url.AbsoluteUri;
            if (cid == 0)
            {
                Session["Url"] = url;
                Session["Logo"] = sqam.LogoPath;
                Session["Bname"] = sqam.BusinessName;
                Session["bid"] = sqam.BusinessId;
                Session["Desc"] = sqam.ShortDescription;
                Session["Gimage"] = sqam.GameImagepath;
                Session["Isage"] = sqam.IsAgeRequire;
                Session["cond"] = sqam.Conditions;
                Session["agecond"] = sqam.AgeConditionstring;
                Session["sweepstakes"] = 1;
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.logo = sqam.LogoPath;
                ViewBag.gname = sqam.GameName;
                ViewBag.url = url;
                return View();
            }
        }
	}
}