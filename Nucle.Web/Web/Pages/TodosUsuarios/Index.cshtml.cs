using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Web.Pages.TodosUsuarios
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<UsuarioResponse> usuarios { get; set; } = new List<UsuarioResponse>();
        public List<PerfilResponse> Perfiles { get; set; } = new List<PerfilResponse>();

        [BindProperty]
        public Guid UsuarioSeleccionadoId { get; set; }

        [BindProperty]
        public int PerfilSeleccionadoId { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                // Usuarios
                string endpointUsuarios = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "TodosUsuarios");
                var cliente = new HttpClient();
                var usuariosResponse = await cliente.GetStringAsync(endpointUsuarios);
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                usuarios = JsonSerializer.Deserialize<List<UsuarioResponse>>(usuariosResponse, opciones)!;

                // Perfiles
                string endpointPerfiles = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "TodosPerfiles");
                var perfilesResponse = await cliente.GetStringAsync(endpointPerfiles);
                Perfiles = JsonSerializer.Deserialize<List<PerfilResponse>>(perfilesResponse, opciones)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Método para guardar perfil seleccionado
        public async Task<IActionResult> OnPostAsignarPerfil(Guid IdUsuario, int IdPerfil)
        {
            if (IdUsuario == Guid.Empty || IdPerfil <= 0)
            {
                // Verifica si los parámetros son válidos
                ModelState.AddModelError(string.Empty, "Los datos proporcionados no son válidos.");
                return Page();
            }

            try
            {
                // Llamar al API para asignar el perfil
                string endpointAsignarPerfil = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "AsignarPerfilAUsuario");
                var cliente = new HttpClient();

                var perfilxusuario = new PerfilxUsuario() { IdPerfil = IdPerfil, IdUsuario = IdUsuario };

                /*var datos = new
                {
                    IdUsuario = IdUsuario,
                    PerfilId = IdPerfil
                };

                var contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");*/
                var respuesta = await cliente.PostAsJsonAsync(endpointAsignarPerfil, perfilxusuario);

                if (respuesta.IsSuccessStatusCode)
                {
                    // Si la asignación es exitosa, redirigir o mostrar mensaje
                    return RedirectToPage("Index");  // Redirige a la página deseada, por ejemplo, la lista de usuarios
                }
                else
                {
                    // Captura el error de la API
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al asignar el perfil: {contenidoError}");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción y muestra el mensaje en el modelo de la página
                ModelState.AddModelError(string.Empty, $"Error al asignar el perfil: {ex.Message}");
                return Page();
            }
        }


    }
}
