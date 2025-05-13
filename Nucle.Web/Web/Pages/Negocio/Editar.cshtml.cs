using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Web.Pages.Negocio
{
    [Authorize(Roles = "3 , 4")]

    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public NegocioRequest Negocio { get; set; } = new NegocioRequest();

        [BindProperty]
        public IFormFile? ImagenFile { get; set; }

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            try
            {
                string endpointNegocio = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerNegocioPorId");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuestaNegocio = await cliente.GetAsync(string.Format(endpointNegocio, id));
                respuestaNegocio.EnsureSuccessStatusCode();

                var contenidoNegocio = await respuestaNegocio.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var negocioResponse = JsonSerializer.Deserialize<NegocioResponse>(contenidoNegocio, opciones);

                Negocio = new NegocioRequest
                {
                    idNegocio = negocioResponse.idNegocio,
                    nombre = negocioResponse.nombre ?? string.Empty,
                    descripcion = negocioResponse.descripcion ?? string.Empty,
                    ImagenUrl = negocioResponse.ImagenUrl ?? string.Empty
                };

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al cargar el negocio: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ActualizarNegocio");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(Negocio.idNegocio.ToString()), "idNegocio");
                formData.Add(new StringContent(Negocio.nombre), "nombre");
                formData.Add(new StringContent(Negocio.descripcion), "descripcion");

                if (!string.IsNullOrEmpty(Negocio.ImagenUrl))
                {
                    formData.Add(new StringContent(Negocio.ImagenUrl), "ImagenUrl");
                }

                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    if (ImagenFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ImagenFile", "La imagen no puede ser mayor a 5MB");
                        return Page();
                    }

                    var fileContent = new StreamContent(ImagenFile.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(ImagenFile.ContentType);
                    formData.Add(fileContent, "ImagenFile", ImagenFile.FileName);
                }

                var respuesta = await cliente.PutAsync(string.Format(endpoint, Negocio.idNegocio), formData);

                if (!respuesta.IsSuccessStatusCode)
                {
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error al actualizar el negocio: " + contenidoError);
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el negocio: " + ex.Message);
                return Page();
            }
        }
    }
}