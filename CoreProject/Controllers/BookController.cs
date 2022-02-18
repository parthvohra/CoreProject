using CoreProject_DataAccess.Data;
using CoreProject_Models.Models;
using CoreProject_Models.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbModel;
        public BookController(ApplicationDbContext dbModel)
        {
            _dbModel = dbModel;
        }
        public ActionResult Index()
        {
            var books = _dbModel.Books.Include(x => x.Publisher).ToList();
            //foreach (var item in books)
            //{
            //    //item.Publisher = _dbModel.Publishers.FirstOrDefault(x=>x.Publisher_Id == item.Publisher_Id);
            //    _dbModel.Entry(item).Reference(x=>x.Publisher).Load();
            //}
            return View(books);
        }

        public ActionResult Upsert(int? id)
        {
            var books = new BookVM
            {
                PublishersList = _dbModel.Publishers.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Publisher_Id.ToString()
                }).ToList()
            };
            if (id != null)
            {
                books.Book = _dbModel.Books.FirstOrDefault(x => x.Book_Id.Equals(id));
                if (books == null)
                {
                    return NotFound();
                }
            }
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(BookVM bookVM)
        {
            if (bookVM.Book.Book_Id == 0)
            {
                //create
                _dbModel.Add(bookVM.Book);
            }
            else
            {
                //update
                _dbModel.Update(bookVM.Book);
            }
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var books = new BookVM();
            if (id != null)
            {
                books.Book = _dbModel.Books.Include(a=>a.BookDetail).FirstOrDefault(x => x.Book_Id.Equals(id));
                //books.Book.BookDetail = _dbModel.BookDetails.FirstOrDefault(x => x.BookDetail_Id == id);
                if (books == null)
                {
                    return NotFound();
                }
            }
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(BookVM bookVM)
        {
            var books = new BookVM();
            if (bookVM.Book.BookDetail_Id == null)
            {
                _dbModel.BookDetails.Add(bookVM.Book.BookDetail);
                _dbModel.SaveChanges();
                var book = _dbModel.Books.FirstOrDefault(x => x.Book_Id == bookVM.Book.Book_Id);
                book.BookDetail_Id = bookVM.Book.BookDetail.BookDetail_Id;
            }
            else
            {
                _dbModel.BookDetails.Update(bookVM.Book.BookDetail);
            }
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var book = _dbModel.Books.FirstOrDefault(x => x.Book_Id == id);
            _dbModel.Books.Remove(book);
            _dbModel.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult ManageAuthors(int id)
        {
            var bookAuthor = new BookAuthorVM()
            {
                BookAuthorsList = _dbModel.BookAuthors.Include(x => x.Author).Include(x => x.Book).Where(x => x.Book_Id == id).ToList(),
                Book = _dbModel.Books.FirstOrDefault(x => x.Book_Id == id)
            };
            var assignedAuthors = bookAuthor.BookAuthorsList.Select(x => x.Author_Id).ToList();
            var authorList = _dbModel.Authors.Where(x => !assignedAuthors.Contains(x.Author_Id)).ToList();
            bookAuthor.AuthorsList = authorList.Select(x => new SelectListItem 
            {
                Text = x.FullName,
                Value = x.Author_Id.ToString()
            });
            return View(bookAuthor);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        public IActionResult PlayGround()
        {
            
                var book1 = _dbModel.Books.Include(x => x.BookDetail).FirstOrDefault(a => a.Book_Id == 2);
                book1.BookDetail.NumberOfPages = 2222;
                _dbModel.Books.Update(book1);
                _dbModel.SaveChanges();


                var book2 = _dbModel.Books.Include(x => x.BookDetail).FirstOrDefault(a => a.Book_Id == 2);
                book2.BookDetail.Weight = 3333;
                _dbModel.Books.Attach(book2);
                _dbModel.SaveChanges();


            var category = _dbModel.Categories.FirstOrDefault();
            _dbModel.Entry(category).State = EntityState.Modified;
            _dbModel.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
