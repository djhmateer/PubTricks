﻿using System.Web.Mvc;
using PubTricks.Web.Infrastructure;
using PubTricks.Web.Models;
using System.Dynamic;
using System;

namespace PubTricks.Web.Controllers {
    public class HomeController : Controller {
        ILogger _logger;
        dynamic _tricksTable;

        public HomeController(ILogger logger) {
            _logger = logger;
            _tricksTable = new Tricks();
        }

        public ActionResult Index() {
            _logger.LogInfo("In home");
            dynamic viewModel = new ExpandoObject();
            
            viewModel.TenTricksNewestFirst = _tricksTable.GetTenTricksNewestFirst();
            viewModel.TenTricksMostPopularFirst = _tricksTable.GetTenTricksMostPopularFirst();
            return View(viewModel);
        }

        public ActionResult Thanks() {
            _logger.LogInfo("In thanks");
            return View();
        }
    }
}
