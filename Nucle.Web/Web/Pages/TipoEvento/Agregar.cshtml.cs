using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.TipoEvento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace Web.Pages.TipoEvento
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public TipoEventoRequest tipoEvento { get; set; } = default!;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarTipoEvento"); 
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsJsonAsync(endpoint, tipoEvento);
                respuesta.EnsureSuccessStatusCode();
                return RedirectToPage("./Index");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Error al conectar con la API.");
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado.");
                return Page();
            }
        }
    }
}
