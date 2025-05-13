using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Web.Pages.RegistroEvento
{
    public class DetalleRegistroModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private readonly string _apiBaseUrl;

        public RegistrarEventoResponse Detalle { get; set; }

        public DetalleRegistroModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
            _apiBaseUrl = _configuracion.ObtenerValor("ApiEndPoints:UrlBase");
        }

        

        public async Task<IActionResult> OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "ID de registro no válido";
                return RedirectToPage("/Eventos/Index");
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "VerRegistroEventoPorId");

            using var cliente = new HttpClient();

            try
            {
                var token = await HttpContext.GetTokenAsync("access_token") ??
                           User.FindFirstValue("Token") ??
                           throw new Exception("Token no encontrado");

                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var respuesta = await cliente.GetAsync(string.Format(endpoint, id));

                if (!respuesta.IsSuccessStatusCode)
                {
                    if (respuesta.StatusCode == HttpStatusCode.NotFound)
                    {
                        TempData["ErrorMessage"] = "Registro no encontrado";
                        return RedirectToPage("/Eventos/Index");
                    }
                    respuesta.EnsureSuccessStatusCode();
                }

                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Detalle = JsonSerializer.Deserialize<RegistrarEventoResponse>(contenido, opciones);

                if (Detalle == null)
                {
                    TempData["ErrorMessage"] = "No se pudo interpretar la respuesta del servidor";
                    return RedirectToPage("/Eventos/Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar el registro: {ex.Message}";
                return RedirectToPage("/Eventos/Index");
            }
        }

        public async Task<IActionResult> OnPostDescargarPDF(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "ID de registro no válido";
                return RedirectToPage();
            }

            try
            {
                var token = await HttpContext.GetTokenAsync("access_token") ??
                           User.FindFirstValue("Token") ??
                           throw new Exception("Token no encontrado");

                using var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                // Usamos la URL base de la configuración
                var endpoint = $"{_apiBaseUrl}/RegistroEvento/{id}/pdf";
                var response = await cliente.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Error al generar PDF: {errorContent}";
                    return RedirectToPage();
                }

                var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                return File(pdfBytes, "application/pdf", $"Registro_{id}.pdf");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}