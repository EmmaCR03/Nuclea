using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Negocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Negocio
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<NegocioResponse> negocios { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;

        }
        public async Task OnGet()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodosNegocios");

                // Crear el cliente HTTP
                var cliente = new HttpClient();

                // Crear la solicitud
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                // Enviar la solicitud
                var respuesta = await cliente.SendAsync(solicitud);

                // Verificar que la respuesta sea exitosa
                respuesta.EnsureSuccessStatusCode();

                // Leer y deserializar la respuesta
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                negocios = JsonSerializer.Deserialize<List<NegocioResponse>>(resultado, opciones);
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de HTTP
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

