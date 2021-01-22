using Data.Helper;
using Infra.helper;
using Infra.Helper;
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
    public class Admin_CustomerController : Controller
    {
        // GET: AdminCustomer
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public Admin_CustomerController()
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



            IQueryable<tbCustomer> result = uow.customerRepo.GetAll().Where(searchfilter).Where(a => a.IsDeleted != true).AsQueryable();
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
            else
            {
                if (Direction == "ASC")
                {
                    result = result.OrderBy(a => a.UserName);

                }
                else
                {
                    result = result.OrderByDescending(a => a.UserName);

                }
            }
            var skipindex = pagesize * (page - 1);
            var objs = result.Skip(skipindex).Take(pagesize).ToList();

            var model = new PagedListClient<tbCustomer>(objs, page, pagesize, totalCount);
            return PartialView("_list", model);
        }



        public ActionResult OperationList(int customerid = 0)
        {
            ViewBag.customerid = customerid;
            return View();
        }


        public ActionResult _operationlist(int pagesize = 10, int page = 1, string searchvalue = null,
         string OrderBy = "Accesstime", string Direction = "DESC",
         DateTime? fromdate = null, DateTime? todate = null, string type = null
         , string washtype = null, string cardata = null, string bookingsource = null
          ,int customerid = 0,string carno = null)
        {
            Expression<Func<tbCustomer, bool>> searchfilter = null;
            Expression<Func<tbOperation, bool>> datefilter, washtypefilter, statusfilter, bookingsourcefilter = null;
            Expression<Func<tbCustomerVehicle, bool>> carfilter, carnofilter = null;
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

            if (carno != null && carno != "")
            {

                carnofilter = l => l.VehicleNo == carno;
            }
            else
            {
                carnofilter = l => l.IsDeleted != true;
            }


            if (fromdate != null && todate != null)
            {
                fromdate = fromdate.Value.Date;
                todate = todate.Value.AddDays(1).Date;
                datefilter = l => l.FinishedTime >= fromdate && l.FinishedTime < todate;
            }
            else
            {
                var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
                var nextday = today.AddDays(1).Date;
                datefilter = l => l.FinishedTime >= today && l.FinishedTime < nextday;
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
                                                             Where(a => a.IsDeleted != true && a.CustomerId == customerid && a.Status == "Finished").Where(datefilter).Where(washtypefilter)
                                                             .Where(statusfilter).Where(bookingsourcefilter)
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == customerid)
                                                   .Where(searchfilter)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
                                                   .Where(carfilter).Where(carfilter).Where(carnofilter)
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
            return PartialView("_operationlist", model);
        }



        public ActionResult _exceloperationlist(int pagesize = 10, int page = 1, string searchvalue = null,
        string OrderBy = "Accesstime", string Direction = "DESC",
        DateTime? fromdate = null, DateTime? todate = null, string type = null
        , string washtype = null, string cardata = null, string bookingsource = null
         , int customerid = 0,string carno = null)
        {
            Expression<Func<tbCustomer, bool>> searchfilter = null;
            Expression<Func<tbOperation, bool>> datefilter, washtypefilter, statusfilter, bookingsourcefilter = null;
            Expression<Func<tbCustomerVehicle, bool>> carfilter, carnofilter = null;
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


            if (carno != null && carno != "")
            {

                carnofilter = l => l.VehicleNo == carno;
            }
            else
            {
                carnofilter = l => l.IsDeleted != true;
            }





            if (fromdate != null && todate != null)
            {
                fromdate = fromdate.Value.Date;
                todate = todate.Value.AddDays(1).Date;
                datefilter = l => l.FinishedTime >= fromdate && l.FinishedTime < todate;
            }
            else
            {
                var today = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow).Date;
                var nextday = today.AddDays(1).Date;
                datefilter = l => l.FinishedTime >= today && l.FinishedTime < nextday;
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
                                                             Where(a => a.IsDeleted != true && a.CustomerId == customerid && a.Status == "Finished").Where(datefilter).Where(washtypefilter)
                                                             .Where(statusfilter).Where(bookingsourcefilter)
                                                   join customer in uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == customerid)
                                                   .Where(searchfilter)
                                                   on operation.CustomerId equals customer.Id
                                                   join car in uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true)
                                                   .Where(carfilter).Where(carfilter).Where(carnofilter)
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
            return PartialView("_exceloperationlist", model);
        }




        public ActionResult getCarNoByCustomer(string customerid = null)
        {
            IQueryable<string> result = uow.customerVehicleRepo.Get().Where(a => a.CustomerId == customerid && a.IsDeleted != true).Select(a => a.VehicleNo).AsQueryable();
          
            return Json(result, JsonRequestBehavior.AllowGet);
        }








        public ActionResult CustomerForm(int ID = 0,string FormType = "Add")
        {
            ViewBag.formtype = FormType;
            if(ID > 0)
            {
                tbCustomer data = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == ID).FirstOrDefault();
                return View(data);
            }
            else
            {
                tbCustomer data = new tbCustomer();
                return View(data);             
            }
        }

        public ActionResult IsMember(int ID = 0)
        {           
             tbCustomer data = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == ID).FirstOrDefault();
             
             if(data.IsMember == true)
             {
                data.IsMember = false;
                data = uow.customerRepo.UpdateWithObj(data);
             }
            else
            {
                data.IsMember = true;
                data = uow.customerRepo.UpdateWithObj(data);
            }

            
             if(data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
          
        }

        public ActionResult AddPrepaidCode(int ID = 0,decimal? prepaidamt = null)
        {
            tbCustomer data = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == ID).FirstOrDefault();

            if(prepaidamt != null)
            {
                data.IsPrepaid = true;
                data.PrepaidAmount = prepaidamt;
                data.PrepaidLeftAmount = prepaidamt;
                data = uow.customerRepo.UpdateWithObj(data);
            }
            else
            {
                data.IsPrepaid = false;
                data.PrepaidAmount = null;
                data.PrepaidLeftAmount = null;
                data = uow.customerRepo.UpdateWithObj(data);
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





        public async System.Threading.Tasks.Task<ActionResult> UpsertData(tbCustomer obj)
        {
            tbCustomer UpdateEntity = null;
          
        
            if (obj.Id > 0)
            {
                if (obj.Photo != null)
                {
                    FileUploadViewModel fileupload = new FileUploadViewModel();
                    fileupload.photo = obj.Photo;
                    fileupload.filepath = "/ImageStorage/CarWaterlessProject/Customer";
                    var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                    obj.Photo = responsefile;
                }
                else
                {
                    var olddata = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.Id).FirstOrDefault();
                    obj.Photo = olddata.Photo;
                }

                UpdateEntity = uow.customerRepo.UpdateWithObj(obj);
            }
            else
            {
                if (obj.Photo != null)
                {
                    FileUploadViewModel fileupload = new FileUploadViewModel();
                    fileupload.photo = obj.Photo;
                    fileupload.filepath = "/ImageStorage/CarWaterlessProject/Customer";
                    var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                    obj.Photo = responsefile;
                }
               
                obj.IsDeleted = false;
                obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow);              
                UpdateEntity = uow.customerRepo.InsertReturn(obj);
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


        public ActionResult Car(string customerid = null)
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




     
        public ActionResult CarForm(int Id = 0, string type = "Add", string customerid = null)
        {
            ViewBag.id = Id;
            ViewBag.formtype = type;
            if (Id > 0)
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


            if (UpdateEntity != null)
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
                    catch (Exception ex)
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