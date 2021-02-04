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
    public class Admin_FeedbackController : Controller
    {
        // GET: Admin_Feedback
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public Admin_FeedbackController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _list(int pagesize = 10, int page = 1, string searchvalue = null, string OrderBy = "Accesstime",
           string Direction = "DESC")
        {
            Expression<Func<tbFeedBack, bool>> searchfilter = null;

            if (searchvalue != "" && searchvalue != null)
            {
                searchfilter = PredicateBuilder.New<tbFeedBack>();
                searchfilter = searchfilter.Or(l => l.CustomerName.StartsWith(searchvalue));
                searchfilter = searchfilter.Or(l => l.Phone.StartsWith(searchvalue));
             
            }
            else
            {
                searchfilter = l => l.IsDeleted != true;
            }



            IQueryable<tbFeedBack> result = uow.feedbackRepo.GetAll().Where(searchfilter).Where(a => a.IsDeleted != true).AsQueryable();
            var totalCount = result.Count();


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;
            if (OrderBy == "Accesstime")
            {
                if (Direction == "ASC")
                {
                    result = result.OrderBy(a => a.CreateDate);

                }
                else
                {
                    result = result.OrderByDescending(a => a.CreateDate);

                }
            }
           
            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<tbFeedBack>(objs, page, pagesize, totalCount);
            return PartialView("_list", model);
        }

    }
}