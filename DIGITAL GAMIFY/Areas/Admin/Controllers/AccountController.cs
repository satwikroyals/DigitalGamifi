using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult CheckLogin()
        {
            Globalsettings _global = new Globalsettings();
            if (!_global.IsAdminLoggedin())
            {
                return RedirectToAction("login", "admin", new { area = "" });
            }
            else { return null; }
        }

        // GET: Owner/Account
        public ActionResult LogOut()
        {
            Globalsettings _global = new Globalsettings();
            _global.DoAdminLogOut();
            return RedirectToAction("login", "admin", new { area = "" });
        }
    }
}