using Infra.Helper;
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
    public class PushNotiTestController : ApiController
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public PushNotiTestController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        [HttpGet]
        [Route("api/test/sendnotiall")]
        public HttpResponseMessage sendnoti(HttpRequestMessage request)
        {
            FCMViewModel fcm = new FCMViewModel();
            fcm.to = "/topics/GoldChannel_Message_Topic";

            fcmdata fcmdata = new fcmdata();
            fcmdata.type = "All";
            fcmdata.title = "Booking";
            fcmdata.body = "Booking is successfully done.";
         
            fcm.data = fcmdata;
            FCMRequestHelper.sendTokenMessage(fcm);

            return request.CreateResponse<string>(HttpStatusCode.OK, "Success");
          

        }


        [HttpGet]
        [Route("api/test/sendnotiindividual")]
        public HttpResponseMessage ssendnotiindividualendnoti(HttpRequestMessage request,int customerid = 0)
        {
            var user = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == customerid).FirstOrDefault();

            FCMViewModel fcm = new FCMViewModel();
            fcm.to = user.UserToken;

            fcmdata fcmdata = new fcmdata();
            fcmdata.type = "Individual";
            fcmdata.title = "Booking";
            fcmdata.body = "Booking is successfully done.";
            fcmdata.weburl = "https://www.google.com/";

            Notification notification = new Notification();
            notification.title = "Booking";
            notification.body = "Booking is successfully done.";


            fcm.notification = notification;
            fcm.data = fcmdata;
            FCMRequestHelper.sendTokenMessage(fcm);

            return request.CreateResponse<string>(HttpStatusCode.OK, "Success");



        }




        //    FCMViewModel fcm = new FCMViewModel();
        //    fcm.to = "/topics/GoldChannel_Message_Topic";
        //            fcm.priority = "high";
        //            fcm.content_available = true;

        //            Notification notification = new Notification();
        //    notification.body = UpdateEntity.Message;
        //            notification.title = UpdateEntity.Title;

        //            DataModel data = new DataModel();

        //            if (channel != null)
        //            {
        //                data.catId = channel.CategoryName != null ? channel.CategoryName.Replace("_", ",") : "";
        //                data.itemId = channel.ID.ToString();
        //                data.type = channel.MovieType;
        //                data.catName = channel.CategoryName != null ? channel.CategoryName.Replace("_", ",") : "";
        //            }


        //fcm.notification = notification;
        //            fcm.data = data;



        //            var response = FCMRequestHelper.sendTokenMessage(fcm);
    }

}
