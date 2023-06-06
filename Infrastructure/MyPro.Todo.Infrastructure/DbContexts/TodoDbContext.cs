using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;

namespace MyPro.Todo.Infrastructure.DbContexts
{
    internal class TodoDbContext : DbContext, ITodoDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.Todo> Todos => base.Set<Entities.Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

