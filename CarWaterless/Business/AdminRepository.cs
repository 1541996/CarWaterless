using Infra.Models;
using Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarWaterless.Business
{
    public class AdminRepository
    {
        public List<AdminViewModel> GetUsers()
        {
            List<AdminViewModel> lst = new List<AdminViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdmins
                             where data.IsDeleted == false
                             select new AdminViewModel
                             {
                                username = data.UserName
                             });
               
                lst = query.AsEnumerable().Select((data, index) => new AdminViewModel()
                {
                   username = data.username
                }).ToList();
            }
            return lst;

        }

    }
}