using System.Net.Http.Headers;
using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Net.Http.Headers;
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
            ClientId = "private-api",
            ClientSecret = "secret",
            Scope = "read"
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

    [HttpGet(nameof(GetUser))]
    public async Task<IActionResult> GetUser()
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

        return new JsonResult(response.HttpResponse.Content.ReadAsStringAsync());
    }
}

