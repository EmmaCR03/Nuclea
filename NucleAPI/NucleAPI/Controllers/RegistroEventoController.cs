using Abstracciones.Interfaces.Flujo.Eventos;
using Abstracciones.Interfaces.Flujo.Eventos.Registrar;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroEventoController : ControllerBase
    {
        private IRegistrarUsuarioAEventoFlujo _registrar;
        private ILogger<RegistroEventoController> _logger;

        public RegistroEventoController(IRegistrarUsuarioAEventoFlujo registrarUsuarioFlujo, ILogger<RegistroEventoController> logger)
        {
            _registrar = registrarUsuarioFlujo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] RegistrarEventoRequest RegistrarAevento)
        {
            var resultado = await _registrar.Agregar(RegistrarAevento);
            return CreatedAtAction(nameof(Obtener), new { idEventoRegistrado = resultado }, null);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _registrar.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        [HttpGet("{idEventoRegistrado}")]
        [AllowAnonymous]
        public async Task<IActionResult> Obtener([FromRoute] Guid idEventoRegistrado)
        {
            var resultado = await _registrar.Obtener(idEventoRegistrado);
            if (resultado == null)
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{idEventoRegistrado}/pdf")]
        public async Task<IActionResult> DescargarPDF([FromRoute] Guid idEventoRegistrado)
        {
            try
            {
                _logger.LogInformation($"Generando PDF para registro {idEventoRegistrado}");

                var pdfBytes = await _registrar.GenerarPDF(idEventoRegistrado);

                _logger.LogInformation($"PDF generado correctamente para {idEventoRegistrado}");
                return File(pdfBytes, "application/pdf", $"Registro_{idEventoRegistrado}.pdf");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Registro no encontrado: {idEventoRegistrado}");
                return NotFound(new { Error = "Registro no encontrado" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error de validación: {ex.Message}");
                return BadRequest(new { Error = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error al comunicarse con CraftMyPDF");
                return StatusCode(502, new
                {
                    Error = "Error en el servicio de PDF",
                    Detalles = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error inesperado generando PDF");
                return StatusCode(500, new
                {
                    Error = "Error interno al generar PDF",
                    Detalles = ex.Message
                });
            }
        }
    }
}
