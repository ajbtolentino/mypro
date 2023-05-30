using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPro.App.Core.Contracts.DbContexts;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Services;
using MyPro.Todo.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Repositories;
using MyPro.Todo.Infrastructure.Services;

namespace MyPro.Todo.Infrastructure.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddTodoInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoEFDbContext>(options => options.UseInMemoryDatabase("CleanArchitectureDb"));
            services.AddScoped<ITodoDbContext>(provider => provider.GetRequiredService<DbContexts.TodoEFDbContext>());

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();
        }
    }
}

