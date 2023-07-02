using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPro.Gateway.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IntegrationController : ControllerBase
{
    private readonly ILogger<IntegrationController> _logger;
    private readonly IConfiguration _configuration;

    public IntegrationController(ILogger<IntegrationController> logger, IConfiguration configration)
    {
        _logger = logger;
        _configuration = configration;
    }

    [HttpGet(nameof(TestConnection))]
    public async Task<IActionResult> TestConnection()
    {
        // discover endpoints from metadata
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync($"{_configuration.GetValue<string>("AuthUrl")}/.well-known/openid-configuration");

        if (disco.IsError)
        {
            return new JsonResult(disco.Error);
        }

        // request token
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "api",
            ClientSecret = "secret",
            Scope = "api"
        });

        if (tokenResponse.IsError)
        {
            return new JsonResult(tokenResponse.Error);
        }

        // call api
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var shoppingApiUrl = _configuration.GetValue<string>("ShoppingApiUrl");
        var response = await apiClient.GetAsync($"{shoppingApiUrl}/integration/testconnection");

        if (!response.IsSuccessStatusCode)
        {
            return new JsonResult(response.RequestMessage);
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            return new JsonResult(content);
        }

    }
}

