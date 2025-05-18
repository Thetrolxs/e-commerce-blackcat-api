using e_commerce_blackcat_api.Src.Dtos.User;
using e_commerce_blackcat_api.Src.Services.Interface;

using Microsoft.AspNetCore.Mvc;

namespace e_commerce_blackcat_api.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Constructor del controlador de autenticación.
    /// </summary>
    /// <param name="authService">Servicio de autenticación.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Inicia sesión para un usuario.
    /// </summary>
    /// <param name="loginUserDto">Credenciales del usuario.</param>
    /// <returns>Usuario logueado con el token generado.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoggedUserDto>> Login(LoginUserDto loginUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var response = await _authService.Login(loginUserDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Registra un nuevo usuario.
    /// </summary>
    /// <param name="registerUserDto">Datos del usuario a registrar.</param>
    /// <returns>Usuario registrado con sus credenciales.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<LoggedUserDto>> Register(RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var response = await _authService.RegisterUser(registerUserDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }  

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Auth endpoint is working");
    }

    [HttpGet("error")]
    public IActionResult Error()
    {
        return BadRequest("An error occurred");
    }
}

