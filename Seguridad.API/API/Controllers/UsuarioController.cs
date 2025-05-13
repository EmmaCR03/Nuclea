using Abstracciones.API;
using Abstracciones.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller, IUsuarioController
    {
        private IUsuarioFlujo _usuarioFlujo;

        public UsuarioController(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }

        [Authorize(Roles = "1")]
        [HttpPost("ObtenerInformacionUsuario")]
        public async Task<IActionResult> ObtenerUsuario([FromBody] UsuarioBase usuario)
        {
            return Ok(await _usuarioFlujo.ObtenerUsuario(usuario));
        }

        [AllowAnonymous]
        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> PostAsync([FromBody] UsuarioBase usuario)
        {
            var resultado = await _usuarioFlujo.CrearUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuario), null, resultado);
        }

        [AllowAnonymous]
        [HttpGet("TodosLosUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var resultado = await _usuarioFlujo.ObtenerUsuarios();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(Guid Id)
        {
            var resultado = await _usuarioFlujo.ObtenerPorId(Id);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);


        }
        [AllowAnonymous]
        [HttpDelete("Eliminar/{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _usuarioFlujo.Eliminar(Id);
            if (resultado == Guid.Empty)
                return NotFound();

            return Ok(resultado);
        }
        [AllowAnonymous]
        [HttpGet("perfiles")]
        public async Task<ActionResult<List<PerfilResponse>>> ObtenerPerfiles()
        {
            var perfiles = await _usuarioFlujo.ObtenerTodosPerfiles();
            if (perfiles == null || !perfiles.Any())
            {
                return NotFound("No se encontraron perfiles.");
            }

            return Ok(perfiles);
        }
        [AllowAnonymous]
        [HttpPost("asignar-perfil")]
        public async Task<ActionResult> AsignarPerfil([FromBody] PerfilxUsuario perfilxUsuario)

        {
            if (perfilxUsuario == null || perfilxUsuario.IdUsuario == Guid.Empty || perfilxUsuario.IdPerfil <= 0)

            {
                return BadRequest("Los datos proporcionados no son válidos.");
            }

            try
            {
                // Llamar al flujo o a la lógica de la base de datos para asignar el perfil
                await _usuarioFlujo.AsignarPerfilAUsuario(perfilxUsuario.IdUsuario, perfilxUsuario.IdPerfil);
                return Ok("Perfil asignado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al asignar el perfil: {ex.Message}");
            }
        }


    }
}
