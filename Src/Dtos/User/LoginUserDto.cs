using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El Correo electrónico es obligatorio.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set;} = string.Empty;    
    }
}