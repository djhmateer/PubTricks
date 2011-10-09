using System.Web.Mvc;
using PubTricks.Web.Infrastructure;
using PubTricks.Web.Models;

namespace PubTricks.Web.Controllers {
    public class HomeController : Controller {
        ILogger _logger;
        dynamic _tricksTable;

        //Every time you see a request for an ILogger interface in a controller, return a new NLogger class
        public HomeController(ILogger logger) {
            _logger = logger;
            _tricksTable = new Tricks();
        }

        public ActionResult Index() {
            _logger.LogInfo("In home");
            return View(_tricksTable.All());
        }

    }
}
