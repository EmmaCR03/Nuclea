using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    public class IndexADMModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<EventoResponse> eventos { get; set; } = default!;

        public IndexADMModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;

        }
        public async Task OnGet()
        {
            try
            {
                // Obtener el endpoint para "ObtenerTodosEventos"
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosEventos");

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
                eventos = JsonSerializer.Deserialize<List<EventoResponse>>(resultado, opciones);
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de HTTP
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
     
    }
}

