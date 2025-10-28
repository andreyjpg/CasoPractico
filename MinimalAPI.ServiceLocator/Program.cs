using MinimalAPI.Architecture;
using MinimalAPI.Architecture.Providers;
using MinimalAPI.ServiceLocator.ServiceFactory;
using MinimalAPI.ServiceLocator.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IServiceFactory, ServiceFactory>();
builder.Services.AddScoped<TicketsService>();

builder.Services.AddScoped<IRestProvider, RestProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// prueba 
app.MapGet("/ping", () => Results.Ok(new { ok = true, svc = "ServiceLocator", at = DateTime.UtcNow }));

app.Run();