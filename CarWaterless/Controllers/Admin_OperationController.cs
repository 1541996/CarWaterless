using Data.Helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using LinqKit;
using Microsoft.AspNet.SignalR;
using MoreLinq;
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

        public ActionResult _list(int pagesize = 10, int page = 1, string searchvalue = null,
            string OrderBy = "Accesstime", string Direction = "DESC",
            DateTime? fromdate = null, DateTime? todate = null,string type = null
            ,string washtype = null,string cardata = null,string bookingsource = null)
        {
            Expression<Func<tbCustomer, bool>> searchfilter = null;
            Expression<Func<tbOperation, bool>>  datefilter, washtypefilter, statusfilter, bookingsourcefilter = null;
            Expression<Func<tbCustomerVehicle, bool>> carfilter = null;
            if (searchvalue != "" && searchvalue != null)
            {
                searchfilter = PredicateBuilder.New<tbCustomer>();
                searchfilter = searchfilter.Or(l => l.FullName.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.UserName.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.PhoneNo.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.Email.Contains(searchvalue));
            }
            else
            {
                searchfilter = l => l.IsDeleted != true;
            }

            if (cardata != "" && cardata != null)
            {
                carfilter = PredicateBuilder.New<tbCustomerVehicle>();
                carfilter = carfilter.Or(l => l.VehicleName.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleNo.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleColor.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleBrand.Contains(searchvalue));
            }
            else
            {
                carfilter = l => l.IsDeleted != true;
            }

            if (type == "Waiting")
            {
                if (fromdate != null && todate != null)
                {
                    fromdate = fromdate.Value.Date;
                    todate = todate.Value.AddDays(1).Date;
                    datefilter = l => l.OperationDate >= fromdate && l.OperationDate < todate;
                }
                else
                {
                    var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    var nextday = today.AddDays(1).Date;
                    datefilter = l => l.OperationDate >= today && l.OperationDate < nextday;
                }
            }
            else if(type == "Cancel")
            {
                if (fromdate != null && todate != null)
                {
                    fromdate = fromdate.Value.Date;
                    todate = todate.Value.AddDays(1).Date;
                    datefilter = l => l.CancelTime >= fromdate && l.CancelTime < todate;
                }
                else
                {
                    var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    var nextday = today.AddDays(1).Date;
                    datefilter = l => l.CancelTime >= today && l.CancelTime < nextday;
                }
            }
            else
            {
                if (fromdate != null && todate != null)
                {
                    fromdate = fromdate.Value.Date;
                    todate = todate.Value.AddDays(1).Date;
                    datefilter = l => l.ConfirmedTime >= fromdate && l.ConfirmedTime < todate;
                }
                else
                {
                    var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    var nextday = today.AddDays(1).Date;
                    datefilter = l => l.ConfirmedTime >= today && l.ConfirmedTime < nextday;
                }

            }

            if (bookingsource != null && bookingsource != "")
            {

                bookingsourcefilter = l => l.BookingSource == bookingsource;
            }
            else
            {

                bookingsourcefilter = l => l.IsDeleted != true;
            }





            if (type != null && type != "")
            {

                statusfilter = l => l.Status == type;
            }
            else
            {

                statusfilter = l => l.IsDeleted != true;
            }



            if (washtype != null && washtype != "")
            {
                
                washtypefilter = l => l.WashOption == washtype;
            }
            else
            {

                washtypefilter = l => l.IsDeleted != true;
            }
          




            IQueryable<BookingViewModel> result = (from operation in uow.operationRepo.GetAll().
                                                             Where(a => a.IsDeleted != true).Where(datefilter).Where(washtypefilter)
                                                             .Where(statusfilter).Where(bookingsourcefilter)
                                                             join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             .Where(searchfilter)
                                                             on operation.CustomerId equals customer.Id                                                    
                                                             join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             .Where(carfilter).Where(carfilter)
                                                             on operation.CustomerVehicleId equals car.Id
                                                             join carcategory in uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true)
                                                             on car.CarCategoryId equals carcategory.Id                                                    
                                                             select new BookingViewModel
                                                             {
                                                                   FullName = customer.FullName,
                                                                   VehicleBrand = car.VehicleBrand,
                                                                   VehicleName = car.VehicleName,
                                                                   VehicleColor = car.VehicleColor,
                                                                   CategoryName = carcategory.Name,
                                                                   VehicleNo = car.VehicleNo,
                                                                   PhoneNo = customer.PhoneNo,
                                                                   OperationId = operation.Id,
                                                                   BookingStatus = operation.Status,
                                                                   WashOption = operation.WashOption,
                                                                   CustomerAddress = operation.CustomerAddress,
                                                                   CategoryType = carcategory.Type,
                                                                   CategoryBasicPrice = carcategory.BasicPrice,
                                                                   AdditionalNames = operation.AdditionalNames,
                                                                   AdditionalPrices = operation.AdditionalPrices,
                                                                   CustomerId = operation.CustomerId ?? 0,
                                                                   OperationDate = operation.OperationDate,
                                                                   ConfirmedDate = operation.ConfirmedTime,
                                                                   TotalAmount = operation.TotalAmount,
                                                                   CancelDate = operation.CancelTime,
                                                                   FinishedDate = operation.FinishedTime,
                                                                   Email = customer.Email,
                                                                   BookingPackage = operation.BookingPackage,
                                                                   MemberPackage = operation.MemberPackageName,
                                                                   PaymentType = operation.PaymentType,
                                                                   ComplaintMessage = operation.ComplaintsMessage,
                                                                   Branch = operation.BranchName,
                                                                   Township = operation.TownshipName

                                                             }).AsQueryable();
            var totalCount = result.Count();

            if(type == "Waiting")
            {
                result = result.OrderByDescending(a => a.OperationDate);
            }
            else
            {
                result = result.OrderByDescending(a => a.ConfirmedDate);
            }


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;
            //if (OrderBy == "Accesstime")
            //{
            //    if (Direction == "ASC")
            //    {
            //        result = result.OrderBy(a => a.operation.OperationDate);

            //    }
            //    else
            //    {
            //        result = result.OrderByDescending(a => a.operation.OperationDate);

            //    }
            //}
            //else
            //{
            //    if (Direction == "ASC")
            //    {
            //        result = result.OrderBy(a => a.customer.FullName);

            //    }
            //    else
            //    {
            //        result = result.OrderByDescending(a => a.customer.FullName);

            //    }
            //}
            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<BookingViewModel>(objs, page, pagesize, totalCount);
            return PartialView("_list", model);
        }




        public ActionResult _getuserlist(string searchvalue = null,
         DateTime? fromdate = null, DateTime? todate = null, string cardata = null)
        {
            Expression<Func<tbCustomer, bool>> searchfilter = null;
            Expression<Func<tbOperation, bool>> datefilter, washtypefilter, statusfilter, bookingsourcefilter = null;
            Expression<Func<tbCustomerVehicle, bool>> carfilter = null;
            if (searchvalue != "" && searchvalue != null)
            {
                searchfilter = PredicateBuilder.New<tbCustomer>();
                searchfilter = searchfilter.Or(l => l.FullName.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.UserName.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.PhoneNo.Contains(searchvalue));
                searchfilter = searchfilter.Or(l => l.Email.Contains(searchvalue));
            }
            else
            {
                searchfilter = l => l.IsDeleted != true;
            }

            if (cardata != "" && cardata != null)
            {
                carfilter = PredicateBuilder.New<tbCustomerVehicle>();
                carfilter = carfilter.Or(l => l.VehicleName.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleNo.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleColor.Contains(searchvalue));
                carfilter = carfilter.Or(l => l.VehicleBrand.Contains(searchvalue));
            }
            else
            {
                carfilter = l => l.IsDeleted != true;
            }

            if(fromdate == null)
            {
                fromdate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
            }



            var result = (from operation in uow.operationRepo.GetAll().
                          Where(a => a.IsDeleted != true && a.OperationDate >= fromdate)
                          join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true)
                          .Where(searchfilter)
                          on operation.CustomerId equals customer.Id
                          join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
                                  .Where(carfilter).Where(carfilter)
                          on operation.CustomerVehicleId equals car.Id      
                          join chat in uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true)
                          on customer.Id.ToString() equals chat.UserID
                          select new {
                              operation,
                              car.VehicleBrand,
                              car.VehicleName,
                              car.VehicleNo,
                          }).DistinctBy(a => a.operation.Id);


            var messagelist = uow.chatMessageRepo.GetAll().Where(a => a.IsConversationEnd != true && a.IsDeleted != true && a.OperationDate >= fromdate).AsQueryable();


            if(messagelist.Count() > 0)
            {
                IQueryable<ChatDataViewModel> data = (from d in result
                                                      select new ChatDataViewModel
                                                      {
                                                          vehiclebrand = d.VehicleBrand,
                                                          vehiclename = d.VehicleName,
                                                          vehicleno = d.VehicleNo,
                                                          userid = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().UserID.ToString(),
                                                          username = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().UserName.ToString(),
                                                          senddate = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().SendDateTime,
                                                          lastmessage = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().Message.ToString(),
                                                          isread = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().Type == "Admin" ? true : false,
                                                          type = messagelist.Where(a => a.OperationID == d.operation.Id).OrderByDescending(a => a.SendDateTime).FirstOrDefault().Type.ToString(),

                                                      }).AsQueryable();
                var totalCount = data.Count();



                //var model = new PagedListClient<BookingViewModel>(objs, page, pagesize, totalCount);
                return PartialView("_getuserlist", data);
            }
            else
            {
                IQueryable<ChatDataViewModel> data = null;
                
                return PartialView("_getuserlist", data);
            }
        


         
        }






        public ActionResult newstatuschange(int id = 0, string status = null)
        {
            tbOperation operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            if(status == "Waiting")
            {
                operation.ConfirmedTime = MyExtension.getLocalTime(DateTime.UtcNow);
                operation.Status = "Confirmed";
            }

            if (status == "Confirmed")
            {
                operation.FinishedTime = MyExtension.getLocalTime(DateTime.UtcNow);
                operation.Status = "Finished";
            }

            

            operation = uow.operationRepo.UpdateWithObj(operation);
            return Json(operation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult statusCancel(int id = 0)
        {
            tbOperation operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();         
            operation.CancelTime = MyExtension.getLocalTime(DateTime.UtcNow);
            operation.Status = "Cancel";         
            operation = uow.operationRepo.UpdateWithObj(operation);
            return Json(operation, JsonRequestBehavior.AllowGet);
        }


        public ActionResult getWaitingNotiCount()
        {
            var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
            int operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.OperationDate >= today && a.Status == "Waiting").Count();
           
            return Json(operation, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCarListByCustomer(string customerid = null)
        {
            var data = (from veh in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.CustomerId == customerid)
                        join cat in uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true)
                        on veh.CarCategoryId equals cat.Id
                        select new CarDDViewModel()
                        {
                            carid = veh.Id,
                            carname = veh.VehicleName,
                            carbrand = veh.VehicleBrand,
                            carcategoryid = cat.Id,
                            carcategoryname = cat.Name,
                            carcategorytype = cat.Type
                        }).AsQueryable();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCarCategoryByID(int id = 0)
        {
            var data = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetAdditionalService(string cartype = null)
        {
            IQueryable data = uow.additionalServiceRepo.GetAll().Where(a => a.IsDeleted != true && a.CarType == cartype).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetTownshipList()
        {
            IQueryable data = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerList()
        {
            IQueryable data = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBrandListByTownship(int townshipid = 0)
        {
            IQueryable data = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.TownshipId == townshipid).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Add(string customerid = null)
        {
            ViewBag.customerid = customerid;
            tbOperation operation = new tbOperation();
            return View(operation);

        }

        //public ActionResult BookingSuccess(int id = 0)
        //{
        //    // ViewBag.customerid = customerid;
        //    BookingSuccessModel bk = new BookingSuccessModel();
        //    bk.operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
        //    bk.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CustomerVehicleId).FirstOrDefault();
        //    bk.carCategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CarCategoryId).FirstOrDefault();
        //    bk.photos = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == bk.operation.CustomerVehicleId).AsQueryable();
        //    return View(bk);

        //}



        public async System.Threading.Tasks.Task<ActionResult> SaveBooking(tbOperation obj)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            tbOperation UpdateEntity = null;
            var branch = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.BranchId).FirstOrDefault();
            if (branch != null)
            {
                obj.TownshipId = branch.TownshipId;
                var township = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.TownshipId).FirstOrDefault();
                if (township != null)
                {
                    obj.TownshipName = township.Name;
                }
                obj.BranchName = branch.LocationName;
            }

            //var carcategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.CarCategoryId).FirstOrDefault();
            //if(carcategory != null)
            //{
            //    obj.CarCategoryName = carcategory.Name;
            //    obj.CarCategoryType = carcategory.Type;
            //}


            if (obj.Id > 0)
            {
                UpdateEntity = uow.operationRepo.UpdateWithObj(obj);
            }
            else
            {
                obj.BookingSource = "Portal";
                obj.IsDeleted = false;
                obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow);
                obj.OperationDate = DateTime.Parse(obj.OperationDate.Value.ToShortDateString() + " " + obj.StartTime.Value.ToShortTimeString());
                obj.Status = "Waiting";
                obj.StartTime = null;
                UpdateEntity = uow.operationRepo.InsertReturn(obj);
            }

            var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
            int operationcount = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.OperationDate >= today && a.Status == "Waiting").Count();

            if (UpdateEntity != null)
            {
                hubContext.Clients.All.getWaitingNotiCount(operationcount);
                return Json(UpdateEntity, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }



    }
}