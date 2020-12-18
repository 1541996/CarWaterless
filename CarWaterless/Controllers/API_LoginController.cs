using Infra.Models;
using Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarWaterless.Controllers
{
    public class API_LoginController : ApiController
    {
        UnitOfWork uow;
        CarWaterLessContext dbContext;
        public API_LoginController()
        {
            dbContext = new CarWaterLessContext();
            uow = new UnitOfWork(dbContext);
        }

        [HttpPost]
        [Route("api/user/login")]
        public HttpResponseMessage login(HttpRequestMessage request, tbCustomer cus)
        {
            tbCustomer customer = uow.customerRepo.GetAll().Where(a => a.UserName == cus.UserName && a.Password == cus.Password).FirstOrDefault();
            if(customer != null)
            {
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
            }
            else
            {
                return request.CreateResponse<tbCustomer>(HttpStatusCode.OK, customer);
            }
          


        }


    }
}
