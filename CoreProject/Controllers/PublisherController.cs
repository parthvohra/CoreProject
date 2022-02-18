using CoreProject_DataAccess.Data;
using CoreProject_Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _dbModel;
        public PublisherController(ApplicationDbContext dbModel)
        {
            _dbModel = dbModel;
        }
        public ActionResult Index()
        {
            var publishers = _dbModel.Publishers.ToList();
            return View(publishers);
        }

        public ActionResult Upsert(int? id)
        {
            var Publisher = new Publisher();
            if (id != null)
            {
                Publisher = _dbModel.Publishers.FirstOrDefault(x => x.Publisher_Id.Equals(id));
                if (Publisher == null)
                {
                    return NotFound();
                }
            }
            return View(Publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(Publisher Publisher)
        {
            if (ModelState.IsValid)
            {
                if (Publisher.Publisher_Id == 0)
                {
                    //create
                    _dbModel.Add(Publisher);
                }
                else
                {
                    //update
                    _dbModel.Update(Publisher);
                }
                _dbModel.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Publisher);
        }

        public ActionResult Delete(int id)
        {
            var Publisher = _dbModel.Publishers.FirstOrDefault(x => x.Publisher_Id == id);
            _dbModel.Publishers.Remove(Publisher);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
