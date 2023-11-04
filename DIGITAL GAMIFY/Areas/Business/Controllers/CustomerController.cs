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
    public class CustomerController : Controller
    {
        public ActionResult Games(Int32 id)
        {
            //Int32 id = 0;
            //if (Request.Params["id"] != null)
            //{
            //    id = Convert.ToInt32(Request.Params["id"]);
            //}
            ViewBag.id = id;
            return View();
        }
        public ActionResult GameDetails(Int32 gid)
        {
            Int32 bid = 0;
            if (Request.Params["bid"] != null)
            {
                bid = Convert.ToInt32(Request.Params["bid"]);
            }
            ViewBag.bid = bid;
            ViewBag.gid = gid;
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Finish()
        {
            return View();
        }
        public ActionResult EndGame()
        {
            Int64 GameId = Convert.ToInt64(Request.Params["GId"]);
            Int16 PrizeNumber = Convert.ToInt16(Request.Params["PZNum"]);
            Int64 OutletId = Convert.ToInt64(Request.Params["OutletId"]);
            Session["isgameplay"] = "1";
            Session["gameid"] = GameId;
            Session["prizenumber"] = PrizeNumber;
            Session["outletid"] = OutletId;
            //if (Request.Cookies["clog"] == null)
            //{
                return RedirectToAction("Register", "Customer");
            //}
            //else
            //{
            //    Session["cid"] = Request.Cookies["clog"]["cid"];
            //    Session["cname"] = Request.Cookies["clog"]["cname"];
            //    TempData["Redeemed"] = 1;
            //    return RedirectToAction("RedeemPrize", "Home");
            //}
        }

        public ActionResult RedeemPrize()
        {
            if (TempData["Redeemed"] != null)
            {
                ViewBag.gameid = Session["gameid"];
                ViewBag.outletid = Session["outletid"];
                ViewBag.prizenumber = Session["prizenumber"];
                ViewBag.cid = Session["cid"];
                ViewBag.cname = Session["cname"];
                string emailstring = "";
                string cusemail = "";
                //StatusEntity se = new CustomerManager().RedeemPrize(Convert.ToInt64(ViewBag.cid), Convert.ToInt64(ViewBag.gameid), Convert.ToInt16(ViewBag.prizenumber), Convert.ToInt64(ViewBag.outletid), out emailstring, out cusemail);
                //Globalsettings.SendEmail(cusemail, "Redeem Prize", "", emailstring);
            }
            return View();
        }
	}
}