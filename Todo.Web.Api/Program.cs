using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Todo.Services.Concrete.Api;
using Todo.Web.Api.Configurations;
using Todo.Web.Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddConsole();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();
builder.Services.AddApiServices(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddJwtAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContext>();
    context.Database.EnsureCreated();
}

app.Run();
