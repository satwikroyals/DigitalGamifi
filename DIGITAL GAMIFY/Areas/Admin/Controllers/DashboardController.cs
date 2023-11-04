using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            Globalsettings _global = new Globalsettings();
            if (!_global.IsAdminLoggedin())
            {
                return RedirectToAction(actionName: "Login", controllerName: "Admin");
            }
            else
            {
                return View();
            }
        }

    }
}