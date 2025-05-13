using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Abstracciones.Modelos.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Negocio
{
    [Authorize(Roles = "3 , 4")]

    public class EliminarModel : PageModel
    {
        IConfiguracion _configuracion;
        public NegocioResponse negocio { get; set; } = default!;
        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerNegocioPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            negocio = JsonSerializer.Deserialize<NegocioResponse>
                (resultado, opciones);
            return Page();
        }

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarNegocio");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}
