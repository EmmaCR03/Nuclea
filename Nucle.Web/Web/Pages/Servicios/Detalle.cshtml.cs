using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Servicios
{
    [Authorize(Roles = "2 , 4")]
    public class DetalleADMModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public ServicioResponse Servicio { get; set; } = default!;

        public DetalleADMModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerServicioPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Servicio = JsonSerializer.Deserialize<ServicioResponse>
    (resultado, opciones)!;
        }
    }
}
