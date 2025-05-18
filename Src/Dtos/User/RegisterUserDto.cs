using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_blackcat_api.Src.Helpers;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(8, ErrorMessage = "El Nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El Nombre debe tener a lo más 255 caracteres.")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [DateValidation]
        public string Birthday { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de teléfono debe tener exactamente 9 dígitos.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La Contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La Contraseña debe tener a lo más 20 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Las Contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;    

        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Commune {get; set;}
        public string? Region { get; set; }
        public string? PostalCode { get; set; }    
    }
}