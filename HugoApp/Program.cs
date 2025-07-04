using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using taskmanager_webservice.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de DbContext con PostgreSQL
builder.Services.AddDbContext<OperacionesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// Agregar servicios para controladores
builder.Services.AddControllers();

// Agregar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Manager API", Version = "v1" });
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API v1");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();

