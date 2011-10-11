using System.Web.Mvc;
using PubTricks.Web.Infrastructure;
using PubTricks.Web.Models;
using System.Dynamic;

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
            //var data = _tricksTable.All(orderBy: "DateCreated");
            dynamic viewModel = new ExpandoObject();
            var data = _tricksTable.Query("SELECT * FROM Tricks ORDER BY DateCreated DESC");
            viewModel.AllTricksNewestFirst = data;

            var data2 = _tricksTable.Query("SELECT * FROM Tricks ORDER BY Votes DESC");
            viewModel.AllTricksMostPopularFirst = data2;

            return View(viewModel);
        }

        public ActionResult Thanks() {
            _logger.LogInfo("In thanks");
            return View();
        }
    }
}
