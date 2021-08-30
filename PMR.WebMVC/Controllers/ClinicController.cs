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
    public class ClinicController : Controller
    {
        // GET: Clinic
        public ActionResult Index()
        {
            var service = CreateClinicService();
            var model = service.GetClinics();
            return View(model);
        }

        // Get
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClinicCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateClinicService();

            if (service.CreateClinic(model))
            {
                TempData["SaveResult"] = "Your clinic was created.";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Clinic could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateClinicService();
            var model = svc.GetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateClinicService();
            var detail = service.GetById(id);
            var model = new ClinicEdit
            {
                ClinicId = detail.ClinicId,
                Name = detail.Name,
                Address = detail.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClinicEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ClinicId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateClinicService();

            if (service.UpdateClinic(model))
            {
                TempData["SaveResult"] = "Your clinic was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Clinic could not be updated.");

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateClinicService();
            var model = svc.GetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClinic(int id)
        {
            var service = CreateClinicService();
            service.DeleteClinic(id);
            TempData["SaveResult"] = "Your Clinic was deleted";
            return RedirectToAction("Index");
        }

        private ClinicService CreateClinicService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ClinicService(userId);
            return service;
        }
    }
}