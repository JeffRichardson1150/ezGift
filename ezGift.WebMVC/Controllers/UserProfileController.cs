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
            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var service = new UserProfileService(userId);
            //var model = service.GetUserProfileByOwner(userId);
            //if (model.OwnerId == userId)
            //{
            //    //return RedirectToAction("Details");
            //    TempData["SaveResult"] = "A User Profile for this Account already exists.";

            //    return View(model);
            //    //Details(model);
            //}
            //else
            //{
                return View();
            //}
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
                TempData["SaveResult"] = "Your User Profile was created.";
                return RedirectToAction("Index");

            };
            ModelState.AddModelError("", "User Profile could not be created.");
            return View(model);
        }

        // I created this for the UserProfileCreate.
        // The idea was - When select Create user profile, I first check to see whether a UserProfile exists
        // If it does, I show that UserProfile using the UserProfileDetail model
        // That approach didn't work well because the Create expects the UserProfileCreate model
        // 
        //public ActionResult Details(UserProfileDetail model)
        //{
        //    TempData["SaveResult"] = "A User Profile for this Account already exists.";

        //    return View(model);
        //}

        public ActionResult Details(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserProfileById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateUserProfileService();
            var detail = service.GetUserProfileById(id);
            var model =
                new UserProfileEdit
                {
                    UserProfileId = detail.UserProfileId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Address = detail.Address,
                    Email = detail.Email
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfileEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.UserProfileId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateUserProfileService();

            if (service.UpdateUserProfile(model))
            {
                TempData["SaveResult"] = "Your User Profile was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your User Profile could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserProfileById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateUserProfileService();
            service.DeleteUserProfile(id);
            TempData["SaveResult"] = "Your User Profile was deleted.";
            return RedirectToAction("Index");

        }

        private UserProfileService CreateUserProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            return service;
        }
    }
}