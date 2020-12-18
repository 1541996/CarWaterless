using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class CarPhotoModel
    {
        public tbCustomerVehicle vehicle { get; set; }
        public IQueryable<tbPhoto> photos { get; set; }
    }
}
