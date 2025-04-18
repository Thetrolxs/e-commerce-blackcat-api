using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Models
{
    // Uso de roles en el usuario en base a un ID
    public class Role : IdentityRole<int>
    {
        
    }
}