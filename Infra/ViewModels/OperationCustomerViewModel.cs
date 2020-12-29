using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class OperationCustomerViewModel
    {
        public tbOperation operation { get; set; }
        public tbCustomer customer { get; set; }
        public tbCustomerVehicle vehicle { get; set; }
        public tbCarCategory carcategory { get; set; }
        public tbBranch branch { get; set; }
        public tbTownship township { get; set; }
    }
}
