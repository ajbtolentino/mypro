using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPro.App.Core.Authentication;
using MyPro.App.Core.Services;
using MyPro.App.Infrastructure.Authentication;
using MyPro.App.Infrastructure.Services;

namespace MyPro.App.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public const string CONFIG_KEY_AUTHORITY_URL = "AuthorityUrl";

    public static void RunMicroservice(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddMyProAuthentication(builder.Configuration, JwtBearerDefaults.AuthenticationScheme);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMicroserviceSwaggerGen(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Configuration.GetValue<string>("ApplicationName")}");
                options.OAuthClientId("private-api");
                options.OAuthScopes("read");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static AuthenticationBuilder AddMyProAuthentication(this IServiceCollection services, IConfiguration configuration, string name)
    {
        return services.AddAuthentication(name)
                        .AddJwtBearer(name, options =>
                        {
                            options.Authority = configuration.GetAuthorityUrl();
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
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = configuration.GetValue<string>("ApplicationName"), Version = "v1" });
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Type = SecuritySchemeType.OpenIdConnect,
                        In = ParameterLocation.Header,
                        OpenIdConnectUrl = configuration.GetWellKnownConfiguration()
                    });
                });
    }

    public static Uri GetWellKnownConfiguration(this IConfiguration configuration)
    {
        return new Uri($"{configuration.GetAuthorityUrl()}/.well-known/openid-configuration");
    }

    public static string GetAuthorityUrl(this IConfiguration configuration)
    {
        return configuration.GetValue<string>(CONFIG_KEY_AUTHORITY_URL) ?? string.Empty;
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