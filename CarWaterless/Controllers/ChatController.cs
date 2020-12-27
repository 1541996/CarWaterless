using Data.Helper;
using Infra.Models;
using Infra.UnitOfWork;
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
        public ActionResult Index(string fromuserid = null,string touserid = null,int operationid = 0)
        {
            ViewBag.operationid = operationid;
            ViewBag.fromuserid = fromuserid;
            ViewBag.touserid = touserid;
            return View();
        }


        public ActionResult _chatList(string fromuserid = null, string touserid = null, int operationid = 0)
        {
            ViewBag.operationid = operationid;
            ViewBag.fromuserid = fromuserid;
            ViewBag.touserid = touserid;

            var messagesfromuser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == fromuserid && a.ToUserID == touserid).AsQueryable();
            //receive messages by user 1 

            var messagestouser = uow.chatMessageRepo.GetAll().Where(a => a.IsDeleted != true).Where(a => a.FromUserID == touserid && a.ToUserID == fromuserid).AsQueryable();

            List<tbChatMessage> result = null;
            result = messagesfromuser.Union(messagestouser).OrderBy(a => a.SendDateTime).ToList();

            return PartialView("_chatList", result);
        }


        [HttpPost]
        public ActionResult sendMessage(string fromuserid = null,string touserid = null,string message = null)
        {
            ViewBag.fromuserid = fromuserid;
            ViewBag.touserid = touserid;


            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();          
            tbChatMessage UpdateEntity = null;
            tbChatMessage messageobj = new tbChatMessage();
          
            messageobj.FromUserID = fromuserid;
            messageobj.ToUserID = touserid;
            messageobj.IsDeleted = false;
            messageobj.Message = message;
            messageobj.SendDateTime = MyExtension.getLocalTime(DateTime.UtcNow);
            UpdateEntity = uow.chatMessageRepo.InsertReturn(messageobj);

            hubContext.Clients.All.chatAdd("Success");

            List<tbChatMessage> resultList = new List<tbChatMessage>();
            resultList.Add(UpdateEntity ?? new tbChatMessage());
            return PartialView("_chatList", resultList);

            // string msgdatastring = Serialize(UpdateEntity);
            //  hubContext.Clients.All.sendmsg(msgdatastring);       
        }

    }
}