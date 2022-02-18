using CoreProject_DataAccess.Data;
using CoreProject_Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbModel;
        public CategoryController(ApplicationDbContext dbModel)
        {
            _dbModel = dbModel;
        }
        public ActionResult Index()
        {
            List<Category> categories = _dbModel.Categories.ToList();
            return View(categories);
        }

        public ActionResult Upsert(int? id)
        {
            var category = new Category();
            if (id != null)
            {
                category = _dbModel.Categories.FirstOrDefault(x => x.Category_Id.Equals(id));
                if (category == null)
                {
                    return NotFound();
                }
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Category_Id == 0)
                {
                    //create
                    _dbModel.Add(category);
                }
                else
                {
                    //update
                    _dbModel.Update(category);
                }
                _dbModel.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = _dbModel.Categories.FirstOrDefault(x => x.Category_Id == id);
            _dbModel.Categories.Remove(category);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateMultiple2()
        {
            var category = new List<Category>();
            for (int i = 0; i < 2; i++)
            {
                category.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _dbModel.AddRange(category);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateMultiple5()
        {
            var category = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                category.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _dbModel.AddRange(category);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveMultiple2()
        {
            var category = _dbModel.Categories.OrderByDescending(x => x.Category_Id).Take(2).ToList();
            _dbModel.RemoveRange(category);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult RemoveMultiple5()
        {
            var category = _dbModel.Categories.OrderByDescending(x => x.Category_Id).Take(5).ToList();
            _dbModel.RemoveRange(category);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
