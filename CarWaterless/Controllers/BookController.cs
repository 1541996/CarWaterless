using Data.Helper;
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
                        select new VehicleCategoryViewModel()
                        {
                            vehicle = veh,
                            category = cat
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

        public ActionResult GetBrandListByTownship(int townshipid = 0)
        {
            IQueryable data = uow.branchRepo.GetAll().Where(a => a.IsDeleted != true && a.TownshipId == townshipid).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index(string customerid = null)
        {
            ViewBag.customerid = customerid;
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

            if (UpdateEntity != null)
            {
                return Json(UpdateEntity, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }


    }
}