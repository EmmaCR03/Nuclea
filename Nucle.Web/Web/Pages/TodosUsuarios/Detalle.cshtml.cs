using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TodosUsuarios
{
    public class DetalleModel : PageModel
    {
        IConfiguracion _configuracion;
        public UsuarioResponse usuario { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "ObtenerUsuarioPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            usuario = JsonSerializer.Deserialize<UsuarioResponse>(resultado, opciones);
        }
    }
}
