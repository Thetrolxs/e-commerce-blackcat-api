using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos.User
{
    public class UserPageDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public DateTime UserRegister {get; set;}
        // Estado del usuario (True = Activo, False = Desactivado)
        public bool IsActive { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}