using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWaterless.Business;
using Infra.ViewModels;

namespace CarWaterless.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public ActionResult EditProfile()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            AdminViewModel model = new AdminViewModel();
            if (reqCookies != null)
            {
                int id = int.Parse(reqCookies["userid"].ToString());
                AdminRepository repository = new AdminRepository();
                model.Id = id;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ChangePassword()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            AdminViewModel model = new AdminViewModel();
            if (reqCookies != null)
            {
                int id = int.Parse(reqCookies["userid"].ToString());
                AdminRepository repository = new AdminRepository();
                model = repository.GetById(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(AdminViewModel model)
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                AdminRepository repository = new AdminRepository();
                model = repository.EditProfile(model);
                return Json(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(AdminViewModel model)
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                AdminRepository repository = new AdminRepository();
                model = repository.ChangePassword(model);
                return Json(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Add()
        {
            AdminViewModel model = new AdminViewModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Add(AdminViewModel model)
        {
            AdminRepository repository = new AdminRepository();
            model = repository.Save(model);
            
            return Json(model);
        }

        [HttpPost]
        public JsonResult CheckUserNameValid(string username)
        {
            AdminRepository repository = new AdminRepository();
            int count = repository.CheckUserNameValid(username);
            return Json(count);
        }
    }
}