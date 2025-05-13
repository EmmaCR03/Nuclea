using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.TipoEvento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoEvento
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public TipoEventoRequest TipoEvento { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoEventoPorId");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            TipoEvento = JsonSerializer.Deserialize<TipoEventoRequest>(contenido, opciones);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ActualizarTipoEvento");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, TipoEvento.idTipoEvento), TipoEvento);

            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine("Error de la API al actualizar tipo de evento: " + error);
                ModelState.AddModelError(string.Empty, "Error al actualizar el tipo de evento.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
