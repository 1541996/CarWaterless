using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWaterless.Business;
using Infra.ViewModels;

namespace CarWaterless.Controllers
{
    public class AdminSetupController : Controller
    {
        // GET: AdminSetup
        #region Township
        public ActionResult Township()
        {
            TownshipViewModel model = new TownshipViewModel();
            return View(model);
            //HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            //if (reqCookies != null)
            //{
            //    TownshipViewModel model = new TownshipViewModel();
            //    return View(model);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Login");
            //}

        }

        [HttpPost]
        public ActionResult Township(TownshipViewModel model)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            bool isvalid = true;
            if (model.Id == 0)
            {
                isvalid = repository.CheckExistTownship(model);
                if (isvalid)
                {
                    model = repository.SaveTownship(model);
                }
                else
                {
                    model = new TownshipViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            else
            {
                isvalid = repository.CheckExistUpdateTownship(model);
                if (isvalid)
                {
                    model = repository.EditTownship(model);
                }
                else
                {
                    model = new TownshipViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            return Json(model);
        }

        public ActionResult GetAllTownship()
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            List<TownshipViewModel> list = repository.GetAllTownship();
            return Json(list);
        }

        public ActionResult EditTownship(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            TownshipViewModel model = repository.GetTownshipbyId(id);
            return Json(model);
        }

        public ActionResult DeleteTownship(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            TownshipViewModel model = repository.DeleteTownship(id);
            return Json(model);
        }
        #endregion

    }
}