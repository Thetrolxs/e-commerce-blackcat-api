using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class UserFilterDto
    {
        public bool? IsActive { get; set; }
        public DateTime? FromRegisterDate { get; set; }
        public DateTime? ToRegisterDate { get; set; }
        public string? Email { get; set; }
        public string? NameOrLastName { get; set; }
        public int Page { get; set; } = 1;
    }
}