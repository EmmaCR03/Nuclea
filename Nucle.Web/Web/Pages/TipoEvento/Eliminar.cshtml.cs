using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.TipoEvento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoEvento
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public TipoEventoResponse TipoEvento { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoEventoPorId");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            TipoEvento = JsonSerializer.Deserialize<TipoEventoResponse>(contenido, opciones)!;

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarTipoEvento");
            var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
