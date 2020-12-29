using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string BranchIds { get; set; }
        public string BranchNames { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUserId { get; set; }

        public int? No { get; set; }
        public string Message { get; set; }
        public int? MessageType { get; set; }
        public string CurrentPassword { get; set; }
    }
}
