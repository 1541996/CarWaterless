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

    }
}