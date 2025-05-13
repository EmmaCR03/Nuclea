using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Servicios
{
    [Authorize(Roles = "2 , 4")]
    public class IndexADMModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ServicioResponse> Servicios { get; set; } = default!;

        public IndexADMModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                // Obtener el endpoint para "ObtenerTodosServicios"
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosServicios");

                // Crear el cliente HTTP
                var cliente = new HttpClient();

                // Crear la solicitud
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                // Enviar la solicitud
                var respuesta = await cliente.SendAsync(solicitud);

                // Verificar que la respuesta sea exitosa
                respuesta.EnsureSuccessStatusCode();

                // Leer y deserializar la respuesta
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Servicios = JsonSerializer.Deserialize<List<ServicioResponse>>(resultado, opciones);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
