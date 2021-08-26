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
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateClinicService();
            service.CreateClinic(model);

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