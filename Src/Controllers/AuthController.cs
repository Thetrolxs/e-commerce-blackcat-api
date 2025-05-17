using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace e_commerce_blackcat_api.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
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
