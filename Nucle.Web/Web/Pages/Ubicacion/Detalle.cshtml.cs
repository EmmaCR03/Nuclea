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

                // Opci�n 1: Deserializaci�n directa
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Intenta deserializar primero como objeto �nico
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
                    throw new Exception("No se pudo deserializar la ubicaci�n");
                }
            }
            catch (Exception ex)
            {
                // Log del error
                Console.WriteLine($"Error al obtener ubicaci�n: {ex.Message}");
                // Redirige a una p�gina de error o maneja seg�n necesites
                throw;
            }
        }
    }
}