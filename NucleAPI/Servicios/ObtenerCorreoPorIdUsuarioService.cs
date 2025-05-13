using Abstracciones.Interfaces.Servicios;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Servicios
{
    public class ObtenerCorreoPorIdUsuarioService : IObtenerCorreoPorIdUsuarioService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ObtenerCorreoPorIdUsuarioService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> ObtenerCorreoPorIdUsuario(Guid idUsuario)
        {
            try
            {
                // Obtén la URL base de la API de Seguridad desde appsettings.json
                var apiSeguridadUrl = _configuration["ApiSettings:ApiSeguridad:UrlBase"];

                // Obtener el endpoint con el formato adecuado
                var endpoint = _configuration["ApiSettings:ApiSeguridad:ApiEndPoints:ObtenerCorreoPorIdUsuario"];

                // Crear cliente HTTP
                var client = _httpClientFactory.CreateClient();

                // Llamada al endpoint, reemplazando el {0} por el idUsuario
                var response = await client.GetAsync($"{apiSeguridadUrl}/{string.Format(endpoint, idUsuario)}");

                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta
                    string correo = await response.Content.ReadAsStringAsync();
                    return correo;
                }
                else
                {
                    // Manejar el caso en que no se encontró el correo del usuario
                    throw new Exception("No se pudo obtener el correo del usuario.");
                }
            }
            catch (Exception ex)
            {
                // Loggear el error y lanzar una excepción
                throw new Exception($"Error al obtener el correo del usuario: {ex.Message}", ex);
            }
        }
    }
}
