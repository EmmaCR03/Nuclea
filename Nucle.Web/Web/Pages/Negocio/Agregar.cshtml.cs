using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.Negocio
{
    [Authorize(Roles = "`3 , 4")]

    public class AgregarModel : PageModel
    {

        IConfiguracion _configuracion;
        [BindProperty]
        public NegocioRequest negocio { get; set; } = default!;


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
                // Procesar imagen si existe
                if (Request.Form.Files.Count > 0)
                {
                    var imagenFile = Request.Form.Files["imagenFile"];
                    if (imagenFile != null && imagenFile.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await imagenFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        negocio.ImagenUrl = $"data:{imagenFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                    }
                }

                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarNegocio");
                var cliente = new HttpClient();

                // Configurar el token de autorización si es necesario
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                // Usar FormData para enviar la información
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(negocio.nombre ?? ""), "nombre");
                formData.Add(new StringContent(negocio.descripcion ?? ""), "descripcion");

                if (!string.IsNullOrEmpty(negocio.ImagenUrl))
                {
                    formData.Add(new StringContent(negocio.ImagenUrl), "ImagenUrl");
                }

                var respuesta = await cliente.PostAsync(endpoint, formData);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar el negocio: " + ex.Message);
                return Page();
            }
        }


    }
}