﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infra.ViewModels;
using Infra.Models;
using Infra.Helper;
using Data.Helper;
using Infra.helper;

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
                                 TownshipCode = data.TownshipCode,
                             });
                lst = query.AsEnumerable().Select((data, index) => new TownshipViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    TownshipCode = data.TownshipCode,
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
                                 TownshipCode = data.TownshipCode,
                             });
                model = query.AsEnumerable().Select((data, index) => new TownshipViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    TownshipCode = data.TownshipCode,
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
                    obj.TownshipCode = model.TownshipCode;
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
                var query = context.tbTownships.Where(x => x.Name == model.Name && x.TownshipCode == model.TownshipCode && x.IsDeleted == false).ToList();
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
                    context.tbTownships.First(x => x.Id == model.Id).TownshipCode = model.TownshipCode;
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
                var query = context.tbTownships.Where(x => x.Name == model.Name && x.TownshipCode == model.TownshipCode && x.Id == model.Id && x.IsDeleted == false).ToList();
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
                                 IsDailyHot = data.IsDailyHot,
                                 IsActive = data.IsActive,
                             });
                lst = query.AsEnumerable().Select((data, index) => new AdditionalServiceViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    CarType = data.CarType,
                    Price = data.Price,
                    IsDailyHot = data.IsDailyHot??false,
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
                                 IsDailyHot = data.IsDailyHot,
                                 IsActive = data.IsActive,
                             });
                model = query.AsEnumerable().Select((data, index) => new AdditionalServiceViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    CarType = data.CarType,
                    Price = data.Price,
                    DiscountPrice = data.DiscountPrice,
                    IsDailyHot = data.IsDailyHot??false,
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
                    obj.IsDailyHot = false;
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

        public AdditionalServiceViewModel ActivateDeactivateAdditionalService(int id, bool currentflag)
        {
            AdditionalServiceViewModel model = new AdditionalServiceViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbAdditionalServices.First(x => x.Id == id).IsDailyHot = !currentflag;
                context.SaveChanges();

                if (currentflag == true)
                {
                    model.MessageType = 1;
                    model.Message = "Deactivate for Daily Hot Successful.";
                }
                else
                {
                    model.MessageType = 1;
                    model.Message = "Activate for Daily Hot Successful.";
                }

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
                             where data.IsDeleted == false && data.IsActive == model.IsActive
                             orderby data.CreateDate ascending
                             select new BranchViewModel
                             {
                                 Id = data.Id,
                                 LocationName = data.LocationName,
                                 LocationPhoneNo = data.LocationPhoneNo,
                                 LocationAddress = data.LocationAddress,
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
                lst = query.AsEnumerable().Select((data, index) => new BranchViewModel()
                {
                    Id = data.Id,
                    LocationName = data.LocationName,
                    LocationPhoneNo = data.LocationPhoneNo,
                    LocationAddress = data.LocationAddress,
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
                             where data.IsDeleted == false && data.Id == id
                             select new BranchViewModel
                             {
                                 Id = data.Id,
                                 LocationName = data.LocationName,
                                 LocationPhoneNo = data.LocationPhoneNo,
                                 LocationAddress = data.LocationAddress,
                                 CarLimit = data.CarLimit,
                                 CloseTime = data.CloseTime,
                                 OpenTime = data.OpenTime,
                                 TownshipId = data.TownshipId,
                                 TownshipName = town.Name,
                                 IsActive = data.IsActive,
                                 Photo = data.Photo,
                                 MapHtml = data.MapHtml
                             });
                model = query.AsEnumerable().Select((data, index) => new BranchViewModel()
                {
                    Id = data.Id,
                    LocationName = data.LocationName,
                    LocationPhoneNo = data.LocationPhoneNo,
                    LocationAddress = data.LocationAddress,
                    CarLimit = data.CarLimit,
                    CloseTime = data.CloseTime,
                    OpenTime = data.OpenTime,
                    TownshipId = data.TownshipId,
                    TownshipName = data.TownshipName,
                    IsActive = data.IsActive,
                    Photo = data.Photo,
                    MapHtml = data.MapHtml,
                    No = ++index
                }).FirstOrDefault();
            }
            return model;
        }

        public async System.Threading.Tasks.Task<BranchViewModel> SaveBranchAsync(BranchViewModel model)
        {
            BranchViewModel response = new BranchViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbBranch obj = new tbBranch();
                    if (model.Photo != null)
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = model.Photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/Branch";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                        obj.Photo = responsefile;
                    }

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
                    obj.MapHtml = model.MapHtml;
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
        public async System.Threading.Tasks.Task<BranchViewModel> EditBranchAsync(BranchViewModel model)
        {
            BranchViewModel response = new BranchViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    if (model.Id > 0)
                    {
                        if (model.Photo != null)
                        {
                            FileUploadViewModel fileupload = new FileUploadViewModel();
                            fileupload.photo = model.Photo;
                            fileupload.filepath = "/ImageStorage/CarWaterlessProject/Branch";
                            var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                            model.Photo = responsefile;
                        }
                        else
                        {
                            var olddata = context.tbBranches.Where(a => a.IsDeleted != true && a.Id == model.Id).FirstOrDefault();
                            model.Photo = olddata.Photo;
                        }

                       
                    }                  
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
                    context.tbBranches.First(x => x.Id == model.Id).MapHtml = model.MapHtml;
                    context.tbBranches.First(x => x.Id == model.Id).Photo = model.Photo;
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

        #region tbMemberPackage
        public List<MemberPackageViewModel> GetAllMemberPackage()
        {
            List<MemberPackageViewModel> lst = new List<MemberPackageViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbMemberPackages
                             where data.IsDeleted == false
                             select new MemberPackageViewModel
                             {
                                 ID = data.ID,
                                 Title = data.Title,
                                 AdditionalServiceIds = data.AdditionalServiceIds,
                                 AdditionalServiceNames = data.AdditionalServiceNames,
                                 PackagePrice = data.PackagePrice,
                                 CarType = data.CarType,
                                 Photo = data.Photo
                             });
                lst = query.AsEnumerable().Select((data, index) => new MemberPackageViewModel()
                {
                    ID = data.ID,
                    Title = data.Title,
                    AdditionalServiceIds = data.AdditionalServiceIds,
                    AdditionalServiceNames = data.AdditionalServiceNames,
                    PackagePrice = data.PackagePrice,
                    CarType = data.CarType,
                    IsActive = data.IsActive,
                    Photo = data.Photo,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public MemberPackageViewModel GetMemberPackagebyId(int id)
        {
            MemberPackageViewModel model = new MemberPackageViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbMemberPackages
                             where data.IsDeleted == false && data.ID == id
                             select new MemberPackageViewModel
                             {
                                 ID = data.ID,
                                 Title = data.Title,
                                 AdditionalServiceIds = data.AdditionalServiceIds,
                                 AdditionalServiceNames = data.AdditionalServiceNames,
                                 PackagePrice = data.PackagePrice,
                                 CarType = data.CarType,
                                 IsActive = data.IsActive,
                                 Photo = data.Photo,
                             });
                model = query.AsEnumerable().Select((data, index) => new MemberPackageViewModel()
                {
                    ID = data.ID,
                    Title = data.Title,
                    AdditionalServiceIds = data.AdditionalServiceIds,
                    AdditionalServiceNames = data.AdditionalServiceNames,
                    PackagePrice = data.PackagePrice,
                    CarType = data.CarType,
                    IsActive = data.IsActive,
                    Photo = data.Photo,
                }).FirstOrDefault();
            }
            return model;
        }

        public async System.Threading.Tasks.Task<MemberPackageViewModel> SaveMemberPackageAsync(MemberPackageViewModel model)
        {
            MemberPackageViewModel response = new MemberPackageViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    tbMemberPackage obj = new tbMemberPackage();

                    if (model.Photo != null)
                    {
                        FileUploadViewModel fileupload = new FileUploadViewModel();
                        fileupload.photo = model.Photo;
                        fileupload.filepath = "/ImageStorage/CarWaterlessProject/MemberPackage";
                        var responsefile = await FileUploadApiRequestHelper.upload(fileupload);

                        obj.Photo = responsefile;
                    }


                    string ids = model.AdditionalServiceIds;
                    ids = ids.Remove(ids.Length - 1, 1);
                    string names = model.AdditionalServiceNames;
                    names = names.Remove(names.Length - 1, 1);
                  
                    obj.Title = model.Title;
                    obj.CarType = model.CarType;
                    obj.PackagePrice = model.PackagePrice;
                    obj.AdditionalServiceIds = ids;
                    obj.AdditionalServiceNames = names;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    context.tbMemberPackages.Add(obj);
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

        public async System.Threading.Tasks.Task<MemberPackageViewModel> EditMemberPackageAsync(MemberPackageViewModel model)
        {
            MemberPackageViewModel response = new MemberPackageViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    if (model.ID > 0)
                    {
                        if (model.Photo != null)
                        {
                            FileUploadViewModel fileupload = new FileUploadViewModel();
                            fileupload.photo = model.Photo;
                            fileupload.filepath = "/ImageStorage/CarWaterlessProject/MemberPackage";
                            var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                            model.Photo = responsefile;
                        }
                        else
                        {
                            var olddata = context.tbMemberPackages.Where(a => a.IsDeleted != true && a.ID == model.ID).FirstOrDefault();
                            model.Photo = olddata.Photo;
                        }


                    }

                    string ids = model.AdditionalServiceIds;
                    ids = ids.Remove(ids.Length - 1, 1);
                    string names = model.AdditionalServiceNames;
                    names = names.Remove(names.Length - 1, 1);

                    context.tbMemberPackages.First(x => x.ID == model.ID).Title = model.Title;
                    context.tbMemberPackages.First(x => x.ID == model.ID).AdditionalServiceIds = ids;
                    context.tbMemberPackages.First(x => x.ID == model.ID).AdditionalServiceNames = names;
                    context.tbMemberPackages.First(x => x.ID == model.ID).CarType = model.CarType;
                    context.tbMemberPackages.First(x => x.ID == model.ID).PackagePrice = model.PackagePrice;
                    context.tbMemberPackages.First(x => x.ID == model.ID).Photo = model.Photo;
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
        public MemberPackageViewModel DeleteMemberPackage(int id)
        {
            MemberPackageViewModel model = new MemberPackageViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbMemberPackages.First(x => x.ID == id).IsDeleted = true;
                context.SaveChanges();


                model.MessageType = 1;
                model.Message = "";
            }

            return model;
        }

        public List<tbAdditionalService> BindServiceByCarType(string cartype)
        {
            List<tbAdditionalService> lst = new List<tbAdditionalService>();
            using(var context = new CarWaterLessContext())
            {
                lst = context.tbAdditionalServices.Where(x => x.CarType == cartype && x.IsDeleted == false).ToList();
            }
            return lst;
        }
        #endregion

        #region tbDailyHot
        public List<DailyHotViewModel> GetAllDailyHot(DailyHotViewModel model)
        {
            List<DailyHotViewModel> lst = new List<DailyHotViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var isactive = false;
                var asquery = context.tbAdditionalServices.Where(x => x.IsDailyHot == true).ToList();
                if (asquery.Count > 0)
                {
                    isactive = true;
                }
                var query = (from data in context.tbDailyHots
                             where data.IsDeleted == false
                             orderby data.CreatedDate ascending
                             select new DailyHotViewModel
                             {
                                ID=data.ID,
                                Title = data.Title,
                             });
                lst = query.AsEnumerable().Select((data, index) => new DailyHotViewModel()
                {
                    ID = data.ID,
                    Title = data.Title,
                    IsActive = isactive,
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public DailyHotViewModel GetDailyHotbyId(int id)
        {
            DailyHotViewModel model = new DailyHotViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbDailyHots
                             where data.IsDeleted == false && data.ID == id
                             select new DailyHotViewModel
                             {
                                 ID = data.ID,
                                 Title = data.Title,
                                 IsActive = data.IsActive,
                                 Photo = data.Photo,
                             });
                model = query.AsEnumerable().Select((data, index) => new DailyHotViewModel()
                {
                    ID = data.ID,
                    Title = data.Title,
                    IsActive = data.IsActive,
                    Photo = data.Photo,
                    No = ++index
                }).FirstOrDefault();
            }
            return model;
        }
        public async System.Threading.Tasks.Task<DailyHotViewModel> EditDailyHotAsync(DailyHotViewModel model)
        {
            DailyHotViewModel response = new DailyHotViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    if (model.ID > 0)
                    {
                        if (model.Photo != null)
                        {
                            FileUploadViewModel fileupload = new FileUploadViewModel();
                            fileupload.photo = model.Photo;
                            fileupload.filepath = "/ImageStorage/CarWaterlessProject/DailyHot";
                            var responsefile = await FileUploadApiRequestHelper.upload(fileupload);
                            model.Photo = responsefile;
                        }
                        else
                        {
                            var olddata = context.tbDailyHots.Where(a => a.IsDeleted != true && a.ID == model.ID).FirstOrDefault();
                            model.Photo = olddata.Photo;
                        }


                    }
                    context.tbDailyHots.First(x => x.ID == model.ID).Title = model.Title;
                    context.tbDailyHots.First(x => x.ID == model.ID).Photo = model.Photo;
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
        public DailyHotViewModel ActivateDeactivateDailyHot(int id,bool currentflag)
        {
            DailyHotViewModel model = new DailyHotViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbDailyHots.First(x => x.ID == id).IsActive = !currentflag;
               
                

                if (currentflag == true)
                {
                    context.tbAdditionalServices.ToList().ForEach(x => x.IsDailyHot = false);
                    model.MessageType = 1;
                    model.Message = "Deactivate Successful.";
                }
                else
                {
                    model.MessageType = 1;
                    model.Message = "Activate Successful.";
                }
                context.SaveChanges();
            }

            return model;
        }

        #endregion
    }
}