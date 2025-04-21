using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Models;

namespace e_commerce_blackcat_api.Src.Models
{
    //Representa el carrito de compras temporal de un usuario
    public class CartItem
    {
        public int Id {get; set;}

        public int ProductId {get; set;}
        public Product Product {get; set;} = null!;

        public string UserId {get; set;} = string.Empty;
        public User User {get; set;} = null!;

        public int Quantity {get; set;}
    }
}