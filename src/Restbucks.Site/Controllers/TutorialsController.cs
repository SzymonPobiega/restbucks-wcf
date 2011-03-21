using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restbucks.Site.Controllers
{
    public class TutorialsController : RestbucksController
    {
        public ActionResult Index()
        {
            if (IsPolish())
            {
                return View("Index-pl");
            }
            return View();
        }

        public ActionResult Building()
        {
            if (IsPolish())
            {
                return View("Building-pl");
            }
            return View();
        }
    }
}
