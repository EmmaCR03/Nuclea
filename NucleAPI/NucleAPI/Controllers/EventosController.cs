using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo.Eventos;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventosController : ControllerBase, IEventosController
    {
        private IEventosFlujo _eventosFlujo;
        private ILogger<EventosController> _logger;

        public EventosController(IEventosFlujo eventosFlujo, ILogger<EventosController> logger)
        {
            _eventosFlujo = eventosFlujo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromForm] EventoRequest eventos)
        {
            try
            {
                if (eventos.ImagenFile != null && eventos.ImagenFile.Length > 0)
                {
                    eventos.ImagenUrl = await GuardarImagen(eventos.ImagenFile);
                }

                var resultado = await _eventosFlujo.Agregar(eventos);
                return CreatedAtAction(nameof(Obtener), new { IdEvento = resultado }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar evento");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpPut("{idEvento}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditarEvento(
        [FromRoute] Guid idEvento,
        [FromForm] EventoRequest eventoRequest)
        {
            if (eventoRequest.ImagenFile != null && eventoRequest.ImagenFile.Length > 0)
            {
                eventoRequest.ImagenUrl = await GuardarImagen(eventoRequest.ImagenFile);
            }

            var resultado = await _eventosFlujo.Editar(idEvento, eventoRequest);
            return Ok(resultado);
        }



        [HttpDelete("{IdEvento}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdEvento)
        {
            if (!await VerificarNegocioExiste(IdEvento))
                return NotFound("El evento no existe");
            var resultado = await _eventosFlujo.Eliminar(IdEvento);
            return Ok(resultado);
        }
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _eventosFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }
            return Ok(resultado);
        }
        [HttpGet("{IdEvento}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid IdEvento)
        {
            var resultado = await _eventosFlujo.Obtener(IdEvento);



            return Ok(resultado);
        }
        #region Helpers
        private async Task<bool> VerificarNegocioExiste(Guid IdEvento)
        {
            var resultadoValidacion = false;
            var resultadoNegocio = await _eventosFlujo.Obtener(IdEvento);
            if (resultadoNegocio != null)
            {
                resultadoValidacion = true;
            }
            return resultadoValidacion;
        }

          
        private async Task<string> GuardarImagen(IFormFile imagenFile)
        {
            // Convertir imagen a base64
            using var memoryStream = new MemoryStream();
            await imagenFile.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            return $"data:{imagenFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
#endregion
