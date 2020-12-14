using CarWaterless.Business;
using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class YeGyiController : Controller
    {
        // GET: YeGyi
        public ActionResult Index()
        {
            AdminRepository repository = new AdminRepository();
            var data = repository.GetUsers();
            return View(data);
        }

      

    }
}