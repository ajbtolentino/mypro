using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyPro.Gateway.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IntegrationController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<IntegrationController> _logger;

    public IntegrationController(ILogger<IntegrationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(nameof(TestConnection))]
    public IActionResult TestConnection()
    {
        return new JsonResult("success");
    }
}

