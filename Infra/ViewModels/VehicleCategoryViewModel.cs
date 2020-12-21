using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class VehicleCategoryViewModel
    {
        public tbCustomerVehicle vehicle { get; set; }
        public tbCarCategory category { get; set; }
    }


    public class BranchViewModel
    {
        public tbBranch branch { get; set; }
        public tbTownship township { get; set; }
    }
}
