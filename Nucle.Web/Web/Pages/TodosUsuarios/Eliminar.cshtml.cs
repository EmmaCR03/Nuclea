using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TodosUsuarios
{
    public class EliminarModel : PageModel
    {
        IConfiguracion _configuracion;
        public UsuarioResponse usuario { get; set; } = default!;
        public EliminarModel(IConfiguracion configuracion)
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

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "EliminarUsuario");
            var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
