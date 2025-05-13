using Abstracciones.DA;
using Abstracciones.Modelos;
using System.Data.SqlClient;
using Dapper;
using Helpers;
using System.Data;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> CrearUsuario(UsuarioBase usuario)
        {
            var sql = "[AgregarUsuario]";
            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new { NombreUsuario=usuario.NombreUsuario, PasswordHash=usuario.PasswordHash, CorreoElectronico=usuario.CorreoElectronico, Nombre= usuario.Nombre, Apellido= usuario.Apellido });
            return resultado;
        }

        public async Task<IEnumerable<Perfil>> ObtenerPerfilesxUsuario(UsuarioBase usuario)
        {
            string sql = @"[ObtenerPerfilesxUsuario]";
            var resultado = await _sqlConnection.QueryAsync<Perfil>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return resultado;
        }

        public async Task<Usuario> ObtenerUsuario(UsuarioBase usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var resultado = await _sqlConnection.QueryAsync<Usuario>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            string query = @"ObtenerTodosLosUsuarios";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Usuario>(query);

            return resultadoConsulta;
        }

        public async Task<Usuario> ObtenerPorId(Guid Id)
        {
            string sql = @"[ObtenerUsuarioPorId]";
            var resultado = await _sqlConnection.QueryAsync<Usuario>(sql, new { Id = Id });
            return resultado.FirstOrDefault();
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            string query = @"EliminarUsuario";

            await _sqlConnection.ExecuteAsync(query, new
            {
                Id
            }, commandType: CommandType.StoredProcedure);

            return Id;
        }
        public async Task<Guid> Editar(Guid Id, UsuarioRequest usuario)
        {
            string query = @"EditarUsuario";

            await _sqlConnection.ExecuteAsync(query, new
            {
                Id,
                NombreUsuario = usuario.NombreUsuario,
                PasswordHash = usuario.PasswordHash,
                CorreoElectronico = usuario.CorreoElectronico,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido
            }, commandType: CommandType.StoredProcedure);

            return Id;
        }

        public async Task<List<PerfilResponse>> ObtenerTodosPerfiles()
        {
            // Definimos el SQL para la llamada al procedimiento almacenado
            string sql = "ObtenerPerfiles"; // Asegúrate de que este procedimiento esté bien definido

            // Ejecutamos la consulta usando la conexión previamente obtenida
            var resultado = await _sqlConnection.QueryAsync<PerfilResponse>(
                sql,
                commandType: CommandType.StoredProcedure
            );

            // Convertimos el resultado a una lista y lo retornamos
            return resultado.ToList();
        }

        public async Task AsignarPerfilAUsuario(Guid idUsuario, int idPerfil)
        {
            string query = @"AsignarPerfilAUsuario"; // Nombre del procedimiento almacenado

            // Ejecutamos el procedimiento almacenado con los parámetros
            await _sqlConnection.ExecuteAsync(query, new
            {
                IdUsuario = idUsuario, // Parámetro @IdUsuario en el SP
                IdPerfil = idPerfil     // Parámetro @IdPerfil en el SP
            }, commandType: CommandType.StoredProcedure);
        }




    }
}
