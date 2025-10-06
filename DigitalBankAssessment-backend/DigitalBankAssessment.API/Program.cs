using System.Reflection;
using DigitalBankAssessment.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080", "http://localhost:5000");


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var conString = builder.Configuration.GetConnectionString("DefaultConnection") ??
     throw new InvalidOperationException("Connection string 'DefaultConnection'" +
    " not found.");

Console.WriteLine("ConnectionString: " + conString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//MediatR
var assemblies = Assembly.Load("DigitalBankAssessment.UseCases");

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(assemblies);
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

//CORS II
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
