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
    public class BranchController : Controller
    {
        // GET: Branch
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public BranchController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id = 0)
        {
            BranchViewModel obj = new BranchViewModel();
            obj.branch = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            obj.township = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.branch.TownshipId).FirstOrDefault();
            return View(obj);
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
                        }).OrderByDescending(a => a.branch.CreateDate).AsQueryable();

            return PartialView("_branchlist", data);
        }
    }
}