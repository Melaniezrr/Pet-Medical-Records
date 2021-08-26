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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PetCreate model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreatePetService();
            service.CreatePet(model);

            return RedirectToAction("Index");
        }

        private PetService CreatePetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            return service;
        }
    }
}