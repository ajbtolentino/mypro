using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPro.App.Core.Authentication;
using MyPro.App.Core.Services;
using MyPro.App.Infrastructure.Authentication;
using MyPro.App.Infrastructure.Services;

namespace MyPro.App.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static AuthenticationBuilder AddMyProAuthentication(this IServiceCollection services, IConfiguration configuration, string name)
    {
        return services.AddAuthentication(name)
                        .AddJwtBearer(name, options =>
                        {
                            options.Authority = configuration.GetValue<string>("AuthorityUrl");
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateAudience = false
                            };
                        });
    }

    public static IServiceCollection AddMicroserviceSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyPro API", Version = "v1" });
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Scheme = "Bearer",
                        Type = SecuritySchemeType.OAuth2,
                        In = ParameterLocation.Header,
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{configuration.GetValue<string>("AuthorityUrl")}/connect/authorize"),
                                TokenUrl = new Uri($"{configuration.GetValue<string>("AuthorityUrl")}/connect/token")
                            }
                        },
                    }); ;
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="oauth2"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new string[]{}
                        }
                    });
                });
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddServices(configuration).AddAuthentication(configuration);
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}