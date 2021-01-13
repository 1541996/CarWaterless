using Data.Helper;
using Infra.Models;
using Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public FeedBackController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }
        public ActionResult Index(int customerid = 0)
        {
            ViewBag.customerid = customerid;
            return View();
        }

        [HttpPost]
        public ActionResult sendMessage(tbFeedBack feedback)
        {
            tbFeedBack UpdateEntity = null;
            feedback.IsDeleted = false;         
            feedback.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow);
            UpdateEntity = uow.feedbackRepo.InsertReturn(feedback);
            if (UpdateEntity != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }
    }
}