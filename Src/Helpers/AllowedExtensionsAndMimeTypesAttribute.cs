using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Helpers
{
    public class AllowedExtensionsAndMimeTypesAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly string[] _mimeTypes;

        public AllowedExtensionsAndMimeTypesAttribute(string[] extensions, string[] mimeTypes)
        {
            _extensions = extensions;
            _mimeTypes = mimeTypes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                // Validar la extensión
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"La extensión del archivo no es válida. Solo se permiten: {string.Join(", ", _extensions)}");
                }

                // Validar el tipo MIME
                if (!_mimeTypes.Contains(file.ContentType))
                {
                    return new ValidationResult($"El tipo MIME del archivo no es válido. Solo se permiten: {string.Join(", ", _mimeTypes)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}