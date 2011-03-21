using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restbucks.Site.Controllers
{
    public class HomeController : RestbucksController
    {
        public ActionResult Index()
        {
            if (IsPolish())
            {
                return View("Index-pl");
            }
            return View();
        }        
        
        public ActionResult Presentation()
        {
            if (IsPolish())
            {
                return View("Presentation-pl");
            }
            return View();
        }
    }
}
