using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class DailyHotController : Controller
    {
        // GET: DailyHot
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public DailyHotController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        public ActionResult Index()
        {
            var daily = uow.dailyHotRepo.GetAll().Where(a => a.IsDeleted != true && a.IsActive == true).FirstOrDefault();

            var data = uow.additionalServiceRepo.GetAll().Where(a => a.IsDeleted != true && a.IsDailyHot == true).AsQueryable();
            DailyHotDataViewModel dailyviewmodel = new DailyHotDataViewModel();
            dailyviewmodel.title = daily.Title;
            dailyviewmodel.photourl = daily.PhotoUrl;
            dailyviewmodel.additionalservicelist = data;
            return View(dailyviewmodel);
        }

      

    }
}