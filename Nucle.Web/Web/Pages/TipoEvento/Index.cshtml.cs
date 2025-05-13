using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.TipoEvento;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoEvento
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<TipoEventoResponse> tipoEventos { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosTiposEvento");

                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var respuesta = await cliente.SendAsync(solicitud);

                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                tipoEventos = JsonSerializer.Deserialize<List<TipoEventoResponse>>(resultado, opciones)!;
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
