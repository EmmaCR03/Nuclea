using Abstracciones.Interfaces.Flujo.Negocios;
using Abstracciones.Interfaces.Flujo.Servicios;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioFlujo _serviciosFlujo;
        private readonly ILogger<ServiciosController> _logger;

        public ServiciosController(IServicioFlujo serviciosFlujo, ILogger<ServiciosController> logger)
        {
            _serviciosFlujo = serviciosFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ServicioRequest servicios)
        {
            var resultado = await _serviciosFlujo.Agregar(servicios);
            return CreatedAtAction(nameof(Obtener), new { idServicio = resultado }, servicios);
        }

        [HttpPut("{idServicio}")]
        public async Task<IActionResult> Editar([FromRoute] string idServicio, [FromBody] ServicioRequest servicios)
        {
            if (!Guid.TryParse(idServicio, out Guid servicioId))
                return BadRequest("El ID del servicio no tiene un formato válido.");

            try
            {
                if (!await VerificarServicioExiste(servicioId))
                    return NotFound("El servicio no existe");

                await _serviciosFlujo.Editar(servicioId, servicios);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el servicio.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{idServicio}")]
        public async Task<IActionResult> Eliminar([FromRoute] string idServicio)
        {
            if (!Guid.TryParse(idServicio, out Guid servicioId))
                return BadRequest("El ID del servicio no tiene un formato válido.");

            try
            {
                if (!await VerificarServicioExiste(servicioId))
                    return NotFound("El servicio no existe");

                await _serviciosFlujo.Eliminar(servicioId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el servicio.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _serviciosFlujo.Obtener();
            if (!resultado.Any())
            {
                return NotFound("No se encontraron servicios.");
            }
            return Ok(resultado);
        }

        [HttpGet("{idServicio}")]
        public async Task<IActionResult> Obtener([FromRoute] string idServicio)
        {
            if (!Guid.TryParse(idServicio, out Guid servicioId))
                return BadRequest("El ID del servicio no tiene un formato válido.");

            var resultado = await _serviciosFlujo.Obtener(servicioId);
            if (resultado == null)
            {
                return NotFound("El servicio no existe.");
            }
            return Ok(resultado);
        }

        private async Task<bool> VerificarServicioExiste(Guid IdServicio)
        {
            var resultadoServicio = await _serviciosFlujo.Obtener(IdServicio);
            return resultadoServicio != null;
        }
    }
}