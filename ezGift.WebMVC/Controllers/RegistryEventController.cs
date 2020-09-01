using ezGift.Models;
using ezGift.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ezGift.WebMVC.Controllers
{
    public class RegistryEventController : Controller
    {
        // GET: RegistryEvent
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RegistryEventService(userId);
            var model = service.GetRegistryEvents();
            return View(model);

        }

        //GET
        public ActionResult Create()
        {
            var service = CreateRegistryEventService();

            if (service.CheckForUserProfile())
            {
                return View();

            }
            TempData["SaveResult"] = "Your Event could not be created. Please create a User Profile.";
            return RedirectToAction("Index");

        }

        // POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistryEventCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateRegistryEventService();

            if (service.CreateRegistryEvent(model))
            {
                // Use TempData rather than ViewBag. TempData removes information after it's accessed
                TempData["SaveResult"] = "Your Event was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Event could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateRegistryEventService();
            var model = svc.GetRegistryEventById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRegistryEventService();
            var detail = service.GetRegistryEventById(id);
            var model =
                new RegistryEventEdit
                {
                    RegistryEventId = detail.RegistryEventId,
                    UserProfileId = detail.UserProfileId,
                    UserProfile = detail.UserProfile,
                    RegistryEventTitle = detail.RegistryEventTitle,
                    RegistryEventDescription = detail.RegistryEventDescription,
                    EventLocation = detail.EventLocation,
                    EventDate = detail.EventDate
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RegistryEventEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.RegistryEventId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateRegistryEventService();

            if (service.UpdateRegistryEvent(model))
            {
                TempData["SaveResult"] = "Your Event was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Event could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRegistryEventService();
            var model = svc.GetRegistryEventById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRegistryEventService();
            service.DeleteRegistryEvent(id);
            TempData["SaveResult"] = "Your Event was deleted.";
            return RedirectToAction("Index");

        }

        private RegistryEventService CreateRegistryEventService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RegistryEventService(userId);
            return service;
        }

    }
}