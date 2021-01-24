using Data.Helper;
using Infra.helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class Admin_AdvertisementController : Controller
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public Admin_AdvertisementController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        // GET: Admin_Advertisement
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _list(int pagesize = 10, int page = 1, string searchvalue = null, string OrderBy = "Accesstime",
      string Direction = "ASC")
        {
           

            IQueryable<tbAdvertisement> result = uow.adsRepo.GetAll().Where(a => a.IsDeleted != true && a.IsActive == true).OrderByDescending(a => a.PostedDate).AsQueryable();
            var totalCount = result.Count();


            ViewBag.pagesize = pagesize;
            ViewBag.page = page;
         
            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<tbAdvertisement>(objs, page, pagesize, totalCount);
            return PartialView("_list", model);
        }


        public ActionResult AdvertisementForm(int ID = 0, string FormType = "Add")
        {
            ViewBag.formtype = FormType;
            if (ID > 0)
            {
                tbAdvertisement data = uow.adsRepo.GetAll().Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();
                return View(data);
            }
            else
            {
                tbAdvertisement data = new tbAdvertisement();
                return View(data);
            }
        }



        public async System.Threading.Tasks.Task<ActionResult> UpsertData(tbAdvertisement obj)
        {
            var today = MyExtension.getLocalTime(DateTime.UtcNow).Date;
            tbAdvertisement UpdateEntity = null;

           


            if (obj.ID > 0)
            {
                var olddata = uow.adsRepo.GetAll().Where(a => a.IsDeleted != true && a.ID == obj.ID).FirstOrDefault();
                if (obj.Photo != null)
                {
                    if(obj.IsGif == true)
                    {
                            FileUploadViewModel fileupload = new FileUploadViewModel();
                            fileupload.photo = obj.Photo;
                            fileupload.filepath = "/ImageStorage/CarWaterlessProject/Advertisement";
                            var responsefile = await FileUploadApiRequestHelper.uploadgif(fileupload);

                            obj.Photo = responsefile;
                        }
                    else
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = obj.Photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Advertisement";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                        obj.Photo = responsefile;
                    }

                }
                else
                {
                   
                    obj.Photo = olddata.Photo;
                }


                    if (obj.Duration == "1 month")
                    {
                        if (olddata.FromDate <= today && olddata.ToDate >= today)
                        {
                            obj.FromDate = olddata.FromDate;
                            obj.ToDate = olddata.ToDate.Value.AddMonths(1).Date;
                        }
                        else
                        {
                            obj.FromDate = today;
                            obj.ToDate = today.AddMonths(1).Date;
                        }

                    }
                    else if (obj.Duration == "3 months")
                    {
                        if (olddata.FromDate <= today && olddata.ToDate >= today)
                        {
                            obj.FromDate = olddata.FromDate;
                            obj.ToDate = olddata.ToDate.Value.AddMonths(3).Date;
                        }
                        else
                        {
                            obj.FromDate = today;
                            obj.ToDate = today.AddMonths(3).Date;
                        }

                    }
                    else if (obj.Duration == "6 months")
                    {
                        if (olddata.FromDate <= today && olddata.ToDate >= today)
                        {
                            obj.FromDate = olddata.FromDate;
                            obj.ToDate = olddata.ToDate.Value.AddMonths(6).Date;
                        }
                        else
                        {
                            obj.FromDate = today;
                            obj.ToDate = today.AddMonths(6).Date;
                        }


                    }
                    else if (obj.Duration == "9 months")
                    {
                        if (olddata.FromDate <= today && olddata.ToDate >= today)
                        {
                            obj.FromDate = olddata.FromDate;
                            obj.ToDate = olddata.ToDate.Value.AddMonths(9).Date;
                        }
                        else
                        {
                            obj.FromDate = today;
                            obj.ToDate = today.AddMonths(9).Date;
                        }

                    }
                    else if (obj.Duration == "12 months")
                    {
                        if (olddata.FromDate <= today && olddata.ToDate >= today)
                        {
                            obj.FromDate = olddata.FromDate;
                            obj.ToDate = olddata.ToDate.Value.AddYears(1).Date;
                        }
                        else
                        {
                            obj.FromDate = today;
                            obj.ToDate = today.AddYears(1).Date;
                        }

                    }
                    
              

                UpdateEntity = uow.adsRepo.UpdateWithObj(obj);
            }
            else
            {
                if (obj.Duration != null)
                {
                    if (obj.Duration == "1 month")
                    {
                        obj.FromDate = today;
                        obj.ToDate = today.AddMonths(1).Date;
                    }
                    if (obj.Duration == "3 months")
                    {
                        obj.FromDate = today;
                        obj.ToDate = today.AddMonths(3).Date;
                    }
                    else if (obj.Duration == "6 months")
                    {
                        obj.FromDate = today;
                        obj.ToDate = today.AddMonths(6).Date;

                    }
                    else if (obj.Duration == "9 months")
                    {
                        obj.FromDate = today;
                        obj.ToDate = today.AddMonths(9).Date;
                    }
                    else if (obj.Duration == "12 months")
                    {
                        obj.FromDate = today;
                        obj.ToDate = today.AddYears(1).Date;
                    }
                  
                }

                if (obj.Photo != null)
                {
                    if(obj.IsGif == true)
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = obj.Photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Advertisement";
                        var responsefile = await FileUploadApiRequestHelper.uploadgif(fileupload);

                        obj.Photo = responsefile;
                    }
                    else
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = obj.Photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Advertisement";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                        obj.Photo = responsefile;
                    }

                   
                }

                obj.IsActive = true;
                obj.IsDeleted = false;
                obj.PostedDate = MyExtension.getLocalTime(DateTime.UtcNow);
                UpdateEntity = uow.adsRepo.InsertReturn(obj);
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



        public ActionResult Delete(int ID = 0)
        {

            tbAdvertisement UpdateEntity;
            tbAdvertisement photo = uow.adsRepo.GetAll().Where(a => a.ID == ID).Where(a => a.IsDeleted != true).FirstOrDefault();
            photo.IsDeleted = true;
            //  photo.Accesstime = MyExtension.getLocalTime(DateTime.UtcNow);
            UpdateEntity = uow.adsRepo.UpdateWithObj(photo);

            if (UpdateEntity != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult IsActive(int ID = 0)
        {
            tbAdvertisement data = uow.adsRepo.GetAll().Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();

            if (data.IsActive == true)
            {
                data.IsActive = false;
                data = uow.adsRepo.UpdateWithObj(data);
            }
            else
            {
                data.IsActive = true;
                data = uow.adsRepo.UpdateWithObj(data);
            }


            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }

    }
}