using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public ActionResult Index(string customerid = null)
        {
            ViewBag.customerid = customerid;
            return View();
        }

        public ActionResult Detail(int id = 0,string customerid = null)
        {
            ViewBag.customerid = customerid;
            BranchViewModel obj = new BranchViewModel();
            obj.branch = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            obj.township = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.branch.TownshipId).FirstOrDefault();
            return View(obj);
        }

        public ActionResult GetBranchList(string searchvalue = null,string customerid = null)
        {
            Expression<Func<tbBranch, bool>> searchfilter = null;
            Expression<Func<tbTownship, bool>> townshipfilter = null;

            if (searchvalue != "" && searchvalue != null)
            {
                searchfilter = PredicateBuilder.New<tbBranch>();
                searchfilter = searchfilter.Or(l => l.LocationName.StartsWith(searchvalue));

                //townshipfilter = PredicateBuilder.New<tbTownship>();
                //townshipfilter = townshipfilter.Or(l => l.Name.StartsWith(searchvalue));

            }
            else
            {
                searchfilter = l => l.IsDeleted != true;
              //  townshipfilter = l => l.IsDeleted != true;
            }


            ViewBag.customerid = customerid;

            //   var data = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            var data = (from branch in uow.branchRepo.GetAll().Where(a => a.IsDeleted != true).Where(searchfilter)
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