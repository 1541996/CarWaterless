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
        public string photourl { get; set; }
        public string CustomerName { get; set; }

    }

    public class ServiceViewModel
    {
        public List<tbMemberPackage> big { get; set; }
        public List<tbMemberPackage> small { get; set; }
        public List<tbMemberPackage> medium { get; set; }
    }

    public class DailyHotDataViewModel
    {
        public string title { get; set; }
        public string photourl { get; set; }
        public IQueryable<tbAdditionalService> additionalservicelist { get; set; }
    }

}
