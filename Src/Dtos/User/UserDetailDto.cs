using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class UserDetailDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public DateTime UserRegister { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
        public string? DesactivationReason { get; set; }
        public ShippingAddres? ShippingAddress { get; set; }
    }
}