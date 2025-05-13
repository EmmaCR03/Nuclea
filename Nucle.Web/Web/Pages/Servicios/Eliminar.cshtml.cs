using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Servicios
{
    [Authorize(Roles = "2 , 4")]

    public class EliminarModel : PageModel
    {
        IConfiguracion _configuracion;
        public ServicioResponse Servicios { get; set; } = default!;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        // Obtener los detalles del servicio para mostrar en la p�gina
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();

            // Obtener el endpoint desde la configuraci�n
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerServicioPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            // Leer la respuesta JSON
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Deserializar el servicio en el modelo
            Servicios = JsonSerializer.Deserialize<ServicioResponse>(resultado, opciones);
            return Page();
        }

        // Acci�n POST para eliminar el servicio
        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            if (!ModelState.IsValid)
                return Page();

            // Obtener el endpoint desde la configuraci�n
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarServicio");
            var cliente = new HttpClient();

            // Realizar la solicitud DELETE
            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            // Redirigir a la p�gina principal de servicios (o cualquier otra p�gina)
            return RedirectToPage("./IndexADM");
        }
    }
}
