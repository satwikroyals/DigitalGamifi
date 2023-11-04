using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Areas.Business.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult CheckLogin()
        {
            Globalsettings _global = new Globalsettings();
            if (!_global.IsBusinessLoggedin())
            {
                return RedirectToAction("login", "business", new { area = "" });
            }
            else { return null; }
        }


        public ActionResult LogOut()
        {
            Globalsettings _global = new Globalsettings();
            _global.DoAdminLogOut();
            return RedirectToAction("login", "business", new { area = "" });
        }
    }
}