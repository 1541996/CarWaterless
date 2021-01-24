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
        public ActionResult Index(string fromuserid = null,string touserid = null,int operationid = 0,string type = null)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            ViewBag.operationid = operationid;
            ViewBag.fromuserid = fromuserid;
            ViewBag.touserid = touserid;
            ViewBag.type = type;
            if (type == "admin")
            {

                tbCustomer cus = uow.customerRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == touserid).FirstOrDefault();
                ViewBag.username = cus.FullName;
                ViewBag.photo = cus.Photo;

                hubContext.Clients.All.chatAppear(touserid, fromuserid, operationid);
            }
            else
            {
                tbAdmin admin = uow.adminRepo.GetAll().Where(a => a.IsDeleted != true && a.Id.ToString() == touserid).FirstOrDefault();
                ViewBag.username = admin.FullName;
                ViewBag.photo = "";


            }


          
            return View();
        }


        public ActionResult _chatList(string fromuserid = null, string touserid = null, int operationid = 0)
        {
            ViewBag.operationid = operationid;
            ViewBag.fromuserid = fromuserid;
            ViewBag.touserid = touserid;

            var messagesfromuser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == fromuserid && a.ToUserID == touserid
                                   && a.OperationID == operationid).AsQueryable();
            //receive messages by user 1 

            var messagestouser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == touserid 
                                 && a.ToUserID == fromuserid && a.OperationID == operationid).AsQueryable();

            List<tbChatMessage> result = null;
            result = messagesfromuser.Union(messagestouser).OrderBy(a => a.SendDateTime).ToList();

            return PartialView("_chatList", result);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> sendMessageAsync(ChatViewModel obj)
        {
            ViewBag.fromuserid = obj.fromuserid;
            ViewBag.touserid = obj.touserid;


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
                    messageobj.FromUserID = obj.fromuserid;
                    messageobj.ToUserID = obj.touserid;
                    messageobj.IsDeleted = false;
                  //  messageobj.Message = obj.message;
                    messageobj.OperationID = obj.operationid ?? 0;
                    messageobj.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
                    UpdatePhoto = uow.chatMessageRepo.InsertReturn(messageobj);
                   
                    if(obj.message != null && obj.message != "")
                    {
                       
                        tbChatMessage messageobj2 = new tbChatMessage();

                        
                        messageobj2.FromUserID = obj.fromuserid;
                        messageobj2.ToUserID = obj.touserid;
                        messageobj2.IsDeleted = false;
                        messageobj2.Message = obj.message;
                        messageobj2.OperationID = obj.operationid ?? 0;
                        messageobj2.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
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

                messageobj.FromUserID = obj.fromuserid;
                messageobj.ToUserID = obj.touserid;
                messageobj.IsDeleted = false;
                messageobj.Message = obj.message;
                messageobj.OperationID = obj.operationid ?? 0;
                messageobj.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
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