using Microsoft.EntityFrameworkCore;
using MyPro.App.Infrastructure.Extensions;
using MyPro.Catalog.Api.DbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase<CatalogDbContext>(builder.Configuration);
builder.Services.AddTransient<ICatalogDbContext, CatalogDbContext>();
var app = builder.BuildMicroservice();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<CatalogDbContext>();

    dbContext.Database.Migrate();

    CatalogDbContextSeeder.Seed(dbContext);
}

app.Run();