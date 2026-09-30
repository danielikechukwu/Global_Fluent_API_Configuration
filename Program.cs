using EF_FluentAPIGlobalConfiguration.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;     
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, CancellationToken) =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Global Configuration";
        document.Info.Description = "In Entity Framework Core, Global Configurations (also called Model-Wide Configurations) are settings that apply universally to all entities and properties within a particular DbContext. Instead of repeating the same configuration for every entity, EF Core allows defining these configurations once at the model level, ensuring";
        return Task.CompletedTask;
    });
});

var connection_string = builder.Configuration.GetConnectionString("FluentAPIGlobalConfiguration");

builder.Services.AddDbContext<FluentAPIGlobalConfigurationDbContext>(options => options.UseNpgsql(connection_string).UseSnakeCaseNamingConvention());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(opt =>
    {
        opt.WithTitle("Global configuration");
        opt.WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.Run();

