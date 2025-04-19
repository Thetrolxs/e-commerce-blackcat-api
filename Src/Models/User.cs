using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Models
{
    // Usuario identity con nombre completo
    public class User : IdentityUser
    {
        public string FullName {get; set;} = string.Empty;

        public ShippingAddres? ShippingAddres { get; set; }

        //Relaciones
        public ICollection<Order> Orders {get; set;} = null!;
        public ICollection<CartItem> CartItems {get; set;} = null!;
    }
}