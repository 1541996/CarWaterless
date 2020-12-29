﻿using Infra.Models;
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
    public class Admin_OperationController : Controller
    {
        // GET: Admin_Operation
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public Admin_OperationController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _list(int pagesize = 10, int page = 1, string searchvalue = null, string OrderBy = "Accesstime",
         string Direction = "ASC")
        {
            Expression<Func<tbCustomer, bool>> searchfilter = null;

            if (searchvalue != "" && searchvalue != null)
            {
                searchfilter = PredicateBuilder.New<tbCustomer>();
                searchfilter = searchfilter.Or(l => l.FullName.StartsWith(searchvalue));
                searchfilter = searchfilter.Or(l => l.UserName.StartsWith(searchvalue));
                searchfilter = searchfilter.Or(l => l.PhoneNo.StartsWith(searchvalue));
            }
            else
            {
                searchfilter = l => l.IsDeleted != true;
            }



            IQueryable<OperationCustomerViewModel> result = (from customer in uow.customerRepo.GetAll()
                                                             join operation in uow.operationRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on customer.Id equals operation.CustomerId                                                    
                                                             join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on operation.CustomerVehicleId equals car.Id
                                                             join carcategory in uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on car.CarCategoryId equals carcategory.Id
                                                             join branch in uow.branchRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on operation.BranchId equals branch.Id
                                                             join township in uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on branch.TownshipId equals township.Id
                                                             select new OperationCustomerViewModel
                                                             {
                                                                 operation = operation,
                                                                 customer = customer,
                                                                 carcategory = carcategory,
                                                                 branch = branch,
                                                                 township = township
                                                             }).AsQueryable();
            var totalCount = result.Count();


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;
            if (OrderBy == "Accesstime")
            {
                if (Direction == "ASC")
                {
                    result = result.OrderBy(a => a.operation.CreateDate);

                }
                else
                {
                    result = result.OrderByDescending(a => a.operation.CreateDate);

                }
            }
            else
            {
                if (Direction == "ASC")
                {
                    result = result.OrderBy(a => a.customer.FullName);

                }
                else
                {
                    result = result.OrderByDescending(a => a.customer.FullName);

                }
            }
            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<OperationCustomerViewModel>(objs, page, pagesize, totalCount);
            return PartialView("_list", model);
        }


    }
}