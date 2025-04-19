using System.ComponentModel.DataAnnotations;

namespace e_commerce_blackcat_api.Src.Dtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(150)]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El número telefónico es obligatorio.")]
    [Phone]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]|\\:;""<>,.?/~`]).+$", 
        ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public required string ConfirmPassword { get; set; }
}
