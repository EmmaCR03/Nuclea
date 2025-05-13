using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Abstracciones.Interfaces;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Options;

namespace Servicios.Implementaciones
{
    public class GeneradorPDFService : IGeneradorPDFService
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PdfApiConfig _pdfConfig;
        private readonly IConfiguration _configuration;

        public GeneradorPDFService(
            IConfiguracion configuracion,
            IHttpClientFactory httpClientFactory,
            IOptions<PdfApiConfig> pdfOptions,
            IConfiguration configuration)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
            _pdfConfig = pdfOptions.Value;
            _configuration = configuration;
        }

        public async Task<byte[]> GenerarPDFAsync(RegistrarEventoResponse evento)
        {
            try
            {
                // 1. Obtener endpoint base usando IConfiguracion con la ruta completa
                var endpoint = _configuracion.ObtenerMetodo("ApiSettings:ApiEndPointsGeneradorPDF", "GenerarPDF");
                // 2. Validar configuración del PDF
                if (string.IsNullOrWhiteSpace(_pdfConfig.ApiKey))
                    throw new ArgumentNullException(nameof(_pdfConfig.ApiKey), "API Key no configurada para el servicio de PDF");

                if (string.IsNullOrWhiteSpace(_pdfConfig.TemplateId))
                    throw new ArgumentNullException(nameof(_pdfConfig.TemplateId), "Template ID no configurado");

                // 3. Configurar HttpClient
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("X-API-KEY", _pdfConfig.ApiKey);

                // 4. Construir request body
                var requestData = new
                {
                    template_id = _pdfConfig.TemplateId,
                    data = new
                    {
                        eventName = evento.nombreEvento,
                        userName = evento.nombreUsuario,
                        eventDate = evento.fecha.ToString("yyyy-MM-dd"),
                        startTime = evento.horaInicio.ToString(@"hh\:mm"),
                        description = evento.descripcion,
                        qr_code = evento.qr
                    },
                    export_type = "file"
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                    "application/json");

                // 5. Logging para debug
                Console.WriteLine($"Endpoint: {endpoint}");
                Console.WriteLine($"Template ID: {_pdfConfig.TemplateId}");
                Console.WriteLine($"Request Body: {JsonSerializer.Serialize(requestData)}");

                // 6. Enviar petición
                var response = await client.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(
                        $"Error al generar PDF. Status: {response.StatusCode}. Detalles: {errorContent}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GeneradorPDFService: {ex}");
                throw;
            }
        }
    }
}