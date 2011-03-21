using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restbucks.Site.Controllers
{
    public class APIController : RestbucksController
    {
        public ActionResult Index()
        {
            if (IsPolish())
            {
                return View("Index-pl");
            }
            return View();
        }

        public ActionResult LinkRelations()
        {
            if (IsPolish())
            {
                return View("LinkRelations-pl");
            }
            return View();
        }

        public ActionResult Resources()
        {
            if (IsPolish())
            {
                return View("Resources-pl");
            }
            return View();
        }
    }
}
