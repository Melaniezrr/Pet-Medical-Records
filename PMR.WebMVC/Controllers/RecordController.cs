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
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateRecordService();
            service.CreateRecord(model);

            return RedirectToAction("Index");
        }

        private RecordService CreateRecordService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RecordService(userId);
            return service;
        }
    }
}