using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;

namespace PubTricks.Web.Controllers
{
    public class TricksController : Controller
    {
        dynamic _tricksTable;
        public TricksController() {
            _tricksTable = new Tricks();
        }
       
        //
        // GET: /Tricks/Details/5
        // /Trick/the-pen-trick
        public ActionResult Details(int id)
        {
            var model = _tricksTable.Get(ID: id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, FormCollection collection) {
            var trickToAddVoteTo = _tricksTable.Get(ID: id);
            var currentVotes = trickToAddVoteTo.Votes;
            currentVotes += 1;
            trickToAddVoteTo.Votes = currentVotes;
            //var itemToUpdate = _tricksTable.CreateFrom(collection);
            try {
                _tricksTable.Update(trickToAddVoteTo, id);
                return RedirectToAction("Details", id);
            }
            catch (Exception ex) {
                TempData["Error"] = "There was a problem updating the like: " + ex.Message;
                return View();
            }
        }
    }
}
