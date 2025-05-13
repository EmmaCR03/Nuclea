using Abstracciones.Interfaces.Flujo.Persona;
using Abstracciones.Modelos.Persona;
using Microsoft.AspNetCore.Mvc;

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaFlujo _personaFlujo;
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(IPersonaFlujo personaFlujo, ILogger<PersonaController> logger)
        {
            _personaFlujo = personaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] PersonaRequest persona)
        {
            var resultado = await _personaFlujo.Agregar(persona);
            return CreatedAtAction(nameof(Obtener), new { idPersona = resultado }, null);
        }

        [HttpPut("{idPersona}")]
        public async Task<IActionResult> Editar([FromRoute] Guid idPersona, [FromBody] PersonaRequest persona)
        {
            if (!await VerificarPersonaExiste(idPersona))
                return NotFound("La persona no existe");

            var resultado = await _personaFlujo.Editar(idPersona, persona);
            return Ok(resultado);
        }

        [HttpDelete("{idPersona}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid idPersona)
        {
            if (!await VerificarPersonaExiste(idPersona))
                return NotFound("La persona no existe");

            var resultado = await _personaFlujo.Eliminar(idPersona);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _personaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{idPersona}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid idPersona)
        {
            var resultado = await _personaFlujo.Obtener(idPersona);
            return Ok(resultado);
        }

        private async Task<bool> VerificarPersonaExiste(Guid idPersona)
        {
            var resultadoPersona = await _personaFlujo.Obtener(idPersona);
            return resultadoPersona != null;
        }
    }
}
