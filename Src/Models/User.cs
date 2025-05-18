using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Models
{
    // Usuario identity con nombre completo
    public class User : IdentityUser
    {
        public required string FullName {get; set;}
        // Fecha en la que se registro el usuario
        public DateTime UserRegister { get; set; } = DateTime.UtcNow;
        // Ultimo acceso a la cuenta del usuario
        public DateTime? LastAccess {get; set;}
        public DateTime Birthday {get; set;}
        // Estado del usuario (True = Activo, False = Desactivado)
        public bool IsActive {get; set;}
        // Posible razon que el usuario fue desactivado.
        public string? DesactivationReason {get; set;}
        //Relaciones
        public ICollection<Order> Orders {get; set;} = null!;
        public ICollection<CartItem> CartItems {get; set;} = null!;
        // Datos del domicilio del usuario
        public ShippingAddres? ShippingAddres { get; set; }
    }
}