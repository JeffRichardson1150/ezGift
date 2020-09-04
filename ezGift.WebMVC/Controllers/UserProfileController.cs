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
            var email = User.Identity.GetUserName();

            // Each Registered Account may only have 1 User Profile
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            // Check whether this Account already has a User Profile
            var model = service.GetUserProfileByOwner(userId);
            // If there already is a UserProfile for this Account
            if (model != null)
            {
                // Format the message
                TempData["SaveResult"] = "A User Profile for this Account already exists.";

                // Redirect to the ActionResult ProfileExists and pass it a UserProfileDetail model
                // containing the contents of the existing UserProfile for this Account
                return RedirectToAction("ProfileExists", new 
                {
                    UserProfileId = model.UserProfileId,
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    OwnerId = model.OwnerId
                });

            }
            // If there isn't an existing UserProfile for this Registered Account
            // return a blank View so we can Create a new UserProfile
            else
            {
                return View(
                    new UserProfileCreate
                    {
                        //UserProfileId = "",
                        FirstName = "",
                        LastName = "",
                        //Name = "",
                        Address = "",
                        Email = email,
                        //xxxxxxxx - xxxx - xxxx - xxxx - xxxxxxxxxxxx
                        OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000000")
                    });
            }
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

        // Called from Create (Get)
        // When someone tries to create a User Profile, if a User Profile already exists for that Registered Account
        // Display the existing UserProfile in Detail View.  From there, they can Edit it, or return to Index
        public ActionResult ProfileExists(UserProfileDetail model)
        {
            TempData["SaveResult"] = "A User Profile for this Account already exists.";

            return View(model);
        }

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