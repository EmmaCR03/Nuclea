using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Abstracciones.Modelos.Negocio;
using Abstracciones.Modelos.Servicios;
using Abstracciones.Modelos.TipoEvento;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    [Authorize(Roles = "3, 4")]
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public EventoRequest evento { get; set; } = default!;

        [BindProperty]
        public IFormFile ImagenFile { get; set; }

        [BindProperty]
        public List<SelectListItem> ubicacion { get; set; }

        [BindProperty]
        public List<SelectListItem> tipoEvento { get; set; }

        public List<SelectListItem> Negocio { get; set; }
        public List<SelectListItem> Servicios { get; set; }

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarEvento");
                using var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var formData = new MultipartFormDataContent();

                // Agregar campos del evento
                formData.Add(new StringContent(evento.nombreEvento), "nombreEvento");
                formData.Add(new StringContent(evento.descripcion), "descripcion");
                formData.Add(new StringContent(evento.fecha.ToString("yyyy-MM-dd")), "fecha");
                formData.Add(new StringContent(evento.horaInicio.ToString()), "horaInicio");
                formData.Add(new StringContent(evento.horaFin.ToString()), "horaFin");
                formData.Add(new StringContent(evento.fkTipoEvento.ToString()), "fkTipoEvento");
                formData.Add(new StringContent(evento.fkUbicacion.ToString()), "fkUbicacion");
                formData.Add(new StringContent(evento.fkNegocio.ToString()), "fkNegocio");
                formData.Add(new StringContent(evento.fkServicios.ToString()), "fkServicios");

                // Agregar archivo de imagen si existe
                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    var fileContent = new StreamContent(ImagenFile.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(ImagenFile.ContentType);
                    formData.Add(fileContent, "ImagenFile", ImagenFile.FileName);
                }

                var respuesta = await cliente.PostAsync(endpoint, formData);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./IndexADM");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al agregar el evento: " + ex.Message);
                return Page();
            }
        }

        public async Task<ActionResult> OnGet()
        {
            try
            {
                // Cargar las ubicaciones
                var endpointUbicaciones = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodasUbicaciones");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuestaUbicaciones = await cliente.GetAsync(endpointUbicaciones);
                respuestaUbicaciones.EnsureSuccessStatusCode();
                var contenidoUbicaciones = await respuestaUbicaciones.Content.ReadAsStringAsync();
                ubicacion = JsonSerializer.Deserialize<List<UbicacionResponse>>(contenidoUbicaciones)
                    .Select(u => new SelectListItem { Value = u.idUbicacion.ToString(), Text = u.nombreUbicacion })
                    .ToList();

                // Cargar los tipos de evento
                var endpointTiposEvento = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosTiposEvento");
                var respuestaTiposEvento = await cliente.GetAsync(endpointTiposEvento);
                respuestaTiposEvento.EnsureSuccessStatusCode();
                var contenidoTiposEvento = await respuestaTiposEvento.Content.ReadAsStringAsync();
                tipoEvento = JsonSerializer.Deserialize<List<TipoEventoResponse>>(contenidoTiposEvento)
                    .Select(t => new SelectListItem { Value = t.idTipoEvento.ToString(), Text = t.nombre })
                    .ToList();

                // Cargar negocios
                var endpointNegocio = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosNegocios");
                var respuestaNegocio = await cliente.GetAsync(endpointNegocio);
                respuestaNegocio.EnsureSuccessStatusCode();
                var contenidoNegocio = await respuestaNegocio.Content.ReadAsStringAsync();
                Negocio = JsonSerializer.Deserialize<List<NegocioResponse>>(contenidoNegocio)
                    .Select(t => new SelectListItem { Value = t.idNegocio.ToString(), Text = t.nombre })
                    .ToList();
                var endpointServicio = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosServicios");
                var respuestaServicio = await cliente.GetAsync(endpointServicio);
                respuestaServicio.EnsureSuccessStatusCode();
                var contenidoServicio = await respuestaServicio.Content.ReadAsStringAsync();
                Servicios = JsonSerializer.Deserialize<List<ServicioResponse>>(contenidoServicio)
                    .Select(t => new SelectListItem { Value = t.idServicio.ToString(), Text = t.nombreServicio })
                    .ToList();

                return Page();
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Error al conectar con el servidor. Por favor, inténtelo de nuevo más tarde.");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado. Por favor, inténtelo de nuevo más tarde.");
                return Page();
            }
        }
    }
}