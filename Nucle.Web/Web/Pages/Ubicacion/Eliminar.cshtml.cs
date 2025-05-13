using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Ubicacion
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public UbicacionResponse Ubicacion { get; set; } = default!;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUbicacionPorId");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Ubicacion = JsonSerializer.Deserialize<UbicacionResponse>(contenido, opciones);

            return Page();
        }

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarUbicacion");
            var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
