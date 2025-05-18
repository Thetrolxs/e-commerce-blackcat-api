using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class ChangeUserStateDto
    {
        public bool IsActive { get; set; }
        public string? Reason { get; set; }
    }
}