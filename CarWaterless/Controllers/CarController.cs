using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Helper;
using Infra.helper;
using Infra.Helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;

namespace CarWaterless.Controllers
{
    public class CarController : Controller
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public CarController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }
        // GET: Car
        public ActionResult Index(string customerid = null)
        {
            ViewBag.customerid = customerid;
            return View();
        }

        public ActionResult GetCarList(string customerid = null)
        {
            var data = (from veh in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.CustomerId == customerid)
                        join cat in uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true)
                        on veh.CarCategoryId equals cat.Id
                        select new VehicleCategoryViewModel()
                        {
                            vehicle = veh,
                            category = cat
                        }).AsQueryable();

            return PartialView("_getCarList", data);
        }


       

        public ActionResult Detail(int Id = 0)
        {
            CarPhotoModel obj = new CarPhotoModel();
            obj.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == Id).FirstOrDefault();
            obj.carcategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.vehicle.CarCategoryId).FirstOrDefault();
            obj.photos = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == Id).AsQueryable();
            return View(obj);
        }

        public ActionResult Add(int Id = 0,string type = "Add",string customerid = null)
        {
            ViewBag.id = Id;
            ViewBag.formtype = type;
            if(Id > 0)
            {
                CarPhotoModel obj = new CarPhotoModel();
                obj.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == Id).FirstOrDefault();
                obj.carcategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.vehicle.CarCategoryId).FirstOrDefault();
                obj.photos = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == Id).AsQueryable();
                return View(obj);
            }
            else
            {
                CarPhotoModel obj = new CarPhotoModel();
                //  obj.photos = new List<tbPhoto>();
                obj.carcategory = new tbCarCategory();
                obj.vehicle = new tbCustomerVehicle();
                obj.vehicle.CustomerId = customerid;
                return View(obj);
            }
            
        }
    
        public async System.Threading.Tasks.Task<ActionResult> SaveCarAsync(CarPhotoModel obj)
        {
            tbCustomerVehicle UpdateEntity = null;
            
            if (obj.vehicle.Id > 0)
            {
                UpdateEntity = uow.customerVehicleRepo.UpdateWithObj(obj.vehicle);
            }
            else
            {
                obj.vehicle.IsDeleted = false;
                obj.vehicle.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow);               
                UpdateEntity = uow.customerVehicleRepo.InsertReturn(obj.vehicle);
            }


            if(UpdateEntity != null)
            {
                List<string> photolist = new List<string>();
              //  tbPhoto photoobj = new tbPhoto();
                if (obj.vehicle.CarPhoto != null && obj.vehicle.CarPhoto != "")
                {
                    photolist = obj.vehicle.CarPhoto.Split('~').ToList<string>();
                }
                List<FileUploadViewModel> fileuploadlist = new List<FileUploadViewModel>();
                if (photolist.Count > 0)
                {

                    foreach (var photo in photolist)
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/CustomerVehicle";
                        fileuploadlist.Add(fileupload);
                    }


                    List<string> responsefilelist = await FileUploadApiRequestHelper.uploadlist(fileuploadlist);

                    List<tbPhoto> savephotolist = new List<tbPhoto>();
                    foreach (var photo in responsefilelist)
                    {
                        tbPhoto photoobj = new tbPhoto();
                        photoobj.Photo = photo;
                        photoobj.Type = "Vehicle";
                        photoobj.CarID = UpdateEntity.Id;
                        photoobj.IsDeleted = false;
                        photoobj.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                        savephotolist.Add(photoobj);
                    }

                    try
                    {
                        uow.photoRepo.InsertReturnList(savephotolist);
                    }
                    catch(Exception ex)
                    {

                    }

                  


                }


            }


            if (UpdateEntity != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }
       
        public ActionResult DeletePhoto(int photoId = 0)
        {

            tbPhoto UpdateEntity;
            tbPhoto photo = uow.photoRepo.GetAll().Where(a => a.ID == photoId).Where(a => a.IsDeleted != true).FirstOrDefault();
            photo.IsDeleted = true;
          //  photo.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
            UpdateEntity = uow.photoRepo.UpdateWithObj(photo);

            if (UpdateEntity != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetCategory(string cartype = null)
        {
            IQueryable data = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Type == cartype).AsQueryable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       
    
    
    }
}