using CarWaterless.Helper;
using Infra.helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarWaterless.Controllers
{
    public class API_LoginController : ApiController
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public API_LoginController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        [HttpPost]
        [Route("api/user/login")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> loginAsync(HttpRequestMessage request, tbCustomer cus)
        {
            // normal login
            if (cus.UserName != null && cus.Password != null)
            {
                try
                {
                    tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.UserName == cus.UserName && a.Password == cus.Password).FirstOrDefault();
                    if (customer != null)
                    {
                        customer.UserToken = cus.UserToken;
                        customer.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                        customer = uow.customerRepo.UpdateWithObj(customer);
                        if (customer != null)
                        {
                            customer.ReturnStatus = "Success";
                        }

                        return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                    }
                    else
                    {
                        customer = new tbCustomer();
                        customer.ReturnStatus = "Fail";
                        return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                    }
                }
                catch(Exception ex)
                {

                }

                return null;
               
            }
           
            // fb login
            else if (cus.FacebookId != null)
            {
                tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.FacebookId == cus.FacebookId).FirstOrDefault();
                if(customer != null)
                {                  
                    customer.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer = uow.customerRepo.UpdateWithObj(customer);
                    if (customer != null)
                    {
                        customer.ReturnStatus = "Success";
                    }
                    return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                }
                else
                {
                    // photo upload

                    if(cus.Photo != null)
                    {
                        string base64 = CommonHelper.downloadphoto(cus.Photo);
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = base64;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Customer";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                    }

                    customer.FacebookId = cus.FacebookId;
                    customer.FullName = cus.FullName;
                    customer.UserName = cus.UserName;
                    customer.UserAppId = cus.UserAppId;
                    customer.Email = cus.Email;
                    customer.IsDeleted = false;
                    customer.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer.CreateDate = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer = uow.customerRepo.InsertReturn(customer);
                    if (customer != null)
                    {
                        customer.ReturnStatus = "Success";
                    }
                    return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                }
            }

            // google login
            else if (cus.Email != null)
            {
                tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.Email == cus.Email).FirstOrDefault();
                if (customer != null)
                {
                    customer.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer = uow.customerRepo.UpdateWithObj(customer);
                    if (customer != null)
                    {
                        customer.ReturnStatus = "Success";
                    }
                    return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                }
                else
                {
                    if (cus.Photo != null)
                    {
                        string base64 = CommonHelper.downloadphoto(cus.Photo);
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = base64;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Customer";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                    }

                    customer.FullName = cus.FullName;
                    customer.UserName = cus.UserName;
                    customer.UserAppId = cus.UserAppId;
                    customer.IsDeleted = false;
                    customer.Email = cus.Email;
                    customer.IsDeleted = false;
                    customer.FacebookId = customer.FacebookId;
                    customer.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer.CreateDate = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
                    customer = uow.customerRepo.InsertReturn(customer);
                    if (customer != null)
                    {
                        customer.ReturnStatus = "Success";
                    }
                    return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
                }
            }
            else
            {
                tbCustomer customer = new tbCustomer();
                customer.ReturnStatus = "Fail";
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
            }

          


        }


        [HttpPost]
        [Route("api/user/register")]
        public HttpResponseMessage register(HttpRequestMessage request, tbCustomer cus)
        {
            var emailexists = uow.customerRepo.GetAll().Where(a => a.Email == cus.Email).Any();
            tbCustomer customer = new tbCustomer();
            if(emailexists == true)
            {
                customer.ReturnStatus = "Email Exists";
                customer.ReturnMessage = "Email account already exists in our system.";
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
            }

            var usernameexists = uow.customerRepo.GetAll().Where(a => a.UserName == cus.UserName).Any();
            if(usernameexists == true)
            {
                customer.ReturnStatus = "Username Exists.";
                customer.ReturnMessage = "Username already exists in our system.";
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
            }

            tbCustomer obj = new tbCustomer();
            obj.LastLoginTime = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
            obj.CreateDate = Data.Helper.MyExtension.getLocalTime(DateTime.UtcNow);
            obj.IsDeleted = false;
            obj.FullName = cus.FullName;
            obj.UserName = cus.UserName;
            obj.UserAppId = cus.UserAppId;
            obj.PhoneNo = cus.PhoneNo;
            obj.Email = cus.Email;
            obj.Password = cus.Password;
            obj = uow.customerRepo.InsertReturn(obj);
            if (customer != null)
            {
                obj.ReturnStatus = "Success";
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, obj);
            }
            else
            {
                obj = new tbCustomer();
                obj.ReturnStatus = "Fail";
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, obj);
            }

            

        }



        [HttpPost]
        [Route("api/user/editData")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> editDataAsync(HttpRequestMessage request, tbCustomer User)
        {
            string filepath = "careme/patient";
            tbCustomer userdata = new tbCustomer();
           // AzurePhotoUpload iPhoto = new AzurePhotoUpload();

            userdata = uow.customerRepo.GetAll().Where(a => a.Id == User.Id).Where(a => a.IsDeleted != true).FirstOrDefault();
            if (User != null)
            {
                userdata.FullName = User.FullName;
            }
            else
            {
                userdata.FullName = userdata.FullName;

            }
            if (User.PhoneNo != null)
            {
                userdata.PhoneNo = User.PhoneNo;
            }
            else
            {
                userdata.PhoneNo = userdata.PhoneNo;

            }
            if (User.UserName != null)
            {
                userdata.UserName = User.UserName;
            }
            else
            {
                userdata.UserName = userdata.UserName;

            }


            if (User.Photo != null)
            {
                FileUploadViewModel fileupload = new FileUploadViewModel();
                fileupload.photo = User.Photo;
                fileupload.filepath = "/ImageStorage/CarWaterlessProject/Customer";
                var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                userdata.Photo = responsefile;
              
                
            }
            else
            {
                userdata.Photo = userdata.Photo;
            }
        
            userdata.IsDeleted = false;
           
            userdata = uow.customerRepo.UpdateWithObj(userdata);

            return request.CreateResponse(HttpStatusCode.OK, userdata);

        }


        [HttpGet]
        [Route("api/user/getUserData")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> getUserData(HttpRequestMessage request, int customerid = 0)
        {
           
            tbCustomer userdata = uow.customerRepo.GetAll().Where(a => a.Id == customerid).Where(a => a.IsDeleted != true).FirstOrDefault();
            if(userdata.Photo != null)
            {
                userdata.Photo = $"http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/Customer/{userdata.Photo}";
            }
            else
            {
                userdata.Photo = $"http://ecowash.centurylinks-stock.com/ArchitectThemes/image/admin.jpg";

            }

            return request.CreateResponse(HttpStatusCode.OK, userdata);

        }




        [HttpGet]
        [Route("api/user/saveFCMToken")]
        public HttpResponseMessage saveFCMToken(HttpRequestMessage request, string UserAppID, string Token)
        {
            string updatEntity;
            tbCustomer user = uow.customerRepo.Get().Where(a => a.UserAppId == UserAppID).Where(a => a.IsDeleted != true).FirstOrDefault();

            if (user != null)
            {
                user.UserToken = Token;
                user = uow.customerRepo.UpdateWithObj(user);
                updatEntity = "Success";

            }
            else
            {
                updatEntity = "Fail";
            }


            return request.CreateResponse(HttpStatusCode.OK, updatEntity);

        }



        [HttpGet]
        [Route("api/user/getNoti")]
        public HttpResponseMessage NotiList(HttpRequestMessage request, int customerid = 0)
        {
            List<tbNotification> objs = uow.notificationRepo.GetAll().Where(a => a.IsDeleted != true && a.CustomerId == customerid).OrderByDescending(r => r.MessageSendDateTime).Take(50).ToList();
          //  List<tbNotification> SpecificNoti = uow.notificationRepo.GetAll().Where(a => a.NotiType == "Specific").Where(a => a.CustomerId.ToString() == customerid).Where(a => a.IsDeleted != true).ToList();
          //  var objs = GeneralNoti.ToList().Union(SpecificNoti.ToList()).OrderByDescending(r => r.MessageSendDateTime);

            //var totalCount = objs.Count();
            //var results = objs.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            //var model = new PagedListServer<tbNotification>(results, totalCount, pageSize);
            return request.CreateResponse(HttpStatusCode.OK, objs);


        }
    }
}
