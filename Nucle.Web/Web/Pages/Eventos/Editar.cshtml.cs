using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Eventos;
using Abstracciones.Modelos.Negocio;
using Abstracciones.Modelos.Servicios;
using Abstracciones.Modelos.TipoEvento;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Web.Pages.Eventos
{
    [Authorize(Roles = "3, 4")]
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public EventoRequest Evento { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> Ubicacion { get; set; }
        [BindProperty]
        public IFormFile? ImagenFile { get; set; }

        [BindProperty]
        public List<SelectListItem> TipoEvento { get; set; }
        public List<SelectListItem> Negocio { get; set; }
        public List<SelectListItem> Servicios { get; set; }


        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            // Cargar el evento existente
            string endpointEvento = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEventoPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);
            var respuestaEvento = await cliente.GetAsync(string.Format(endpointEvento, id));
            respuestaEvento.EnsureSuccessStatusCode();
            var contenidoEvento = await respuestaEvento.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Evento = JsonSerializer.Deserialize<EventoRequest>(contenidoEvento, opciones);

            // Cargar las ubicaciones
            var endpointUbicaciones = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodasUbicaciones");
            var respuestaUbicaciones = await cliente.GetAsync(endpointUbicaciones);
            respuestaUbicaciones.EnsureSuccessStatusCode();
            var contenidoUbicaciones = await respuestaUbicaciones.Content.ReadAsStringAsync();
            Ubicacion = JsonSerializer.Deserialize<List<UbicacionResponse>>(contenidoUbicaciones)
                .Select(u => new SelectListItem { Value = u.idUbicacion.ToString(), Text = u.nombreUbicacion })
                .ToList();

            // Cargar los tipos de evento
            var endpointTiposEvento = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosTiposEvento");
            var respuestaTiposEvento = await cliente.GetAsync(endpointTiposEvento);
            respuestaTiposEvento.EnsureSuccessStatusCode();
            var contenidoTiposEvento = await respuestaTiposEvento.Content.ReadAsStringAsync();
            TipoEvento = JsonSerializer.Deserialize<List<TipoEventoResponse>>(contenidoTiposEvento)
                .Select(t => new SelectListItem { Value = t.idTipoEvento.ToString(), Text = t.nombre })
                .ToList();

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

        public async Task<ActionResult> OnPost()
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ActualizarEvento");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var formData = new MultipartFormDataContent();

                // Agregar campos del evento
                formData.Add(new StringContent(Evento.IdEvento.ToString()), "IdEvento");
                formData.Add(new StringContent(Evento.nombreEvento), "nombreEvento");
                formData.Add(new StringContent(Evento.descripcion), "descripcion");
                formData.Add(new StringContent(Evento.fecha.ToString("yyyy-MM-dd")), "fecha");
                formData.Add(new StringContent(Evento.horaInicio.ToString()), "horaInicio");
                formData.Add(new StringContent(Evento.horaFin.ToString()), "horaFin");
                formData.Add(new StringContent(Evento.fkTipoEvento.ToString()), "fkTipoEvento");
                formData.Add(new StringContent(Evento.fkUbicacion.ToString()), "fkUbicacion");
                formData.Add(new StringContent(Evento.fkNegocio.ToString()), "fkNegocio");
                formData.Add(new StringContent(Evento.fkServicios.ToString()), "fkServicios");
                formData.Add(new StringContent(Evento.ImagenUrl ?? ""), "ImagenUrl");

                // Agregar archivo de imagen si existe
                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    var fileContent = new StreamContent(ImagenFile.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(ImagenFile.ContentType);
                    formData.Add(fileContent, "ImagenFile", ImagenFile.FileName);
                }

                var respuesta = await cliente.PutAsync(string.Format(endpoint, Evento.IdEvento), formData);

                if (!respuesta.IsSuccessStatusCode)
                {
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error al actualizar el evento: " + contenidoError);
                    return Page();
                }

                return RedirectToPage("./IndexADM");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el evento: " + ex.Message);
                return Page();
            }
        }
    }
}

    