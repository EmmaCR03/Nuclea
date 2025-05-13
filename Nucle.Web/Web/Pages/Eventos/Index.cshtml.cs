using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<EventoResponse> eventos { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosEventos");

                using var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(endpoint);

                respuesta.EnsureSuccessStatusCode();

                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                eventos = JsonSerializer.Deserialize<List<EventoResponse>>(contenido, opciones) ?? new List<EventoResponse>();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar eventos: " + ex.Message);
                eventos = new List<EventoResponse>();
            }
        }
    }
}