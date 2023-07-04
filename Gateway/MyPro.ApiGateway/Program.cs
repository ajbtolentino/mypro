using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyPro.App.Infrastructure.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddMyProAuthentication(builder.Configuration, JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.Run();