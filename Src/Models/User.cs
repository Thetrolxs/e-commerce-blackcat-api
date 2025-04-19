using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace e_commerce_blackcat_api.Src.Models
{
    // Usuario identity con nombre completo
    public class User : IdentityUser
    {
        public string FullName {get; set;} = string.Empty;

        //Relaciones
        public ICollection<Order> Orders {get; set;} = null!;
        public ICollection<CartItem> CartItems {get; set;} = null!;
    }
}