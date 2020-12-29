using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWaterless.Business;
using Infra.ViewModels;
using Data.Helper;

namespace CarWaterless.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
            if (Request.Cookies["carwaterlessinfo"] != null)
            {
                Response.Cookies["carwaterlessinfo"].Expires = MyExtension.getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
            }
            //CommonRepository.ClearLoginData();
            return View(model);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(AdminViewModel model)
        {
            AdminRepository repository = new AdminRepository();

            model = repository.Authenticate(model.UserName, model.Password);

            if (model.MessageType == 1)
            {
                CommonRepository.StoreLoginData(model);
                //CommonRepository.CheckExpiredVouchers();
            }

            return Json(model);
        }

       
        
    }
}