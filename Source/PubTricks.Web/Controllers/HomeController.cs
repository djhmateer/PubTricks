using System.Web.Mvc;
using PubTricks.Web.Infrastructure;

namespace PubTricks.Web.Controllers {
    public class HomeController : Controller {
        ILogger _logger;

        //Every time you see a request for an ILogger interface in a controller, return a new NLogger class
        public HomeController(ILogger logger) {
            _logger = logger;
        }

        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            _logger.LogInfo("In home");
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
