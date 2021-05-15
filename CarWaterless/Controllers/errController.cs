using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWaterless.Controllers
{
    public class errController : Controller
    {
        // GET: err
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Err()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}