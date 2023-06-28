using Microsoft.AspNetCore.Mvc;
using MyPro.App.Core.Authentication;
using MyPro.Identity.Api.Infrastructure.Contracts.Services;

namespace MyPro.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IUserService userService,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthenticationController> logger)
    {
        this.userService = userService;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this._logger = logger;
    }

    [HttpGet(nameof(GetAll))]
    public IEnumerable<Infrastructure.Entities.User> GetAll()
    {
        return this.userService.GetAll();
    }

    [HttpGet(nameof(Token))]
    public string Token(string clientId, string clientSecret, string scope)
    {
        return jwtTokenGenerator.GenerateToken<int>(1, "test", "test");
    }
}

