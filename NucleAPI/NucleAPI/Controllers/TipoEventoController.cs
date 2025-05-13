using Abstracciones.Interfaces.Flujo.TipoEventos;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEventoFlujo _tipoEventoFlujo;
        private readonly ILogger<TipoEventoController> _logger;

        public TipoEventoController(ITipoEventoFlujo tipoEventoFlujo, ILogger<TipoEventoController> logger)
        {
            _tipoEventoFlujo = tipoEventoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] TipoEventoRequest tipoEvento)
        {
            var resultado = await _tipoEventoFlujo.Agregar(tipoEvento);
            return CreatedAtAction(nameof(Obtener), new { idTipoEvento = resultado }, null);
        }

        [HttpPut("{idTipoEvento}")]
        public async Task<IActionResult> Editar([FromRoute] Guid idTipoEvento, [FromBody] TipoEventoRequest tipoEvento)
        {
            if (!await VerificarTipoEventoExiste(idTipoEvento))
                return NotFound("El tipo de evento no existe");

            var resultado = await _tipoEventoFlujo.Editar(idTipoEvento, tipoEvento);
            return Ok(resultado);
        }

        [HttpDelete("{idTipoEvento}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid idTipoEvento)
        {
            if (!await VerificarTipoEventoExiste(idTipoEvento))
                return NotFound("El tipo de evento no existe");

            var resultado = await _tipoEventoFlujo.Eliminar(idTipoEvento);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _tipoEventoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{idTipoEvento}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid idTipoEvento)
        {
            var resultado = await _tipoEventoFlujo.Obtener(idTipoEvento);
            return Ok(resultado);
        }

        #region Helpers
        private async Task<bool> VerificarTipoEventoExiste(Guid idTipoEvento)
        {
            var resultadoNegocio = await _tipoEventoFlujo.Obtener(idTipoEvento);
            return resultadoNegocio != null;
        }
        #endregion
    }
}
