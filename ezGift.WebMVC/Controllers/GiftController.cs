using ezGift.Models;
using ezGift.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ezGift.WebMVC.Controllers
{
    public class GiftController : Controller
    {
        //public ActionResult Index(int? eventId)
        // The stupidest thing : the parameter must be called id.  Otherwise, the parameter will be assigned null
        public ActionResult Index(int id)
        {
            // refactor GiftService so, rather than setting a Guid _userId,
            // we set an int _eventId
            // so we can use the _eventId of the event which all the gifts are connected to
            int eventId = id;
            var service = new GiftService(eventId);

            // get the list of gifts 
            //   where Gift.RegistryEvent.RegistryEventId == eventId
            var model = service.GetGiftsByEvent();
            return View(model);

        }

        //GET
        //public ActionResult Create(int? id)
        public ActionResult Create(int id)
        {
            //var service = CreateGiftService();

            //if (id != null)
            //{
                int eventId = (int)id;
                return View(new GiftCreate
            {
                    GiftName = "",
                    GiftDescription = "",
                    RegistryEventId = eventId,
                    //RegistryEvent = null
            });
            //}
            //else
            //{
            //    return View();
            //}

        }

        // POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GiftCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateGiftService();

            if (service.CreateGift(model))
            {
                // Use TempData rather than ViewBag. TempData removes information after it's accessed
                TempData["SaveResult"] = "Your Event was created.";
                return RedirectToAction("Index", new { id = model.RegistryEventId } );
            }

            ModelState.AddModelError("", "Your Event could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateGiftService();
            var model = svc.GetGiftById(id);

            return View(model);
        }

        // GET for Edit
        public ActionResult Edit(int id)
        {
            var service = CreateGiftService();
            var detail = service.GetGiftById(id);
            var model =
                new GiftEdit
                {
                    GiftId = detail.GiftId,
                    GiftName = detail.GiftName,
                    GiftDescription = detail.GiftDescription,
                    RegistryEventId = detail.RegistryEventId,
                    RegistryEvent = detail.RegistryEvent
                };
            return View(model);
        }

        // POST for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GiftEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.GiftId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateGiftService();

            if (service.UpdateGift(model))
            {
                TempData["SaveResult"] = "Your Event was updated";
                return RedirectToAction("Index", new { id = model.RegistryEventId });
            }

            ModelState.AddModelError("", "Your Event could not be updated.");
            return View(model);
        }

        // GET for Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGiftService();
            var model = svc.GetGiftById(id);

            return View(model);
        }

        // POST for Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGiftService();

            var model = service.GetGiftById(id);
            int eventId = model.RegistryEventId;

            service.DeleteGift(id);
            TempData["SaveResult"] = "Your Event was deleted.";
            return RedirectToAction("Index", new { id = eventId });

        }

        private GiftService CreateGiftService()
        {

            var service = new GiftService();
            return service;
        }


    }
}