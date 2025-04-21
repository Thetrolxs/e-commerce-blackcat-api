using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Models;

namespace e_commerce_blackcat_api.Src.Models
{
    //Representa los productos y el detalle la orden realizada 
    public class OrderDetail
    {
        public int Id {get; set;}
        
        public int OrderId {get; set;} 
        public Order Order {get; set;} = null!;

        public int ProductId {get; set;}
        public Product Product {get; set;} = null!;

        public int Quantity {get; set;}

        public int UnitPrice {get; set;}
    }
}