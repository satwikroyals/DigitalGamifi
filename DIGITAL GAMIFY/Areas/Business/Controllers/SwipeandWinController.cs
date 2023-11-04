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
    [Authorize]
    public class SwipeandWinController : Controller
    {


        [ActionName("View")]
        public ActionResult ViewSwipeandWin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            SwipeandWinEntity g = new SwipeandWinEntity();           
            Int32 adminid = 0;
            HttpCookie buscookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (buscookie != null)
            {
                adminid = Convert.ToInt32(buscookie[Globalsettings.CookieBusiAdminId]);
            }         
            if (!string.IsNullOrEmpty(Request.QueryString["_g"]))
            {
                SwipeandWinManager bal = new SwipeandWinManager();
                g = bal.GetSwipeandWinById(Convert.ToInt32(Request.QueryString["_g"]));
            }          

            //foreach (SelectListItem item in (SelectList)g.BusinessList)
            //{

            //        item.Selected = true;

            //}

            return View(g);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ValidateInput(false)]
        public ActionResult Add(HttpPostedFileBase ImageFile, HttpPostedFileBase FirstPrizeImageFile, HttpPostedFileBase SecondPrizeImageFile, HttpPostedFileBase ThirdPrizeImageFile, SwipeandWinEntity p, string[] Attributes, string[] attributes2, string[] attributes3)
        {
            Int32 adminid = 0;
            Int32 businessid = 0;
            HttpCookie busicookie = Request.Cookies[Globalsettings.BusinessCookiename];
            if (busicookie != null)
            {
                adminid = Convert.ToInt32(busicookie[Globalsettings.CookieBusiAdminId]);
                businessid = Convert.ToInt32(busicookie[Globalsettings.CookieBusinessId]);
            }
            if(string.IsNullOrEmpty(p.FirstPrizeCondition))
            {
                p.FirstPrizeCondition="";
            }
            if (string.IsNullOrEmpty(p.SecondPrizeCondition))
            {
                p.SecondPrizeCondition = "";
            }
            if (string.IsNullOrEmpty(p.ThirdPrizeCondition))
            {
                p.ThirdPrizeCondition = "";
            }
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                p.Image = Path.GetFileName(ImageFile.FileName.Replace(" ",""));
            }

            if (FirstPrizeImageFile != null && FirstPrizeImageFile.ContentLength > 0)
            {
                p.FirstPrizeImage = "firstprize" + Path.GetExtension(FirstPrizeImageFile.FileName.Replace(" ",""));
            }

            if (SecondPrizeImageFile != null && SecondPrizeImageFile.ContentLength > 0)
            {
                p.SecondPrizeImage = "secondprize" + Path.GetExtension(SecondPrizeImageFile.FileName.Replace(" ", ""));
            }

            if (ThirdPrizeImageFile != null && ThirdPrizeImageFile.ContentLength > 0)
            {
                p.ThirdPrizeImage = "thirdprize" + Path.GetExtension(ThirdPrizeImageFile.FileName.Replace(" ", ""));
            }
            p.BusinessId = businessid;
          
            p.Interval = "";
            p.QRCode = "QR.jpg";
            SwipeandWinManager bal = new SwipeandWinManager();
            string AttriId = "";
            string AttriId2 = "";
            string AttriId3 = "";
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
            p.Attributes1 = AttriId;
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
            p.Attributes2 = AttriId2;
            if (attributes3 != null)
            {
                foreach (string attri in attributes3)
                {
                    attrivalue = Request.Form["attributes2_" + attri];
                    attricomma = attrivalue.Replace(",", ";");
                    if (attrivalue != "")
                    {
                        AttriId3 += attri + "[" + attricomma + "]" + "";
                    }
                }
            }
            p.Attributes3 = AttriId3;
            Int32 gameId = bal.AddUpdateSwipeandWin(p);

            DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] + gameId.ToString() + "/"));
            string folder = Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] + gameId.ToString() + "/");
            if (!dir.Exists)
            {
                dir.Create();
            }

            if (gameId > 0)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    ImageFile.SaveAs(folder + p.Image);
                }

                if (FirstPrizeImageFile != null && FirstPrizeImageFile.ContentLength > 0)
                {
                    FirstPrizeImageFile.SaveAs(folder + p.FirstPrizeImage);
                }

                if (SecondPrizeImageFile != null && SecondPrizeImageFile.ContentLength > 0)
                {
                    SecondPrizeImageFile.SaveAs(folder + p.SecondPrizeImage);
                }

                if (ThirdPrizeImageFile != null && ThirdPrizeImageFile.ContentLength > 0)
                {
                    ThirdPrizeImageFile.SaveAs(folder + p.ThirdPrizeImage);
                }
            }
            string QRCodeUrl = Globalsettings.GetSwipeandwinQrCodeUrl(gameId);

            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            encoder.QRCodeScale = 10;
            Bitmap img = encoder.Encode(QRCodeUrl);
            Graphics g = Graphics.FromImage(img);
            img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
            //if (gameId > 0)
            //{
            //    //DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["SwipeandWinImagesPath"] + gameId.ToString() + "/"));
               
            //}


            return RedirectToAction("View", "SwipeandWin");
        }


    }
}