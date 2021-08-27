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
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index()
        {
            var service = CreateRecordService();
            var model = service.GetRecords();
            return View(model);
        }

        // Get
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecordCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateRecordService();

            if (service.CreateRecord(model))
            {
                TempData["SaveResult"] = "Your record was created.";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Record could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRecordService();
            var model = svc.GetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRecordService();
            var detail = service.GetById(id);
            var model = new RecordEdit
            {
                RecordId = detail.RecordId,
                VaccineName = detail.VaccineName,
                Date = detail.Date,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RecordEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RecordId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRecordService();

            if (service.UpdateRecord(model))
            {
                TempData["SaveResult"] = "Your record was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Record could not be updated.");

            return View(model);
        }

        private RecordService CreateRecordService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RecordService(userId);
            return service;
        }
    }
}