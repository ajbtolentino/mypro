using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MyPro.Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IntegrationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<IntegrationController> _logger;

    public IntegrationController(ILogger<IntegrationController> logger, IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet(nameof(TestConnection))]
    public async Task<IActionResult> TestConnection()
    {
        var client = new HttpClient();
        var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        var disco = await client.GetDiscoveryDocumentAsync($"{_configuration.GetValue<string>("AuthUrl")}/.well-known/openid-configuration");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetUserInfoAsync(new UserInfoRequest
        {
            Address = disco.UserInfoEndpoint,
            Token = accessToken
        });

        return new JsonResult("success");
    }
}

