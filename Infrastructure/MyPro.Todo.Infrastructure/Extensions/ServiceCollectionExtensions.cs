using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Services;
using MyPro.Todo.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Repositories;
using MyPro.Todo.Infrastructure.Services;

namespace MyPro.Todo.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTodoDbContext(configuration)
                    .AddTodoRepositories(configuration)
                    .AddTodoServices(configuration);

            return services;
        }

        public static IServiceCollection AddTodoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //Add other dbconnection string support
            services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase("CleanArchitectureDb"))
                    .AddScoped<ITodoDbContext>(provider => provider.GetRequiredService<DbContexts.TodoDbContext>());

            return services;
        }

        public static IServiceCollection AddTodoRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }

        public static IServiceCollection AddTodoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
    }
}

