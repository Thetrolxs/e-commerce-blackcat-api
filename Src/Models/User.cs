using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Models
{
    // Uso de identity con clave primaria (ID)
    public class User : IdentityUser<int>
    {
    }
}