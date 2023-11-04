using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;
using DIGITAL_GAMIFY.Entities;


namespace TravelFront.Controllers
{
    public class BusinessController : Controller
    {

        Globalsettings _global = new Globalsettings();
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (!_global.IsBusinessLoggedin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Business" });
            }
        }


        [HttpPost]
        public ActionResult Login(BusinessLoginEntities form)
        {
            if (ModelState.IsValid)
            {
                BusinessManager bal = new BusinessManager();
                BusinessEntity ent = bal.GetBusinessLogin(form);

                if (ent != null && ent.BusinessId > 0)
                {
                    DateTime expiration = DateTime.Now.AddDays(30);
                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, form.UserName,
                        DateTime.Now, expiration, true,
                        FormsAuthentication.FormsCookiePath);
                    HttpCookie ck = new HttpCookie(Globalsettings.BusinessCookiename, FormsAuthentication.Encrypt(tkt));
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    ck.Expires = expiration;
                    ck[Globalsettings.CookieBusiAdminId] = ent.AdminId.ToString();
                    ck[Globalsettings.CookieBusinessId] = ent.BusinessId.ToString();
                    ck[Globalsettings.CookieBusinessun] = ent.FullName;
                    ck[Globalsettings.CookieBusinessun] = ent.FullName;
                    ck[Globalsettings.CookieBusinessLogo] = ent.LogoPath;
                    Response.Cookies.Add(ck);
                    FormsAuthentication.RedirectFromLoginPage(ck[Globalsettings.CookieBusinessun].ToString(), true);

                    return RedirectToAction("Index", "Dashboard", new { area = "Business" });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and Password.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}