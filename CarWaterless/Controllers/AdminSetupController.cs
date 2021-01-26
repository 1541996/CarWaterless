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
        #region CarCategory
        public ActionResult CarCategory()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                CarCategoryViewModel model = new CarCategoryViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult CarCategory(CarCategoryViewModel model)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            bool isvalid = true;
            if (model.Id == 0)
            {
                isvalid = repository.CheckExistCarCategory(model);
                if (isvalid)
                {
                    model = repository.SaveCarCategory(model);
                }
                else
                {
                    model = new CarCategoryViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            else
            {
                isvalid = repository.CheckExistUpdateCarCategory(model);
                if (isvalid)
                {
                    model = repository.EditCarCategory(model);
                }
                else
                {
                    model = new CarCategoryViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            return Json(model);
        }

        public ActionResult GetAllCarCategory()
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            List<CarCategoryViewModel> list = repository.GetAllCarCategory();
            return Json(list);
        }

        public ActionResult EditCarCategory(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            CarCategoryViewModel model = repository.GetCarCategorybyId(id);
            return Json(model);
        }

        public ActionResult DeleteCarCategory(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            CarCategoryViewModel model = repository.DeleteCarCategory(id);
            return Json(model);
        }
        #endregion

        #region Township
        public ActionResult Township()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                TownshipViewModel model = new TownshipViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

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

        #region AdditionalService
        public ActionResult AdditionalService()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                AdditionalServiceViewModel model = new AdditionalServiceViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult AdditionalService(AdditionalServiceViewModel model)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            bool isvalid = true;
            if (model.Id == 0)
            {
                isvalid = repository.CheckExistAdditionalService(model);
                if (isvalid)
                {
                    model = repository.SaveAdditionalService(model);
                }
                else
                {
                    model = new AdditionalServiceViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            else
            {
                isvalid = repository.CheckExistUpdateAdditionalService(model);
                if (isvalid)
                {
                    model = repository.EditAdditionalService(model);
                }
                else
                {
                    model = new AdditionalServiceViewModel();
                    model.MessageType = 2;
                    model.Message = "Already Exist!";
                }
            }
            return Json(model);
        }

        public ActionResult GetAllAdditionalService()
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            List<AdditionalServiceViewModel> list = repository.GetAllAdditionalService();
            return Json(list);
        }

        public ActionResult EditAdditionalService(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            AdditionalServiceViewModel model = repository.GetAdditionalServicebyId(id);
            return Json(model);
        }

        public ActionResult DeleteAdditionalService(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            AdditionalServiceViewModel model = repository.DeleteAdditionalService(id);
            return Json(model);
        }
        #endregion

        #region Branch
        public ActionResult BranchList()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                BranchViewModel model = new BranchViewModel();
                AdminSetupRepository repository = new AdminSetupRepository();
                model.TownshipList = new SelectList(repository.GetAllTownship(), "Id", "Name");
                AdminRepository adminRepository = new AdminRepository();
                model.AdminAgentList = new SelectList(adminRepository.GetAllAgents(), "Id", "FullName");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult GetAllBranch(BranchViewModel model)
        {
            List<BranchViewModel> list = new List<BranchViewModel>();
            AdminSetupRepository repository = new AdminSetupRepository();
            model.IsActive = true;
            list = repository.GetAllBranch(model);
            return Json(list);
        }

        public ActionResult Branch(int id=0)
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                BranchViewModel model = new BranchViewModel();
                AdminSetupRepository repository = new AdminSetupRepository();
                AdminRepository adminRepository = new AdminRepository();
                if (id != 0)
                {
                    model = repository.GetBranchbyId(id);
                    model.CreateUserId = Convert.ToInt32(reqCookies["userid"].ToString());
                   
                }
                model.TownshipList = new SelectList(repository.GetAllTownship(), "Id", "Name");

                model.AdminAgentList = new SelectList(adminRepository.GetAllAgents(), "Id", "FullName");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public async System.Threading.Tasks.Task<ActionResult> Branch(BranchViewModel model)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            if (model.Id == 0)
            {
                var checkexit = repository.CheckExistBranch(model);
                if (checkexit == true)
                {
                    model = await repository.SaveBranchAsync(model);
                }
                else
                {
                    model = new BranchViewModel();
                    model.MessageType = 2;
                    model.Message = "Branch already exists";
                }

            }
            else
            {
                var checkexit = repository.CheckExistUpdateBranch(model);
                if (checkexit == true)
                {
                    model = await repository.EditBranchAsync(model);
                }
                else
                {
                    model = new BranchViewModel();
                    model.MessageType = 2;
                    model.Message = "Branch already exists";
                }

            }
            return Json(model);
        }

        public ActionResult DeleteBranch(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            BranchViewModel model = new BranchViewModel();
            model = repository.DeleteBranch(id);
            return Json(model);
        }

        #endregion

        #region MemberPackage
        public ActionResult MemberPackage()
        {
            HttpCookie reqCookies = Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                MemberPackageViewModel model = new MemberPackageViewModel();
                AdminSetupRepository repository = new AdminSetupRepository();
                model.AdditionalServiceList = new SelectList(repository.GetAllAdditionalService(), "Id", "Name");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> MemberPackage(MemberPackageViewModel model)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            
            if (model.ID == 0)
            {
                model = await repository.SaveMemberPackageAsync(model);
            }
            else
            {
                model = await repository.EditMemberPackageAsync(model);
            }
            return Json(model);
        }

        public ActionResult GetAllMemberPackage()
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            List<MemberPackageViewModel> list = repository.GetAllMemberPackage();
            return Json(list);
        }

        public ActionResult EditMemberPackage(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            MemberPackageViewModel model = repository.GetMemberPackagebyId(id);
            return Json(model);
        }

        public ActionResult DeleteMemberPackage(int id)
        {
            AdminSetupRepository repository = new AdminSetupRepository();
            MemberPackageViewModel model = repository.DeleteMemberPackage(id);
            return Json(model);
        }

        public ActionResult BindServiceByCarType(string cartype)
        {
            List<Infra.Models.tbAdditionalService> lst = new List<Infra.Models.tbAdditionalService>();
            AdminSetupRepository repository = new AdminSetupRepository();
            lst = repository.BindServiceByCarType(cartype);
            return Json(lst);
        }
        #endregion
    }
}