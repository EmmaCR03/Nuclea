using Abstracciones.Flujo;
using Abstracciones.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioBase usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Usuario> ObtenerUsuario(UsuarioBase usuario)
        {
            return await _usuarioDA.ObtenerUsuario(usuario);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioDA.ObtenerUsuarios();
        }

        public async Task<Usuario> ObtenerPorId(Guid Id)
        {
            return await _usuarioDA.ObtenerPorId(Id);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _usuarioDA.Eliminar(Id);
        }

        public async Task<List<PerfilResponse>> ObtenerTodosPerfiles()
        {
            // Aquí obtenemos todos los perfiles disponibles desde la base de datos
            return await _usuarioDA.ObtenerTodosPerfiles();
        }

        public async Task AsignarPerfilAUsuario(Guid idUsuario, int idPerfil)
        {
            // Llamada al método de la capa DA
            await _usuarioDA.AsignarPerfilAUsuario(idUsuario, idPerfil);
        }
    }
}
