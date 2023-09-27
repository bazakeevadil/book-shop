using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerGen(option =>
{
    option.EnableAnnotations();
    option.ExampleFilters();
    option.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Rolls-Royce API Documentation",
        Description = "Документация каталога работников Rolls-Royce API",
        TermsOfService = new Uri("https://rolls-roycemotorcars.com"),
        Contact = new OpenApiContact()
        {
            Name = "Rolls-Royce",
            Url = new Uri("https://rolls-roycemotorcars.com")
        }
    });
});


builder.Services.AddTransient<ErrorHandlingMiddleware>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Rolls-Royce API Documentation";
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
