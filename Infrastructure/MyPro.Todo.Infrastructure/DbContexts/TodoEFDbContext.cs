using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyPro.App.Core.Contracts.DbContexts;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;

namespace MyPro.Todo.Infrastructure.DbContexts
{
    internal class TodoEFDbContext : EFDbContext, ITodoDbContext
    {
        public TodoEFDbContext(DbContextOptions<TodoEFDbContext> options)
            : base(options)
        {
        }

        DbSet<Entities.Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

