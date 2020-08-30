using ezGift.Models;
using ezGift.Services;
using ezGift.WebMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ezGift.WebMVC.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            var model = service.GetUserProfiles();
            return View(model);
        }

        //Get for Create
        public ActionResult Create()
        {
            return View();
        }

        // POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfileCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateUserProfileService();
            if (service.CreateUserProfile(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");

            };
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserProfileById(id);

            return View(model);
        }

        private UserProfileService CreateUserProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            return service;
        }
    }
}