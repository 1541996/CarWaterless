using Infra.Models;
using Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public PackageController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        public ActionResult Index()
        {
            var data = uow.memberPackageRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            return View(data);
        }
        
        public ActionResult Detail()
        {
            return View();
        }


    }

}