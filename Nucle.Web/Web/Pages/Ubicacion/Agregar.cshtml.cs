using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Ubicacion;
using Abstracciones.Modelos.Ubicacion.Abstracciones.Modelos.Ubicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Ubicacion
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public UbicacionRequest Ubicacion { get; set; } = new UbicacionRequest();

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarUbicacion");
                var cliente = new HttpClient();

                var json = JsonSerializer.Serialize(Ubicacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endpoint, content);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./Index");
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