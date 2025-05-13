using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Ubicacion
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public UbicacionResponse Ubicacion { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUbicacionPorId");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();

                // Opción 1: Deserialización directa
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Intenta deserializar primero como objeto único
                try
                {
                    Ubicacion = JsonSerializer.Deserialize<UbicacionResponse>(resultado, opciones);
                }
                catch (JsonException)
                {
                    // Si falla, intenta como array
                    var ubicaciones = JsonSerializer.Deserialize<List<UbicacionResponse>>(resultado, opciones);
                    Ubicacion = ubicaciones?.FirstOrDefault();
                }

                if (Ubicacion == null)
                {
                    throw new Exception("No se pudo deserializar la ubicación");
                }
            }
            catch (Exception ex)
            {
                // Log del error
                Console.WriteLine($"Error al obtener ubicación: {ex.Message}");
                // Redirige a una página de error o maneja según necesites
                throw;
            }
        }
    }
}