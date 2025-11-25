using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        // Inyecciones de dependencias
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IAutorRepository, AutorRepository>();
        builder.Services.AddScoped<ILibroRepository, LibroRepository>();
        builder.Services.AddScoped<IExtractorService<LibroDTO>, LibroService>();
        builder.Services.AddScoped<IExtractorService<AutorDTO>, AutorService>();

        // Configuración dinámica de PostgreSQL desde variable de entorno DATABASE_URL
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        if (!string.IsNullOrEmpty(databaseUrl))
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var npgsqlBuilder = new Npgsql.NpgsqlConnectionStringBuilder()
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.AbsolutePath.TrimStart('/'),
                SslMode = Npgsql.SslMode.Require,
                TrustServerCertificate = true
            };

            builder.Services.AddDbContext<ContactDbContext>(options =>
                options.UseNpgsql(npgsqlBuilder.ConnectionString));
        }
        else
        {
            // Fallback a appsettings.json
            builder.Services.AddDbContext<ContactDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
        }

        // builder.Services.AddDbContext<ContactDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middleware global para capturar errores y mostrar detalles
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Muestra errores completos en el navegador
        }
        else
        {
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    if (feature != null)
                    {
                        var ex = feature.Error;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = ex.Message,
                            stackTrace = ex.StackTrace
                        });
                    }
                });
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
