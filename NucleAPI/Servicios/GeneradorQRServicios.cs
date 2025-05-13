using Abstracciones.Interfaces;
using Abstracciones.Interfaces.Servicios;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servicios
{
    public class GeneradorQRServicios : IGeneradorQRServicios
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;

        public GeneradorQRServicios(IConfiguracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GenerarQR(string texto)
        {
            try
            {
                var url = _configuracion.ObtenerMetodo("ApiSettings:ApiEndPointsGeneradorQR", "GenerarCodigoQR");


                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                // Loggear el error
                throw new Exception($"Error al generar QR: {ex.Message}", ex);
            }
        }
    }
}