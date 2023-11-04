using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;
using DIGITAL_GAMIFY.Entities;

namespace DIGITAL_GAMIFY.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        Globalsettings _global = new Globalsettings();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {

            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) || Request.QueryString["ReturnUrl"].ToLower().IndexOf("admin") != -1)
            {
                if (!_global.IsAdminLoggedin())
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
            }
            else if (Request.QueryString["ReturnUrl"].ToLower().IndexOf("business") != -1)
            {
                return RedirectToAction("login", "business");
            }

            else { return View(); }
        }


        [HttpPost]
        public ActionResult Login(AdminLoginEntities form)
        {
            if (ModelState.IsValid)
            {
                AdminManager bal = new AdminManager();
                AdminEntities ent = bal.GetAdminLogin(form);

                if (ent != null && ent.AdminId > 0)
                {
                    DateTime expiration = DateTime.Now.AddDays(30);
                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, form.UserName,
                        DateTime.Now, expiration, true,
                        FormsAuthentication.FormsCookiePath);
                    HttpCookie ck = new HttpCookie(Globalsettings.AdminCookiename, FormsAuthentication.Encrypt(tkt));
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    ck.Expires = expiration;
                    ck[Globalsettings.CookieAdminId] = ent.AdminId.ToString();
                    ck[Globalsettings.CookieAdminun] = form.UserName;
                    Response.Cookies.Add(ck);
                    FormsAuthentication.RedirectFromLoginPage(ck[Globalsettings.CookieAdminun].ToString(), true);

                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
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