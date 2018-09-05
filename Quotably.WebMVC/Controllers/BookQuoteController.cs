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
    public class BookQuoteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookQuote
        public ActionResult Index()
        {
            var service = CreateBookQuoteService();
            var model = service.GetBookQuotes();

            return View(model);
        }

        // GET VIEW (CREATE)
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookQuoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBookQuoteService();

            if(service.CreateBookQuote(model))
            {
                TempData["SaveResult"] = "Your book quote was created.";
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", model.BookID);
            ModelState.AddModelError("", "Book Quote could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBookQuoteService();
            var model = svc.GetBookQuoteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBookQuoteService();
            var detail = service.GetBookQuoteById(id);
            var model = new BookQuoteEdit
            {
                BookQuoteID = detail.BookQuoteID,
                Content = detail.Content,
                BookID = detail.BookID,
                AuthorID = detail.AuthorID,
            };
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", model.BookID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookQuoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.BookQuoteID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBookQuoteService();

            if(service.UpdateBookQuote(model))
            {
                TempData["SaveResult"] = "Your book quote was updated.";
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorLastName", model.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", model.BookID);
            ModelState.AddModelError("", "Your book quote could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBookQuoteService();
            var model = svc.GetBookQuoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookQuote(int id)
        {
            var service = CreateBookQuoteService();

            service.DeleteBookQuote(id);

            TempData["SaveResult"] = "Your book quote was deleted.";

            return RedirectToAction("Index");
        }

        private BookQuoteService CreateBookQuoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookQuoteService(userId);
            return service;
        }
    }
}