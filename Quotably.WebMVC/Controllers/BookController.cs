using Microsoft.AspNet.Identity;
using Quotably.Models;
using Quotably.Services;
using Quotably.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quotably.WebMVC.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var service = CreateBookService();
            var model = service.GetBooks();

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBookService();

            if(service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your book was created.";
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);

            ModelState.AddModelError("", "Book could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBookById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBookService();
            var detail = service.GetBookById(id);
            var model = new BookEdit
            {
                BookID = detail.BookID,
                Title = detail.Title,
                Description = detail.Description,
                AuthorID = detail.AuthorID,
            };
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.BookID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBookService();
            
            if(service.UpdateBook(model))
            {
                TempData["SaveResult"] = "Your book was updated.";
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);

            ModelState.AddModelError("", "Your book could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBookById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            var service = CreateBookService();

            service.DeleteBook(id);

            TempData["SaveResult"] = "Your book was deleted.";

            return RedirectToAction("Index");
        }

        private BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookService(userId);
            return service;
        }
    }
}