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
    public class AdminRepository
    {
        public AdminViewModel Authenticate(string username, string password)
        {
            AdminViewModel model = new AdminViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    var query = (from data in context.tbAdmins
                                 where data.UserName == username && data.Password == password
                                 select new AdminViewModel
                                 {
                                     Id = data.Id,
                                     UserName = data.UserName,
                                     FullName = data.FullName,
                                     UserRole = data.UserRole,
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        model = query.AsEnumerable().Select((data, index) => new AdminViewModel()
                        {
                            Id = data.Id,
                            UserName = data.UserName,
                            FullName = data.FullName,
                            UserRole = data.UserRole,
                        }).FirstOrDefault();
                        model.MessageType = 1;
                    }
                    else
                    {
                        model.MessageType = 2;
                        model.Message = "User name or Password is incorrect!";
                    }
                }
            }
            catch (Exception e)
            {
                model.MessageType = 2;
                model.Message = e.Message;
            }
            return model;
        }

        public int CheckUserNameValid(string username)
        {
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbAdmins.Where(x => x.IsActive == true && x.UserName == username).ToList();
                return query.Count;
            }
        }

        public AdminViewModel Save(AdminViewModel model)
        {
            AdminViewModel response = new AdminViewModel();
            try
            {
                using (var context = new CarWaterLessContext())
                {
                    string userid = Guid.NewGuid().ToString();
                    tbAdmin obj = new tbAdmin();
                    obj.UserName = model.UserName;
                    obj.FullName = model.FullName;
                    obj.Password = model.Password;
                    obj.UserRole = model.UserRole;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                    context.tbAdmins.Add(obj);
                    context.SaveChanges();

                    response.MessageType = 1;
                    response.Message = "Account created successfully";
                }
            }
            catch (Exception e)
            {
                response.MessageType = 2;
                response.Message = "Save Failed!";
            }

            return response;
        }

        public List<AdminViewModel> GetAllAgents()
        {
            List<AdminViewModel> lst = new List<AdminViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdmins
                             where data.IsDeleted == false && data.UserRole =="branchagent"
                             select new AdminViewModel
                             {
                                 Id = data.Id,
                                 UserName = data.UserName,
                                 FullName = data.FullName,
                                 Password = data.Password,
                             });
                lst = query.AsEnumerable().Select((data, index) => new AdminViewModel()
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    FullName = data.FullName,
                    Password = data.Password
                }).ToList();
            }
            return lst;
        }

        public AdminViewModel GetById(int id)
        {
            AdminViewModel model = new AdminViewModel();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdmins
                             where data.IsDeleted == false
                             select new AdminViewModel
                             {
                                 Id = data.Id,
                                 UserName = data.UserName,
                                 FullName = data.FullName,
                                 Password = data.Password,
                             });
                model = query.AsEnumerable().Select((data, index) => new AdminViewModel()
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    FullName = data.FullName,
                    Password = data.Password
                }).FirstOrDefault();
            }
            return model;
        }

        #region Profile
        public AdminViewModel EditProfile(AdminViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var currentname = context.tbAdmins.Where(x => x.UserName == model.UserName).ToList();
                if (currentname.Count > 0)
                {
                    var thisname = currentname.Where(x => x.Id == model.Id).ToList();
                    if (thisname.Count > 0)
                    {
                        var currentpassword = context.tbAdmins.Where(x => x.Id == model.Id).FirstOrDefault().Password;
                        if (currentpassword != model.Password)
                        {
                            model = new AdminViewModel();
                            model.Message = "Your current password is incorrect!";
                            model.MessageType = 3;
                        }
                        else
                        {
                            context.tbAdmins.First(x => x.Id == model.Id).UserName = model.UserName;
                            context.tbAdmins.First(x => x.Id == model.Id).FullName = model.FullName;
                            context.SaveChanges();

                            model = new AdminViewModel();
                            model.Message = "Save Successful.";
                            model.MessageType = 1;
                        }
                    }
                    else
                    {
                        model = new AdminViewModel();
                        model.Message = "User name already exists.";
                        model.MessageType = 3;
                    }

                }
                else
                {
                    var currentpassword = context.tbAdmins.Where(x => x.Id == model.Id).FirstOrDefault().Password;
                    if (currentpassword != model.Password)
                    {
                        model = new AdminViewModel();
                        model.Message = "Your current password is incorrect!";
                        model.MessageType = 3;
                    }
                    else
                    {
                        context.tbAdmins.First(x => x.Id == model.Id).UserName = model.UserName;
                        context.tbAdmins.First(x => x.Id == model.Id).FullName = model.FullName;
                        context.SaveChanges();

                        model = new AdminViewModel();
                        model.Message = "Save Successful.";
                        model.MessageType = 1;
                    }

                }
            }
            return model;
        }

        public AdminViewModel ChangePassword(AdminViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                var currentpassword = context.tbAdmins.Where(x => x.Id == model.Id).FirstOrDefault().Password;
                if (currentpassword != model.CurrentPassword)
                {
                    model = new AdminViewModel();
                    model.Message = "Your current password is incorrect!";
                    model.MessageType = 3;
                }
                else
                {
                    context.tbAdmins.First(x => x.Id == model.Id).Password = model.Password;
                    context.SaveChanges();

                    model = new AdminViewModel();
                    model.Message = "Password changed successfully.";
                    model.MessageType = 1;
                }
            }
            return model;
        }
        #endregion

        public List<AdminViewModel> GetAllUser(AdminViewModel model)
        {
            List<AdminViewModel> lst = new List<AdminViewModel>();
            using (var context = new CarWaterLessContext())
            {
                var query = (from data in context.tbAdmins
                             where data.IsDeleted == false && data.Id!=3
                             select new AdminViewModel
                             {
                                 Id = data.Id,
                                 FullName = data.FullName,
                                 UserName = data.UserName,
                                 UserRole = data.UserRole,
                                 IsActive = data.IsActive,
                             });
                lst = query.AsEnumerable().Select((data, index) => new AdminViewModel()
                {
                    Id = data.Id,
                    FullName = data.FullName,
                    UserName = data.UserName,
                    UserRole = data.UserRole,
                    IsActive = data.IsActive,
                    UserRoleName = CommonRepository.GetUserRoleName(data.UserRole),
                    No = ++index
                }).ToList();
            }
            return lst;
        }

        public AdminViewModel De_activateUser(int id, bool flag)
        {
            AdminViewModel model = new AdminViewModel();
            using (var context = new CarWaterLessContext())
            {

                context.tbAdmins.First(x => x.Id == id).IsActive = flag;
                context.SaveChanges();


                model.MessageType = 1;
                model.Message = "";
            }

            return model;
        }

        public AdminViewModel AddUser(AdminViewModel model)
        {
            using (var context = new CarWaterLessContext())
            {
                tbAdmin obj = new tbAdmin();
                obj.UserName = model.UserName;
                obj.FullName = model.FullName;
                obj.Password = model.Password;
                obj.UserRole = model.UserRole;
                obj.CreateDate = MyExtension.getLocalTime(DateTime.UtcNow).Date;
                obj.CreateUserId = model.CreateUserId;
                obj.IsActive = model.IsActive;
                obj.IsDeleted = false;
                context.tbAdmins.Add(obj);
                context.SaveChanges();
            }
            model = new AdminViewModel();
            model.Message = "New user added";
            model.MessageType = 1;

            return model;
        }

    }
}