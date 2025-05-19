using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Helpers
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    double maxFileSizeInMb = _maxFileSize / (1024.0 * 1024.0);
                    return new ValidationResult(
                        ErrorMessage ?? $"El tamaño máximo del archivo es {maxFileSizeInMb:F2} MB."
                    );
                }
            }

            return ValidationResult.Success;
        }        
    }
}