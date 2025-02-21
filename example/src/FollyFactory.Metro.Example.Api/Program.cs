using System.Reflection;
using FollyFactory.Metro.Example.Application.Features.Catalog.Commands;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Assembly applicationAssembly = typeof(AddProductToCatalog).Assembly;

builder.Services.AddMetro(config =>
{
    config.AssembliesToScan = [applicationAssembly];
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Metro Shop", Version = "v1" });
    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return [api.GroupName];

        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            return [controllerActionDescriptor.ControllerName];

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.DocInclusionPredicate((_, _) => true);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Metro v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
