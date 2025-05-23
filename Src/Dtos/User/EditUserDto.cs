using System.ComponentModel.DataAnnotations;

using e_commerce_blackcat_api.Src.Helpers;

namespace e_commerce_blackcat_api.Src.Dtos
{
    public class EditUserDto
    {
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(8, ErrorMessage = "El nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener a lo más 255 caracteres.")]
        public string? FullName { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de teléfono debe tener exactamente 9 dígitos.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Debe ser el formato de un correo electronico ejemplo@xxxx.xx")]
        public string? Email {get; set;}

        [DataType(DataType.Date)]
        [DateValidation]
        public DateTime? Birthday {get; set;}
        
    }
}