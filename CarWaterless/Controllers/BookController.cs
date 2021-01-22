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


        public ActionResult Index(string customerid = null,string source = "APP")
        {
            ViewBag.bksource = source;
            ViewBag.customerid = customerid;
            tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == customerid).FirstOrDefault();
            if(customer.IsMember == true)
            {
                ViewBag.ismember = true;
            }
            tbOperation operation = new tbOperation();
            return View(operation);
            
        }

        public ActionResult BookingSuccess(int id = 0)
        {
            // ViewBag.customerid = customerid;
            BookingSuccessModel bk = new BookingSuccessModel();
            bk.operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == id).FirstOrDefault();
            bk.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CustomerVehicleId).FirstOrDefault();
            bk.carCategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CarCategoryId).FirstOrDefault();
            bk.photos = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == bk.operation.CustomerVehicleId).AsQueryable();
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