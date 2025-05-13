using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Ubicacion
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public UbicacionRequest Ubicacion { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            // Cargar la ubicación existente
            string endpointUbicacion = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUbicacionPorId");
            var cliente = new HttpClient();
            var respuestaUbicacion = await cliente.GetAsync(string.Format(endpointUbicacion, id));
            respuestaUbicacion.EnsureSuccessStatusCode();
            var contenidoUbicacion = await respuestaUbicacion.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Ubicacion = JsonSerializer.Deserialize<UbicacionRequest>(contenidoUbicacion, opciones);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ActualizarUbicacion");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Ubicacion.idUbicacion), Ubicacion);

            if (!respuesta.IsSuccessStatusCode)
            {
                // Captura el mensaje de error de la API
                var contenidoError = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine("Error de la API:");
                Console.WriteLine(contenidoError);
            }

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}