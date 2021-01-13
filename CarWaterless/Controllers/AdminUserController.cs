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
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["newsenseInfo"];
            AdminViewModel model = new AdminViewModel();
            if (reqCookies != null)
            {
                int id = int.Parse(reqCookies["userid"].ToString());
                int userrole = int.Parse(reqCookies["userrole"].ToString());
                AdminRepository repository = new AdminRepository();
                if (userrole == 1)
                {
                    model.Id = id;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult GetAllUser(AdminViewModel model)
        {
            AdminRepository repository = new AdminRepository();
            List<AdminViewModel> list = repository.GetAllUser(model);
            return Json(list);
        }
        public ActionResult Add()
        {
            HttpCookie reqCookies = Request.Cookies["newsenseInfo"];
            AdminViewModel model = new AdminViewModel();
            if (reqCookies != null)
            {
                int id = int.Parse(reqCookies["userid"].ToString());
                int userrole = int.Parse(reqCookies["userrole"].ToString());
                AdminRepository repository = new AdminRepository();
                if (userrole == 1)
                {
                    model.CreateUserId = id;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Add(AdminViewModel model)
        {
            AdminRepository repository = new AdminRepository();
            model = repository.AddUser(model);
            return Json(model);
        }
        public ActionResult EditProfile()
        {
            HttpCookie reqCookies = Request.Cookies["newsenseInfo"];
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
            HttpCookie reqCookies = Request.Cookies["newsenseInfo"];
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

        [HttpPost]
        public ActionResult EditProfile(AdminViewModel model)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
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
            HttpCookie reqCookies = Request.Cookies["userInfo"];
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

        public ActionResult DeactivateUser(int id)
        {
            AdminRepository repository = new AdminRepository();
            AdminViewModel model = repository.De_activateUser(id, false);
            return Json(model);
        }

        public ActionResult ActivateUser(int id)
        {
            AdminRepository repository = new AdminRepository();
            AdminViewModel model = repository.De_activateUser(id, true);
            return Json(model);
        }

    }
}