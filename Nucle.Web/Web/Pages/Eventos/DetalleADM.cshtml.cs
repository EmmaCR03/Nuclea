using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    [Authorize(Roles = "3, 4")]
    public class DetalleADMModel : PageModel
    {
        IConfiguracion _configuracion;
        public EventoResponse evento { get; set; } = default!;
        public EventoResponse Negocio { get; set; } = default!;
        public EventoResponse Servicios { get; set; } = default!;


        public DetalleADMModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEventoPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                               HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            evento = JsonSerializer.Deserialize<EventoResponse>
                (resultado, opciones);
        }
    }
}