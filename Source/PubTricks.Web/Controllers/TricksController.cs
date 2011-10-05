using System.Web.Mvc;
using PubTricks.Web.Models;

namespace PubTricks.Web.Controllers
{
    public class TricksController : Controller
    {
        Tricks _tricksTable;
        public TricksController() {
            _tricksTable = new Tricks();
        }

        public ActionResult Index()
        {
            return View(_tricksTable.All());
        }

        public ActionResult Details(int id)
        {
            //return View(_tricksTable.FindBy(IDependencyResolver: id, schema: true));
            return View();
        }

        public ActionResult Create()
        {
            return View(_tricksTable.Prototype);
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            dynamic item = _tricksTable.CreateFrom(collection);
            try
            {
                _tricksTable.Insert(item);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["alert"] = "There was an error adding the trick";
                return View();
            }
        }
        
        //
        // GET: /Tricks/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tricks/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tricks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tricks/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
