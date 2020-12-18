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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Add()
        {
            CarPhotoModel obj = new CarPhotoModel();
            return View();
        }

        public ActionResult Edit(int Id = 0)
        {
            CarPhotoModel obj = new CarPhotoModel();
            obj.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == Id).FirstOrDefault();
            obj.photos = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == Id).AsQueryable();
            return View(obj);
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
                tbPhoto photoobj = new tbPhoto();
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


                    List<FileUploadViewModel> responsefilelist = await FileUploadApiRequestHelper.uploadlist(fileuploadlist);

                    List<tbPhoto> savephotolist = new List<tbPhoto>();
                    foreach (var photo in responsefilelist)
                    {
                        photoobj.Photo = photo.photo;
                        photoobj.Type = "Vehicle";
                        photoobj.CarID = UpdateEntity.Id;
                        photoobj.IsDeleted = false;
                        photoobj.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
                        savephotolist.Add(photoobj);
                    }

                    uow.photoRepo.InsertReturnList(savephotolist);


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
   
    
    }
}