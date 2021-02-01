using Data.Helper;
using Infra.helper;
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
    public class ChatController : Controller
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public ChatController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }
        // GET: Chat
        public ActionResult Index(string userid = null,string username = null,int operationid = 0)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            ViewBag.operationid = operationid;
            ViewBag.userid = userid;
            ViewBag.username = username;
            ViewBag.type = "User";
           

           
            return View();
        }

        public ActionResult Admin(string userid = null, string username = null, int operationid = 0)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            ViewBag.operationid = operationid;
            ViewBag.userid = userid;
            ViewBag.username = username;
            ViewBag.type = "Admin";


            hubContext.Clients.All.chatAppear(userid, username, operationid);

            return View();
        }

        public ActionResult getBookingDetail(int operationid = 0)
        {
            // ViewBag.customerid = customerid;
            BookingSuccessModel bk = new BookingSuccessModel();
            bk.operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == operationid).FirstOrDefault();
            bk.vehicle = uow.customerVehicleRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CustomerVehicleId).FirstOrDefault();
            bk.carCategory = uow.carCategoryRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == bk.operation.CarCategoryId).FirstOrDefault();
            bk.photo = uow.photoRepo.GetAll().Where(a => a.IsDeleted != true && a.CarID == bk.operation.CustomerVehicleId).Select(a => a.Photo).FirstOrDefault();
            bk.CustomerName = uow.customerRepo.GetAll().Where(a => a.Id == bk.operation.CustomerId).Select(a => a.FullName).FirstOrDefault();





            return PartialView("getBookingData", bk);


        }


        public ActionResult _chatList(string userid = null, int operationid = 0,string type = null)
        {
            ViewBag.userid = userid;
            ViewBag.type = type;

            //var messagesfromuser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == fromuserid && a.ToUserID == touserid
            //                       && a.OperationID == operationid).AsQueryable();
            ////receive messages by user 1 

            //var messagestouser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == touserid 
            //                     && a.ToUserID == fromuserid && a.OperationID == operationid).AsQueryable();

            //List<tbChatMessage> result = null;
            //result = messagesfromuser.Union(messagestouser).OrderBy(a => a.SendDateTime).ToList();

            List<tbChatMessage> result = null;
            result = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.OperationID == operationid).ToList();

            return PartialView("_chatList", result);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> sendMessageAsync(ChatViewModel obj)
        {
            ViewBag.userid = obj.userid;
            ViewBag.type = obj.type;

            ViewBag.fromuserid = obj.userid;

            var operation = uow.operationRepo.GetAll().Where(a => a.IsDeleted != true && a.Id == obj.operationid).FirstOrDefault();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();        
            

            if(obj.file != null && obj.file != "")
            {
                List<string> photolist = new List<string>();
                //  tbPhoto photoobj = new tbPhoto();
            
                photolist = obj.file.Split('~').ToList<string>();
               
                List<FileUploadViewModel> fileuploadlist = new List<FileUploadViewModel>();
                if (photolist.Count > 0)
                {

                    foreach (var photo in photolist)
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Chat";
                        fileuploadlist.Add(fileupload);
                    }


                    List<string> responsefilelist = await FileUploadApiRequestHelper.uploadlist(fileuploadlist);

                    var photoconcat = "";
                    foreach (var photo in responsefilelist)
                    {
                        photoconcat += photo + ",";
                      
                    }

                    photoconcat = photoconcat.TrimEnd(',');



                    tbChatMessage UpdatePhoto = null;
                    tbChatMessage UpdateEntitiy = null;
                    tbChatMessage messageobj = new tbChatMessage();

                    messageobj.Photo = photoconcat;
                    messageobj.UserID = obj.userid;
                 
                    messageobj.IsDeleted = false;
                    messageobj.Type = obj.type;
                  //  messageobj.Message = obj.message;
                    messageobj.OperationID = obj.operationid ?? 0;
                    messageobj.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
                    messageobj.OperationDate = operation.OperationDate;
                    messageobj.UserName = obj.username;
                    UpdatePhoto = uow.chatMessageRepo.InsertReturn(messageobj);
                   
                    if(obj.message != null && obj.message != "")
                    {
                       
                        tbChatMessage messageobj2 = new tbChatMessage();

                        
                        messageobj2.UserID = obj.userid;
                        messageobj2.Type = obj.type;
                        messageobj2.IsDeleted = false;
                        messageobj2.Message = obj.message;
                        messageobj2.OperationID = obj.operationid ?? 0;
                        messageobj2.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
                        messageobj2.OperationDate = operation.OperationDate;
                        messageobj2.UserName = obj.username;
                        UpdateEntitiy = uow.chatMessageRepo.InsertReturn(messageobj2);
                    }

                    hubContext.Clients.All.chatAdd("Success");

                    List<tbChatMessage> resultList = new List<tbChatMessage>();

                  

                    resultList.Add(UpdatePhoto ?? new tbChatMessage());
                    if (UpdateEntitiy != null)
                    {
                        resultList.Add(UpdateEntitiy ?? new tbChatMessage());
                    }

                    return PartialView("_chatList", resultList);


                }

            }
            else
            {
                tbChatMessage UpdateEntity = null;
                tbChatMessage messageobj = new tbChatMessage();

                messageobj.UserID = obj.userid;
                messageobj.Type = obj.type;
                messageobj.IsDeleted = false;
                messageobj.Message = obj.message;
                messageobj.OperationID = obj.operationid ?? 0;
                messageobj.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
                messageobj.OperationDate = operation.OperationDate;
                messageobj.UserName = obj.username;
                UpdateEntity = uow.chatMessageRepo.InsertReturn(messageobj);

                hubContext.Clients.All.chatAdd("Success");

                List<tbChatMessage> resultList = new List<tbChatMessage>();
                resultList.Add(UpdateEntity ?? new tbChatMessage());
                return PartialView("_chatList", resultList);
            }



            return null;

            // string msgdatastring = Serialize(UpdateEntity);
            //  hubContext.Clients.All.sendmsg(msgdatastring);       
        }

    }
}