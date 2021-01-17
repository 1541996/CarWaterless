using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class BookingSuccessModel
    {
        public tbOperation operation { get; set; }
        public tbCarCategory carCategory { get; set; }
        public tbCustomerVehicle vehicle { get; set; }
        public IQueryable<tbPhoto> photos { get; set; }

    }

    public class ServiceViewModel
    {
        public List<tbMemberPackage> big { get; set; }
        public List<tbMemberPackage> small { get; set; }
        public List<tbMemberPackage> medium { get; set; }
    }

}
