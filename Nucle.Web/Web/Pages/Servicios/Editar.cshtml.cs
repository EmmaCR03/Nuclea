using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Servicios
{
    [Authorize(Roles = "2 , 4")]

    public class EditarServicioModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public ServicioResponse Servicio { get; set; } = default!;

        public EditarServicioModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }


            string endpointServicio = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerServicioPorId");
            var cliente = new HttpClient();
            var respuestaServicio = await cliente.GetAsync(string.Format(endpointServicio, id));
            respuestaServicio.EnsureSuccessStatusCode();
            var contenidoServicio = await respuestaServicio.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Servicio = JsonSerializer.Deserialize<ServicioResponse>(contenidoServicio, opciones);

            return Page();
        }

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ActualizarServicio");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync<ServicioBase>(
                string.Format(endpoint, Servicio.idServicio),
                new ServicioRequest
                {

                    nombreServicio = Servicio.nombreServicio,
                    descripcion = Servicio.descripcion,
                    costo = Servicio.costo
                });

            if (!respuesta.IsSuccessStatusCode)
            {
                var contenidoError = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine("Error de la API:");
                Console.WriteLine(contenidoError);
            }

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./IndexADM");
        }
    }
}
