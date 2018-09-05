using Microsoft.AspNet.Identity;
using Quotably.Data;
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
    public class AuthorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Author
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AuthorService(userId);
            var model = service.GetAuthors();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateAuthorService();

            if (service.CreateAuthor(model))
            {
                TempData["SaveResult"] = "Your author was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Author could not be saved.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAuthorService();
            var model = svc.GetAuthorById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAuthorService();
            var detail = service.GetAuthorById(id);
            var model =
                new AuthorEdit
                {
                    AuthorID = detail.AuthorID,
                    AuthorFirstName = detail.AuthorFirstName,
                    AuthorLastName = detail.AuthorLastName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.AuthorID != id)
            {
                ModelState.AddModelError("", "Id Mismatch!");
                return View(model);
            }

            var service = CreateAuthorService();

            if(service.UpdateAuthor(model))
            {
                TempData["SaveResult"] = "Your author was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your author could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAuthorService();
            var model = svc.GetAuthorById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(int id)
        {
            var service = CreateAuthorService();

            service.DeleteAuthor(id);

            TempData["SaveResult"] = "Your author was deleted.";

            return RedirectToAction("Index");
        }

        private AuthorService CreateAuthorService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AuthorService(userId);
            return service;
        }
    }
}