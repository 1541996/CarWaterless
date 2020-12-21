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
    /// <summary>
    /// test2
    /// </summary>
    public class HomeController : Controller
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public HomeController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult GetBranchList()
        {
         //   var data = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            var data = (from branch in uow.branchRepo.GetAll().Where(a => a.IsDeleted != true)
                        join township in uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true)
                        on branch.TownshipId equals township.Id
                        select new BranchViewModel()
                        {
                            branch = branch,
                            township = township
                        }).OrderByDescending(a => a.branch.CreateDate).Take(5).AsQueryable();

            return PartialView("_branchlist", data);
        }

    }
}
