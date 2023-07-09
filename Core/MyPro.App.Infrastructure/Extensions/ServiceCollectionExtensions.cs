using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPro.App.Core.DbContexts;
using MyPro.App.Core.Services;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.App.Infrastructure.Services;

namespace MyPro.App.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public const string CONFIG_KEY_AUTHORITY_URL = "AuthorityUrl";

    public static WebApplication BuildMicroservice(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllers();

        // Add authentication
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Configuration.GetApplicationName()}");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : ApplicationDbContext
    {
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddSqlite<TDbContext>(connectionString);
        services.AddDbContext<TDbContext>(options =>
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            options.UseSqlite(connection);
        });
        return services;
    }

    public static void Migrate<TDbContext>(this WebApplication app)
        where TDbContext : ApplicationDbContext
    {
        // Migrate latest database changes during startup
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<TDbContext>();

            // Here is the migration executed
            dbContext.Database.Migrate();
        }
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

    public static string GetApplicationName(this IConfiguration configuration)
    {
        return configuration.GetValue<string>("ApplicationName") ?? string.Empty;
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
        return services.AddServices(configuration);
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}