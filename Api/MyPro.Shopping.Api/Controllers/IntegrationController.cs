using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyPro.Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IntegrationController : ControllerBase
{
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

