using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIAnallyzer_v2.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using APIAnallyzer_v2.Services; // Adicione o namespace do CampaignService e ValidationService

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "APIAnallyzer_v2", Version = "v1" });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Registro do MongoDbService
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddHttpClient<ValidationService>();

// Registro do ValidationService
builder.Services.AddScoped<ValidationService>(); 

// Registro do CampaignService
builder.Services.AddScoped<CampaignService>();

// Configuração dos Health Checks
builder.Services.AddHealthChecks()
    .AddMongoDb(
        builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException(),
        name: "MongoDB",
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "db" })
    .AddCheck("Server", () =>
    {
        return HealthCheckResult.Healthy("Servidor está operacional.");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception != null ? e.Value.Exception.Message : null,
            })
        });
        await context.Response.WriteAsync(result);
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
