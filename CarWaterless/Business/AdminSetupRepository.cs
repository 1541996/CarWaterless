using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infra.ViewModels;
using Infra.Models;
using Infra.Helper;
using Data.Helper;

namespace CarWaterless.Business
{
    public class AdminSetupRepository
    {
        #region tbCarCategory
        public List<CarCategoryViewModel> GetAllCarCategory()
        {
            List<CarCategoryViewModel> lst = new List<CarCategoryViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbCarCategories
                             where data.IsDeleted == false
                             select new CarCategoryViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 Type = data.Type,
                                 BasicPrice = data.BasicPrice,
                                 IsActive = data.IsActive,
                             });
                lst = query.AsEnumerable().Select((data, index) => new CarCategoryViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Type = data.Type,
                    BasicPrice = data.BasicPrice,
                    IsActive = data.IsActive,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public CarCategoryViewModel GetCarCategorybyId(int id)
        {
            CarCategoryViewModel model = new CarCategoryViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbCarCategories
                             where data.IsDeleted == false && data.Id == id
                             select new CarCategoryViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 Type = data.Type,
                                 BasicPrice = data.BasicPrice,
                                 IsActive = data.IsActive,
                             });
                model = query.AsEnumerable().Select((data, index) => new CarCategoryViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Type = data.Type,
                    BasicPrice = data.BasicPrice,
                    IsActive = data.IsActive,
                }).FirstOrDefault();
            }
            return model;
        }

        public CarCategoryViewModel SaveCarCategory(CarCategoryViewModel model)
        {
            CarCategoryViewModel response = new CarCategoryViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbCarCategory obj = new tbCarCategory();
                    obj.Name = model.Name;
                    obj.Type = model.Type;
                    obj.BasicPrice = model.BasicPrice;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    context.tbCarCategories.Add(obj);
                    context.SaveChanges();
                }
                response.MessageType = 1;
                response.Message = "Save Successful.";
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Save Failed.";
            }

            return response;
        }

        public bool CheckExistCarCategory(CarCategoryViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbCarCategories.Where(x => x.Name == model.Name && x.Type == model.Type && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public CarCategoryViewModel EditCarCategory(CarCategoryViewModel model)
        {
            CarCategoryViewModel response = new CarCategoryViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    context.tbCarCategories.First(x => x.Id == model.Id).Name = model.Name;
                    context.tbCarCategories.First(x => x.Id == model.Id).Type = model.Type;
                    context.tbCarCategories.First(x => x.Id == model.Id).BasicPrice = model.BasicPrice;
                    context.SaveChanges();

                    response.MessageType = 1;
                    response.Message = "Update Successful.";
                }
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Update failed.";
            }
            return response;
        }
        public bool CheckExistUpdateCarCategory(CarCategoryViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbCarCategories.Where(x => x.Name == model.Name && x.Type == model.Type
                && x.BasicPrice == model.BasicPrice
                && x.Id == model.Id && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public CarCategoryViewModel DeleteCarCategory(int id)
        {
            CarCategoryViewModel model = new CarCategoryViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbCarCategories.First(x => x.Id == id).IsDeleted = true;
                context.SaveChanges();


                model.MessageType = 1;
                model.Message = "";
            }

            return model;
        }


        #endregion

        #region tbTownship
        public List<TownshipViewModel> GetAllTownship()
        {
            List<TownshipViewModel> lst = new List<TownshipViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbTownships
                             where data.IsDeleted == false
                             select new TownshipViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                             });
                lst = query.AsEnumerable().Select((data, index) => new TownshipViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public TownshipViewModel GetTownshipbyId(int id)
        {
            TownshipViewModel model = new TownshipViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbTownships
                             where data.IsDeleted == false && data.Id == id
                             select new TownshipViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                             });
                model = query.AsEnumerable().Select((data, index) => new TownshipViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                }).FirstOrDefault();
            }
            return model;
        }

        public TownshipViewModel SaveTownship(TownshipViewModel model)
        {
            TownshipViewModel response = new TownshipViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbTownship obj = new tbTownship();
                    obj.Name = model.Name;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    context.tbTownships.Add(obj);
                    context.SaveChanges();
                }
                response.MessageType = 1;
                response.Message = "Save Successful.";
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Save Failed.";
            }

            return response;
        }

        public bool CheckExistTownship(TownshipViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbTownships.Where(x => x.Name == model.Name && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public TownshipViewModel EditTownship(TownshipViewModel model)
        {
            TownshipViewModel response = new TownshipViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    context.tbTownships.First(x => x.Id == model.Id).Name = model.Name;
                    context.SaveChanges();

                    response.MessageType = 1;
                    response.Message = "Update Successful.";
                }
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Update failed.";
            }
            return response;
        }
        public bool CheckExistUpdateTownship(TownshipViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbTownships.Where(x => x.Name == model.Name && x.Id == model.Id && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public TownshipViewModel DeleteTownship(int id)
        {
            TownshipViewModel model = new TownshipViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbTownships.First(x => x.Id == id).IsDeleted = true;
                context.SaveChanges();


                model.MessageType = 1;
                model.Message = "";
            }

            return model;
        }

        #endregion

        #region tbAdditionalService
        public List<AdditionalServiceViewModel> GetAllAdditionalService()
        {
            List<AdditionalServiceViewModel> lst = new List<AdditionalServiceViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdditionalServices
                             where data.IsDeleted == false
                             select new AdditionalServiceViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 CarType = data.CarType,
                                 Price = data.Price,
                                 DiscountPrice = data.DiscountPrice,
                                 IsActive = data.IsActive,
                             });
                lst = query.AsEnumerable().Select((data, index) => new AdditionalServiceViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    CarType = data.CarType,
                    Price = data.Price,
                    DiscountPrice = data.DiscountPrice,
                    IsActive = data.IsActive,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public AdditionalServiceViewModel GetAdditionalServicebyId(int id)
        {
            AdditionalServiceViewModel model = new AdditionalServiceViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdditionalServices
                             where data.IsDeleted == false && data.Id == id
                             select new AdditionalServiceViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 CarType = data.CarType,
                                 Price = data.Price,
                                 DiscountPrice = data.DiscountPrice,
                                 IsActive = data.IsActive,
                             });
                model = query.AsEnumerable().Select((data, index) => new AdditionalServiceViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    CarType = data.CarType,
                    Price = data.Price,
                    DiscountPrice = data.DiscountPrice,
                    IsActive = data.IsActive,
                }).FirstOrDefault();
            }
            return model;
        }

        public AdditionalServiceViewModel SaveAdditionalService(AdditionalServiceViewModel model)
        {
            AdditionalServiceViewModel response = new AdditionalServiceViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbAdditionalService obj = new tbAdditionalService();
                    obj.Name = model.Name;
                    obj.CarType = model.CarType;
                    obj.Price = model.Price;
                    obj.DiscountPrice = model.DiscountPrice;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    context.tbAdditionalServices.Add(obj);
                    context.SaveChanges();
                }
                response.MessageType = 1;
                response.Message = "Save Successful.";
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Save Failed.";
            }

            return response;
        }

        public bool CheckExistAdditionalService(AdditionalServiceViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbAdditionalServices.Where(x => x.Name == model.Name && x.CarType == model.CarType && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public AdditionalServiceViewModel EditAdditionalService(AdditionalServiceViewModel model)
        {
            AdditionalServiceViewModel response = new AdditionalServiceViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    context.tbAdditionalServices.First(x => x.Id == model.Id).Name = model.Name;
                    context.tbAdditionalServices.First(x => x.Id == model.Id).CarType = model.CarType;
                    context.tbAdditionalServices.First(x => x.Id == model.Id).Price = model.Price;
                    context.SaveChanges();

                    response.MessageType = 1;
                    response.Message = "Update Successful.";
                }
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Update failed.";
            }
            return response;
        }
        public bool CheckExistUpdateAdditionalService(AdditionalServiceViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbAdditionalServices.Where(x => x.Name == model.Name && x.CarType == model.CarType
                && x.Price == model.Price && x.DiscountPrice == model.DiscountPrice
                && x.Id == model.Id && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public AdditionalServiceViewModel DeleteAdditionalService(int id)
        {
            AdditionalServiceViewModel model = new AdditionalServiceViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbAdditionalServices.First(x => x.Id == id).IsDeleted = true;
                context.SaveChanges();


                model.MessageType = 1;
                model.Message = "";
            }

            return model;
        }


        #endregion

        #region tbBranch
        public List<BranchViewModel> GetAllBranch(BranchViewModel model)
        {
            List<BranchViewModel> lst = new List<BranchViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbBranches
                             join town in context.tbTownships on data.TownshipId equals town.Id
                             join adminagent in context.tbAdmins on data.AdminAgentId equals adminagent.Id
                             where data.IsDeleted == false && data.IsActive == model.IsActive
                             orderby data.CreateDate ascending
                             select new BranchViewModel
                             {
                                 Id = data.Id,
                                 LocationName = data.LocationName,
                                 LocationPhoneNo = data.LocationPhoneNo,
                                 LocationAddress = data.LocationAddress,
                                 AdminAgentId = data.AdminAgentId,
                                 AdminAgentName = adminagent.FullName,
                                 CarLimit = data.CarLimit,
                                 CloseTime = data.CloseTime,
                                 OpenTime = data.OpenTime,
                                 TownshipId = data.TownshipId,
                                 TownshipName = town.Name,
                                 IsActive = data.IsActive,
                             });
                if (model.TownshipId != null)
                {
                    query = query.Where(x => x.TownshipId == model.TownshipId);
                }
                if (model.AdminAgentId != null)
                {
                    query = query.Where(x => x.AdminAgentId == model.AdminAgentId);
                }
                lst = query.AsEnumerable().Select((data, index) => new BranchViewModel()
                {
                    Id = data.Id,
                    LocationName = data.LocationName,
                    LocationPhoneNo = data.LocationPhoneNo,
                    LocationAddress = data.LocationAddress,
                    AdminAgentId = data.AdminAgentId,
                    AdminAgentName = data.AdminAgentName,
                    CarLimit = data.CarLimit,
                    CloseTime = data.CloseTime,
                    OpenTime = data.OpenTime,
                    TownshipId = data.TownshipId,
                    TownshipName = data.TownshipName,
                    IsActive = data.IsActive,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public BranchViewModel GetBranchbyId(int id)
        {
            BranchViewModel model = new BranchViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbBranches
                             join town in context.tbTownships on data.TownshipId equals town.Id
                             join adminagent in context.tbAdmins on data.AdminAgentId equals adminagent.Id
                             where data.IsDeleted == false && data.Id == id
                             select new BranchViewModel
                             {
                                 Id = data.Id,
                                 LocationName = data.LocationName,
                                 LocationPhoneNo = data.LocationPhoneNo,
                                 LocationAddress = data.LocationAddress,
                                 AdminAgentId = data.AdminAgentId,
                                 AdminAgentName = adminagent.FullName,
                                 CarLimit = data.CarLimit,
                                 CloseTime = data.CloseTime,
                                 OpenTime = data.OpenTime,
                                 TownshipId = data.TownshipId,
                                 TownshipName = town.Name,
                                 IsActive = data.IsActive,
                             });
                model = query.AsEnumerable().Select((data, index) => new BranchViewModel()
                {
                    Id = data.Id,
                    LocationName = data.LocationName,
                    LocationPhoneNo = data.LocationPhoneNo,
                    LocationAddress = data.LocationAddress,
                    AdminAgentId = data.AdminAgentId,
                    AdminAgentName = data.AdminAgentName,
                    CarLimit = data.CarLimit,
                    CloseTime = data.CloseTime,
                    OpenTime = data.OpenTime,
                    TownshipId = data.TownshipId,
                    TownshipName = data.TownshipName,
                    IsActive = data.IsActive,
                    No = ++index
                }).FirstOrDefault();
            }
            return model;
        }

        public BranchViewModel SaveBranch(BranchViewModel model)
        {
            BranchViewModel response = new BranchViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbBranch obj = new tbBranch();
                    obj.LocationName = model.LocationName;
                    obj.LocationPhoneNo = model.LocationPhoneNo;
                    obj.LocationAddress = model.LocationAddress;
                    obj.OpenTime = model.OpenTime;
                    obj.CarLimit = model.CarLimit;
                    obj.CloseTime = model.CloseTime;
                    obj.TownshipId = model.TownshipId;
                    obj.AdminAgentId = model.AdminAgentId;
                    obj.IsActive = model.IsActive;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    obj.CreateUserId = model.CreateUserId;
                    context.tbBranches.Add(obj);
                    context.SaveChanges();
                }
                response.MessageType = 1;
                response.Message = "Save Successful.";
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Save Failed.";
            }

            return response;
        }

        public bool CheckExistBranch(BranchViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbBranches.Where(x => x.LocationName == model.LocationName 
                && x.LocationPhoneNo == model.LocationPhoneNo && x.LocationAddress == model.LocationAddress
                && x.AdminAgentId == model.AdminAgentId && x.TownshipId == model.TownshipId && 
                x.OpenTime == model.OpenTime && x.CloseTime==model.CloseTime && x.CarLimit == model.CarLimit
                && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public BranchViewModel EditBranch(BranchViewModel model)
        {
            BranchViewModel response = new BranchViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    context.tbBranches.First(x => x.Id == model.Id).LocationName = model.LocationName;
                    context.tbBranches.First(x => x.Id == model.Id).LocationPhoneNo = model.LocationPhoneNo;
                    context.tbBranches.First(x => x.Id == model.Id).LocationAddress = model.LocationAddress;
                    context.tbBranches.First(x => x.Id == model.Id).CarLimit = model.CarLimit;
                    context.tbBranches.First(x => x.Id == model.Id).CloseTime = model.CloseTime;
                    context.tbBranches.First(x => x.Id == model.Id).OpenTime = model.OpenTime;
                    context.tbBranches.First(x => x.Id == model.Id).TownshipId = model.TownshipId;
                    context.tbBranches.First(x => x.Id == model.Id).AdminAgentId = model.AdminAgentId;
                    context.tbBranches.First(x => x.Id == model.Id).IsActive = model.IsActive;
                    context.tbBranches.First(x => x.Id == model.Id).UpdateUserId = model.UpdateUserId;
                    context.SaveChanges();

                    response.MessageType = 1;
                    response.Message = "Update Successful.";
                }
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Update failed.";
            }
            return response;
        }
        public bool CheckExistUpdateBranch(BranchViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbBranches.Where(x => x.LocationName == model.LocationName
                && x.LocationPhoneNo == model.LocationPhoneNo && x.LocationAddress == model.LocationAddress
                && x.AdminAgentId == model.AdminAgentId && x.TownshipId == model.TownshipId &&
                x.OpenTime == model.OpenTime && x.CloseTime == model.CloseTime && x.CarLimit == model.CarLimit
                && x.IsActive == model.IsActive && x.Id == model.Id && x.IsDeleted == false).ToList();
                if (query.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public BranchViewModel DeleteBranch(int id)
        {
            BranchViewModel model = new BranchViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbBranches.First(x => x.Id == id).IsDeleted = true;
                context.SaveChanges();

                model.MessageType = 1;
                model.Message = "Delete Successful.";
            }

            return model;
        }

        #endregion

        #region tbAdmin

        #endregion
    }
}