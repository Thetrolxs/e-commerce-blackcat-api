using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "La Contraseña antigua es obligatoria.")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Contraseña nueva es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[!@#$%^&*()_+=\[\]{};':"",./<>?\\|])[a-zA-Z0-9!@#$%^&*()_+=\[\]{};':"",./<>?\\|]+$", ErrorMessage = "La Contraseña debe contener al menos una letra, un número y un carácter especial.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public string NewPassword { get; set; } = string.Empty;

        [Compare("NewPassword", ErrorMessage = "Las Contraseñas no coinciden.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}