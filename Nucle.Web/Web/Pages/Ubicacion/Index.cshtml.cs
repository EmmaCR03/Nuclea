using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Ubicacion
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<UbicacionResponse> ubicaciones { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                // Obtener el endpoint desde la configuración
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodasUbicaciones");

                using var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var respuesta = await cliente.SendAsync(solicitud);

                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                ubicaciones = JsonSerializer.Deserialize<List<UbicacionResponse>>(resultado, opciones) ?? new List<UbicacionResponse>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }
    }
}
