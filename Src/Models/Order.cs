using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Models
{
    // Representa una orden de compra realizada por el usuario
    public class Order
    {
        public int Id {get; set;}
        public string UserId {get; set;} = string.Empty;
        public User User {get; set;} = null!;

        public DateTime CreatedOrder {get; set;} = DateTime.UtcNow;

        public string Status {get; set;} = string.Empty;
        public int TotalAmount {get; set;}

        //Relación
        public ICollection<OrderDetail> OrderDetails {get; set;} = null!;
    }
}