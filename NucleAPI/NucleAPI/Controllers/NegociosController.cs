using Abstracciones.Interfaces.Flujo.Eventos;
using Abstracciones.Interfaces.Flujo.Negocios;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NucleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegociosController : ControllerBase
    {
        private INegociosFlujo _negociosFlujo;
        private ILogger<EventosController> _logger;

        public NegociosController(INegociosFlujo negociosFlujo, ILogger<EventosController> logger)
        {
            _negociosFlujo = negociosFlujo;
            _logger = logger;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Agregar([FromForm] NegocioRequest negocio)
        {
            try
            {
                if (negocio.ImagenFile != null && negocio.ImagenFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await negocio.ImagenFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    negocio.ImagenUrl = $"data:{negocio.ImagenFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                }

                var resultado = await _negociosFlujo.Agregar(negocio);
                return CreatedAtAction(nameof(Obtener), new { idNegocio = resultado }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar negocio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpPut("{IdNegocio}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Editar(
      [FromRoute] Guid IdNegocio,
      [FromForm] NegocioRequest negocio)
        {
            try
            {
                // Verificar existencia del negocio
                if (!await VerificarNegocioExiste(IdNegocio))
                    return NotFound("El negocio no existe");

                // Procesar imagen si se envió una nueva
                if (negocio.ImagenFile != null && negocio.ImagenFile.Length > 0)
                {
                    negocio.ImagenUrl = await GuardarImagen(negocio.ImagenFile);
                }
                else if (string.IsNullOrEmpty(negocio.ImagenUrl))
                {
                    // Mantener la imagen existente si no se envió una nueva
                    var negocioExistente = await _negociosFlujo.Obtener(IdNegocio);
                    if (negocioExistente != null)
                    {
                        negocio.ImagenUrl = negocioExistente.ImagenUrl;
                    }
                }

                // Validar modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Actualizar negocio
                var resultado = await _negociosFlujo.Editar(IdNegocio, negocio);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Loggear el error
                _logger.LogError(ex, "Error al editar negocio {IdNegocio}", IdNegocio);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [HttpDelete("{IdNegocio}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdNegocio)
        {
            if (!await VerificarNegocioExiste(IdNegocio))
                return NotFound("El negocio no existe");
            var resultado = await _negociosFlujo.Eliminar(IdNegocio);
            return Ok(resultado);
        }
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _negociosFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }
            return Ok(resultado);
        }
        [HttpGet("{IdNegocio}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid IdNegocio)
        {
            var resultado = await _negociosFlujo.Obtener(IdNegocio);



            return Ok(resultado);
        }
        #region Helpers
        private async Task<bool> VerificarNegocioExiste(Guid IdNegocio)
        {
            var resultadoValidacion = false;
            var resultadoNegocio = await _negociosFlujo.Obtener(IdNegocio);
            if (resultadoNegocio != null)
            {
                resultadoValidacion = true;
            }
            return resultadoValidacion;
            #endregion
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
