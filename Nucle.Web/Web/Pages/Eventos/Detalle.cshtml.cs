using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    public class DetalleModel : PageModel
    {
        IConfiguracion _configuracion;
        public EventoResponse evento { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEventoPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            evento = JsonSerializer.Deserialize<EventoResponse>
                (resultado, opciones);
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Login");

            var userId = User.FindFirstValue("IdUsuario") ??
                        User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                        throw new Exception("Usuario no autenticado");

            if (!Guid.TryParse(userId, out var usuarioId))
                throw new Exception("ID de usuario inválido");

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "RegistroEvento");

            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

            var token = await HttpContext.GetTokenAsync("access_token") ??
                       HttpContext.User.FindFirstValue("Token") ??
                       throw new Exception("Token no encontrado");

            

            try
            {
                var registro = new
                {
                    qr = "", // Vacío para que el backend lo genere
                    idEvento = id,
                    idUsuario = usuarioId
                };

                // 1. Crear el registro
                var respuesta = await cliente.PostAsJsonAsync(endpoint, registro);

                if (respuesta.StatusCode == HttpStatusCode.Created)
                {
                    // 2. Obtener el ID del registro recién creado
                    var registroId = await ObtenerUltimoRegistroId(usuarioId, id.Value);

                    // 3. Redirigir a la página de detalles
                    return RedirectToPage("/RegistrarEvento/DetalleRegistro", new { id = registroId });
                }

                // Manejo de errores
                var errorContent = await respuesta.Content.ReadAsStringAsync();
                throw new Exception($"Error en el registro: {respuesta.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al registrar: {ex.Message}";
                return RedirectToPage("./Detalle", new { id = id });
            }
        }

        private async Task<Guid> ObtenerUltimoRegistroId(Guid usuarioId, Guid eventoId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "VerRegistrosEvento");

            try
            {
                using var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

                var token = await HttpContext.GetTokenAsync("access_token");
                

                var respuesta = await cliente.GetAsync(endpoint);
                var contenido = await respuesta.Content.ReadAsStringAsync();

                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var registros = JsonSerializer.Deserialize<List<RegistrarEventoResponse>>(contenido, opciones);

                return registros?
                    .Where(r => r.idUsuario == usuarioId && r.idEvento == eventoId)
                    .OrderByDescending(r => r.fecha)
                    .FirstOrDefault()?
                    .idEventoRegistrado ?? eventoId; // Fallback al ID del evento
            }
            catch
            {
                return eventoId; // Fallback al ID del evento si hay error
            }
        }
    }
}
