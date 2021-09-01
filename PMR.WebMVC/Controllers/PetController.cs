using Microsoft.AspNet.Identity;
using PMR.Models;
using PMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMR.WebMVC.Controllers
{
    // [Authorize]
    public class PetController : Controller
    {
        // GET: Pet
        public ActionResult Index()
        {

            var service = CreatePetService();
            var model = service.GetPets();
            return View(model);
        }

        // Get
        public ActionResult Create()
        {
            PetCreate model = new PetCreate();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PetCreate model)
        {

            if (!ModelState.IsValid) return View(model);
            //model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            var service = CreatePetService();

            if (service.CreatePet(model))
            {
                TempData["SaveResult"] = "Your pet was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Pet could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePetService();
            var detail = service.GetById(id);
            var model = new PetEdit
            {
                PetId = detail.PetId,
                Name = detail.Name,
                Age = detail.Age,
                Type = detail.Type,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PetId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePetService();

            if (service.UpdatePet(model))
            {
                TempData["SaveResult"] = "Your pet was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Pet could not be updated.");

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePet(int id)
        {
            var service = CreatePetService();
            service.DeletePet(id);
            TempData["SaveResult"] = "Your pet was deleted";
            return RedirectToAction("Index");
        }



        // UTIL
        private PetService CreatePetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            return service;
        }

        
    }
}