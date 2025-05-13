using Abstracciones.Interfaces.Flujo.Ubicacion;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionFlujo _ubicacionFlujo;
        private readonly ILogger<UbicacionController> _logger;

        public UbicacionController(IUbicacionFlujo ubicacionFlujo, ILogger<UbicacionController> logger)
        {
            _ubicacionFlujo = ubicacionFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _ubicacionFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }
            return Ok(resultado);
        }

        [HttpGet("{idUbicacion}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid idUbicacion)
        {
            var resultado = await _ubicacionFlujo.Obtener(idUbicacion);
            if (resultado == null)
            {
                return NotFound("La ubicación no fue encontrada.");
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] UbicacionRequest ubicacion)
        {
            var resultado = await _ubicacionFlujo.Agregar(ubicacion);
            return CreatedAtAction(nameof(Obtener), new { idUbicacion = resultado }, null);
        }

        [HttpPut("{idUbicacion}")]
        public async Task<IActionResult> Editar([FromRoute] Guid idUbicacion, [FromBody] UbicacionRequest ubicacion)
        {
            // Verificar si la ubicación existe antes de intentar actualizarla
            if (!await VerificarUbicacionExiste(idUbicacion))
                return NotFound("La ubicación no existe.");

            var resultado = await _ubicacionFlujo.Editar(idUbicacion, ubicacion);
            return Ok(resultado);
        }

        [HttpDelete("{idUbicacion}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid idUbicacion)
        {
            if (!await VerificarUbicacionExiste(idUbicacion))
                return NotFound("La ubicación no existe.");
            var resultado = await _ubicacionFlujo.Eliminar(idUbicacion);
            return Ok(resultado);
        }

        #region Helpers
        private async Task<bool> VerificarUbicacionExiste(Guid idUbicacion)
        {
            var resultadoValidacion = false;
            var resultadoUbicacion = await _ubicacionFlujo.Obtener(idUbicacion);
            if (resultadoUbicacion != null)
            {
                resultadoValidacion = true;
            }
            return resultadoValidacion;
        }
        #endregion
    }
}
