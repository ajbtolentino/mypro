using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPro.App.Core.Authentication;
using MyPro.App.Core.Services;
using MyPro.App.Infrastructure.Authentication;
using MyPro.App.Infrastructure.Services;

namespace MyPro.App.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
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