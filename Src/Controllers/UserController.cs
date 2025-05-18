using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Dtos.User;
using e_commerce_blackcat_api.Src.Services.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_blackcat_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Listado paginado de usuarios (20 por página)
        /// Solo accesible por Administradores
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> GetPagedUsers([FromQuery] int page = 1)
        {
            var result = await _userService.GetPagedUserAsync(page);
            return Ok(result);
        }

        /// <summary>
        /// Filtros avanzados para búsqueda de usuarios
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpPost("filter")]
        public async Task<IActionResult> GetFilteredUsers([FromBody] UserFilterDto filters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userService.GetFilteredUsersAsync(filters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Búsqueda rápida por texto (nombre completo, email)
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string query)
        {
            try
            {
                var result = await _userService.SearchUser(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ver el propio perfil (usuario autenticado)
        /// </summary>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var result = await _userService.GetUser(User);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Editar el propio perfil
        /// </summary>
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditUserDto editUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var success = await _userService.EditUser(User, editUserDto);
                if (!success)
                {
                    return BadRequest("No se pudo actualizar el usuario.");
                }
                return Ok("Usuario actualizado con exito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cambiar contraseña del usuario autenticado
        /// </summary>
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult<string>> ChangeUserPassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var success = await _userService.ChangeUserPassword(User, changePasswordDto);
                if (!success)
                {
                    return BadRequest("No se pudo cambiar la contraseña.");
                }
                return Ok("Contraseña cambiada con exito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cambiar estado (activo/inactivo) de un usuario (permisos de admin)
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/state")]
        public async Task<ActionResult> ChangeUserState(string id, [FromBody] ChangeUserStateDto changeUserState)
        {
            try
            {
                var result = await _userService.ChangeUserState(id, changeUserState.IsActive, changeUserState.Reason ?? string.Empty);
                if (!result)
                {
                    return BadRequest("No se pudo actualizar el estado del usuario");
                }

                return Ok("Estado actualizado correctamente");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ver perfil completo de un usuario por ID (permisos de admin)
        /// </summary>
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            try
            {
                var result = await _userService.GetUserDetailAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}