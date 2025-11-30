using MedicalImageProcessor.Application.Extensions;
using MedicalImageProcessor.Infrastructure.Processors;
using MedicalImageProcessor.Infrastructure.Services;
using MedicalImageProcessor.Core.Interfaces;
using MedicalImageProcessor.Application.Services;  // ← Додай using для ImageDetectionService

var builder = WebApplication.CreateBuilder(args);

// Контролери
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Нативний OpenAPI .NET 9
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

// DI для Application (оркестратор)
builder.Services.AddApplicationServices();  // Реєструє ImageDetectionService

// DI для Infrastructure (інтерфейси)
builder.Services.AddScoped<IImageProcessor, OnnxImageProcessor>();
builder.Services.AddScoped<IDetectionService, OnnxDetectionService>();

// ← ФІКС: Явна реєстрація конкретного сервісу (якщо AddApplicationServices не спрацьовує)
builder.Services.AddScoped<ImageDetectionService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi("/openapi/v1.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();