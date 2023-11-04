using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIGITAL_GAMIFY.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

    }
}