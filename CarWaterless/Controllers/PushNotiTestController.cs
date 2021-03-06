﻿using Infra.Helper;
using Infra.Models;
using Infra.UnitOfWork;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        [Route("api/test/deletephoto")]
        public HttpResponseMessage deletephoto(HttpRequestMessage request,string filename = null)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/ArchitectThemes/image/" + filename)))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/ArchitectThemes/image/" + filename));
                // Response.Redirect("~/pages/management/helpfiles.aspx");                
            }

            return request.CreateResponse<string>(HttpStatusCode.OK, "Success");


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
        public async Task<HttpResponseMessage> ssendnotiindividualendnotiAsync(HttpRequestMessage request,int customerid = 0,string title = "hh", string body = "hh")
        {
            var user = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == customerid).FirstOrDefault();

            FCMViewModel fcm = new FCMViewModel();
            //fcm.to = "dtlB16NARPqi0oOM81C-4n:APA91bGqdGqSTBwP8IwR-aQVRSIIcNV3aMNXwdNYcAWCm1ECovl0zJzxlvKdOGARUvV3RTKrbFxDsv1avckpOhNnjN23d2Ix201RT9B5xzDMVxtP8lUjMtRoNDSURG-B0SS267PXI7De";

             fcm.to = "cUKmV_3nRNi_lVShkwY5ry:APA91bH6ER_e4O99kyy58qlNXDlnzF5qkFVqL-uRFv9JqO7uoq2Gc_JGJpwcJN9auSvZmLhMLtkgz9AM8B_SYvWHD0Xpjqd9Uz3nLWyAB4opyrULW07ztxMASoNOyVvNNOxtQJD2lCpC";
        
            fcmdata fcmdata = new fcmdata();
            fcmdata.type = "Individual";
            fcmdata.title = "Booking";
            fcmdata.body = "Booking is successfully done.";
            fcmdata.weburl = "https://www.google.com/";

            Notification notification = new Notification();
            notification.title = title;
            notification.body = body;


            fcm.notification = notification;
            fcm.data = fcmdata;
            await FCMRequestHelper.sendTokenMessage(fcm);

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
