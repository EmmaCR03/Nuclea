using Abstracciones.Interfaces.AccesoADatos.Eventos;
using Abstracciones.Interfaces.AccesoADatos.Negocios;
using Abstracciones.Interfaces.AccesoADatos.Persona;
using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Interfaces.AccesoADatos.Servicios;
using Abstracciones.Interfaces.AccesoADatos.TipoEvento;
using Abstracciones.Interfaces.AccesoADatos.Ubicacion;
using Abstracciones.Interfaces.Flujo.Eventos;
using Abstracciones.Interfaces.Flujo.Negocios;
using Abstracciones.Interfaces.Flujo.Persona;
using Abstracciones.Interfaces.Flujo.Servicios;
using Abstracciones.Interfaces.Flujo.TipoEventos;
using Abstracciones.Interfaces.Flujo.Ubicacion;
using Abstracciones.Modelos.Seguridad;
using AccesoADatos;
using AccesoADatos.Negocio;
using AccesoADatos.Repositorios;
using AccesoADatos.TipoEvento;
using AccesoADatos.Ubicacion;
using Flujo;
using Flujo.Eventos;
using Flujo.Flujo.Persona;
using Flujo.Negocio;
using Flujo.TipoEvento;
using Flujo.Ubicacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Autorizacion.Middleware;
using Abstracciones.Interfaces.AccesoADatos.Eventos.Registrar;
using Abstracciones.Interfaces.Flujo.Eventos.Registrar;
using Abstracciones.Interfaces.Servicios;
using Servicios;
using Abstracciones.Interfaces;
using Reglas;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using FluentAssertions.Common;
using Servicios.Implementaciones;
using Abstracciones.Modelos.Servicios;


var builder = WebApplication.CreateBuilder(args);

//Autenticacion
var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
var jwtIssuer = tokenConfiguration.Issuer;
var jwtAudience = tokenConfiguration.Audience;
var jwtKey = tokenConfiguration.key;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IEventosFlujo, EventosFlujo>();
builder.Services.AddScoped<IEventosAccesoADatos, EventosAccesoADatos>();
builder.Services.AddScoped<INegociosFlujo, NegociosFlujo>();
builder.Services.AddScoped<INegocioAccesoADatos, NegocioAccesoADatos>();
builder.Services.AddScoped<ITipoEventoFlujo, TipoEventoFlujo>();
builder.Services.AddScoped<ITipoEventoAccesoADatos, TipoEventoAccesoADatos>();
builder.Services.AddScoped<IUbicacionFlujo, UbicacionFlujo>();
builder.Services.AddScoped<IUbicacionAccesoADatos, UbicacionAccesoADatos>();
builder.Services.AddScoped<IServicioAccesoADatos, ServicioAccesoADatos>();
builder.Services.AddScoped<IServicioFlujo, ServicioFlujo>();
builder.Services.AddScoped<IPersonaFlujo, PersonaFlujo>();
builder.Services.AddScoped<IPersonaAccesoADatos, PersonaAccesoADatos>();
builder.Services.AddScoped<IRegistrarEventoAccesoADatos, RegistrarEventoAccesoADatos>();
builder.Services.AddScoped<IRegistrarUsuarioAEventoFlujo, RegistrarUsuarioAEventoFlujo>();
builder.Services.AddScoped<IGeneradorQRServicios, GeneradorQRServicios>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IObtenerCorreoPorIdUsuarioService, ObtenerCorreoPorIdUsuarioService>();
builder.Services.AddScoped<IGeneradorPDFService, GeneradorPDFService>();
builder.Services.Configure<PdfApiConfig>(
    builder.Configuration.GetSection("ApiSettings:ApiEndPointsGeneradorPDF"));


builder.Services.AddTransient<Autorizacion.Abstracciones.Flujo.IAutorizacionFlujo, Autorizacion.Flujo.AutorizacionFlujo>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA, Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapControllers();

app.Run();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}