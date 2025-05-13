using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.TipoEvento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoEvento
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public TipoEventoResponse tipoEvento { get; set; } = default!;

        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoEventoPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            tipoEvento = JsonSerializer.Deserialize<TipoEventoResponse>(resultado, opciones)!;
        }
    }
}
