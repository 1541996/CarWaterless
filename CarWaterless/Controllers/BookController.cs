using Data.Helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public BookController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
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


        public ActionResult GetMemberPackages(string cartype = null)
        {
            IQueryable data = uow.memberPackageRepo.GetAll().Where(a => a.IsDeleted != true && a.CarType == cartype).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTownshipList()
        {
            IQueryable data = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBrandListByTownship(int townshipid = 0)
        {
            IQueryable data = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.TownshipId == townshipid).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index(string customerid = null,string source = "APP",int branchid = 0,int townshipid = 0)
        {
            ViewBag.bksource = source;
            ViewBag.customerid = customerid;
            ViewBag.branchid = branchid;
            ViewBag.townshipid = townshipid;

            tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid).FirstOrDefault();
            if(customer.IsMember == true)
            {
                ViewBag.ismember = true;
            }
            tbOperation operation = new tbOperation();
            return View(operation);
            
        }



        public ActionResult bookedlist(string customerid = null)
        {
            // ViewBag.bksource = source;
            ViewBag.customerid = customerid;
            return View();

        }


        public ActionResult _finishedlist(string customerid = null,int page = 0,int pagesize = 0)
        {
            IQueryable<BookingViewModel> result = (from operation in uow.operationRepo.GetAll().
                                                           Where(a => a.IsDeleted != true && a.CustomerId.ToString() == customerid && a.Status == "Finished")
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
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


            result = result.OrderByDescending(a => a.FinishedDate);


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;

            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<BookingViewModel>(objs, page, pagesize, totalCount);


            if (model.Results.Count != 0)
            {
                return PartialView("_finishedlist", model);
            }
            else
            {
                if (page == 1)
                {
                    return PartialView("_nobookingdata", result);
                }
                else
                {
                    return Json("NoResult", JsonRequestBehavior.AllowGet);
                }


            }


        }



        public ActionResult _confirmedlist(string customerid = null, int page = 0, int pagesize = 0)
        {
            IQueryable<BookingViewModel> result = (from operation in uow.operationRepo.GetAll().
                                                           Where(a => a.IsDeleted != true && a.CustomerId.ToString() == customerid && a.Status == "Confirmed")
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
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
                                                       Township = operation.TownshipName,
                                                       customername = customer.FullName,


                                                   }).AsQueryable();
            var totalCount = result.Count();


            result = result.OrderByDescending(a => a.ConfirmedDate);


            return PartialView("_confirmedlist", result);

        }




        public ActionResult _waitinglist(string customerid = null, int page = 0, int pagesize = 0)
        {
            IQueryable<BookingViewModel> result = (from operation in uow.operationRepo.GetAll().
                                                   Where(a => a.IsDeleted != true && a.CustomerId.ToString() == customerid && a.Status == "Waiting")
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
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
                                                       Township = operation.TownshipName,
                                                       customername = customer.FullName,


                                                   }).AsQueryable();
            var totalCount = result.Count();


            result = result.OrderByDescending(a => a.OperationDate);


          

            return PartialView("_waitinglist", result);

        }





        public ActionResult RatingList(string customerid = null)
        {
            ViewBag.customerid = customerid;
            return View();
        }


        public ActionResult _ratinglist(string customerid = null, int page = 0, int pagesize = 0)
        {
            IQueryable<BookingViewModel> result = (from operation in uow.operationRepo.GetAll().
                                                           Where(a => a.IsDeleted != true && a.CustomerId.ToString() == customerid && a.Status == "Finished" && a.IsRated != true)
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
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


            result = result.OrderByDescending(a => a.FinishedDate);


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;

            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<BookingViewModel>(objs, page, pagesize, totalCount);


            if (model.Results.Count != 0)
            {
                return PartialView("_ratinglist", model);
            }
            else
            {
                if (page == 1)
                {
                    return PartialView("_nobookingdata", result);
                }
                else
                {
                    return Json("NoResult", JsonRequestBehavior.AllowGet);
                }


            }


        }




        public ActionResult RatedForm(int operationid = 0,string customerid = null)
        {
            ViewBag.customerid = customerid;
            ViewBag.operationid = operationid;
            return View();
        }

        
        public ActionResult saveRating(string ratingscore = null,string feedback = null,int operationid = 0)
        {
            tbOperation operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == operationid && a.Status == "Finished").FirstOrDefault();

            if(operation != null)
            {
                operation.IsRated = true;
                operation.StarRate = ratingscore;
                operation.Feedback = feedback;

                uow.operationRepo.UpdateWithObj(operation);

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

       

        }





        public ActionResult BookingSuccess(int id = 0)
        {
            // ViewBag.customerid = customerid;
            BookingSuccessModel bk = new BookingSuccessModel();
            bk.operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            bk.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CustomerVehicleId).FirstOrDefault();
            bk.carCategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CarCategoryId).FirstOrDefault();
            bk.photourl = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == bk.operation.CustomerVehicleId).Select(a => a.PhotoUrl).FirstOrDefault();
            bk.CustomerName = uow.customerRepo.GetAll().Where(a => a.Id == bk.operation.CustomerId).Select(a => a.FullName).FirstOrDefault();



           

            return View(bk);


        }



        public async System.Threading.Tasks.Task<ActionResult> SaveBooking(tbOperation obj)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            tbOperation UpdateEntity = null;
            var branch = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.BranchId).FirstOrDefault();
            if(branch != null)
            {
                obj.TownshipId = branch.TownshipId;
                var township = uow.townshipRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.TownshipId).FirstOrDefault();
                if(township != null)
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

            if(obj.WashOption == "In-House")
            {
                obj.TransportationCharges = 500;
            }
            else
            {
                obj.TransportationCharges = null;
            }


            if (obj.Id > 0)
            {
                UpdateEntity = uow.operationRepo.UpdateWithObj(obj);
            }
            else
            {                
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