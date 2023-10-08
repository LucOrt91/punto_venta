using POS.Api.Extensions;
using POS.Application.Extensions;
using POS.Infrastructure.Extensions;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Agregar servicios al contenedor.
var Cors = "Cors";
builder.Services.AddInjectionInfrastructure(Configuration);
builder.Services.AddInjectionApplication(Configuration);
builder.Services.AddAuthentication(Configuration);

builder.Services.AddControllers();
// Obtén más información sobre cómo configurar Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(Cors);

// Configurar el canal de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWatchDogExceptionLogger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDog(configuration =>
{
    configuration.WatchPagePassword = Configuration.GetSection("WatchDog:Username").Value;
    configuration.WatchPageUsername = Configuration.GetSection("WatchDog:Password").Value;
});

app.Run();

public partial class Program { }
