using Infra.Models;
using Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public NotificationController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        public ActionResult Index(int id = 0)
        {
            ViewBag.id = id;
            var data = uow.notificationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            return View(data);
        }


    }
}