using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Servicios
{
    [Authorize(Roles = "2 , 4")]
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public ServicioRequest Servicios { get; set; } = default!;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarServicio");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsJsonAsync(endpoint, Servicios);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./IndexADM");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Error al conectar con el servidor.");
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado.");
                return Page();
            }
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
