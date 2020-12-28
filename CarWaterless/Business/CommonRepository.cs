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
    public static class CommonRepository
    {

        #region ClearLoginData
        public static void ClearLoginData()
        {
            if (HttpContext.Current.Request.Cookies["carwaterlessinfo"] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["carwaterlessinfo"];

                httpCookie.Value = null;
                httpCookie.Expires = MyExtension.getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }
        #endregion

        #region StoreLoginData
        public static void StoreLoginData(AdminViewModel model)
        {

            HttpCookie carwaterlessinfo = new HttpCookie("carwaterlessinfo");
            carwaterlessinfo["userid"] = model.Id.ToString();
            carwaterlessinfo["username"] = model.UserName;
            carwaterlessinfo["fullname"] = model.FullName;
            carwaterlessinfo["userrole"] = model.UserRole.ToString();

            carwaterlessinfo.Expires.Add(new TimeSpan(1));
            HttpContext.Current.Response.Cookies.Add(carwaterlessinfo);
        }
        #endregion

        #region GetLoginData
        public static AdminViewModel GetLoginData()
        {
            AdminViewModel model = new AdminViewModel();
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["carwaterlessinfo"];
            if (reqCookies != null)
            {
                model.Id = int.Parse(reqCookies["userid"].ToString());
                model.UserName = reqCookies["username"].ToString();
                model.FullName = reqCookies["fullname"].ToString();
                model.UserRole = reqCookies["userrole"].ToString();
            }
            return model;
        }
        #endregion

        public static string GenerateOperationCode()
        {
            DateTime today = MyExtension.getLocalTime(DateTime.UtcNow).Date;
            int year = today.Year;
            int month = today.Month;
            int day = today.Day;
            string sdate = year.ToString() + month.ToString() + day.ToString();
            string purchaseno = "PNO-" + sdate + "-";
            using (var context = new CarWaterLessContext())
            {
                var query = context.tbOperations.Where(x => x.OperationDate.Value.Year == year && x.OperationDate.Value.Month == month && x.OperationDate.Value.Day == day).OrderBy(x => x.CreateDate).ToList();

                if (query.Count == 0)
                {
                    return purchaseno + "001";
                }
                else
                {
                    string lastregno = query.Last().OperationCode;
                    int lastserialno = Convert.ToInt32(lastregno.Split('-')[2]);
                    lastserialno++;
                    string regno = lastserialno.ToString("000");
                    return purchaseno + regno;
                }
            }
        }

       
        #region ChangeFormatYearMonthDay
        public static string ChangeFormatYearMonthDay(string date)
        {
            string rtnDate = "";
            try
            {
                string[] str = date.Split('/');
                if (str.Length > 0)
                {
                    rtnDate = (str[2] + "-" + str[1] + "-" + str[0]).ToString();
                }
            }
            catch { }
            return rtnDate;
        }
        #endregion

        public static string GetUserRoleName(string userrole)
        {
            if (userrole == "superadministrator")
            {
                return "Super Administrator";
            }
            else if(userrole== "administrator")
            {
                return "Administrator";
            }
            else if(userrole == "branchagent")
            {
                return "Branch Agent";
            }
            else
            {
                return "";
            }
        }


        //#region CheckExpiredCars
        //public static void CheckExpiredCars()
        //{
        //    using(var context = new CarWaterLessContext())
        //    {
        //        DateTime now = MyExtension.getLocalTime(DateTime.UtcNow);
        //        var query = context.SaleCredits.Where(x => x.CreditFinalDate < now).ToList();
        //        query.ToList().ForEach(c => c.IsOverTime = true);

        //        var queryconsignment = context.SaleConsignments.Where(x => x.ConsignmentFinalDate < now).ToList();
        //        queryconsignment.ToList().ForEach(c => c.IsOverTime = true);

        //        context.SaveChanges();
        //    }
        //}
        //#endregion
    }
}