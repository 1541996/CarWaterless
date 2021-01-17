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
    // GET: Package
    
    public class ServiceController : Controller
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public ServiceController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        // GET: Package
        public ActionResult Index()
        {
            var data = uow.additionalServiceRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
        
            return View(data);
        }

        public ActionResult Detail()
        {
            return View();
        }


    }

}