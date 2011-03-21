using System.Threading;
using System.Web.Mvc;

namespace Restbucks.Site.Controllers
{
    public class RestbucksController : Controller
    {
        protected bool IsPolish()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "pl";
        }
    }
}