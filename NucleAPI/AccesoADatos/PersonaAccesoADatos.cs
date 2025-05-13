

using Abstracciones.Interfaces.AccesoADatos;
using Abstracciones.Interfaces.AccesoADatos.Persona;
using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AccesoADatos
{
    public class PersonaAccesoADatos : IPersonaAccesoADatos
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PersonaAccesoADatos(IRepositorioDapper repositorio)
        {
            _repositorioDapper = repositorio;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(PersonaRequest persona)
        {
            string query = @"AgregarPersona";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdPersona = Guid.NewGuid(),
                nombre = persona.Nombre,
                apellido = persona.Apellido,
                estado = persona.Estado,
                fkRol = persona.FkRol,
            });
            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid IdPersona, PersonaRequest persona)
        {
            await VerificarPersonaExiste(IdPersona);
            string query = @"ActualizarPersona";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdPersona = IdPersona,  
                nombre = persona.Nombre,
                apellido = persona.Apellido,
                fkRol = persona.FkRol,
                estado = persona.Estado
            });

            return resultadoQuery;
        }

        public async Task<Guid> Eliminar(Guid IdPersona)
        {
            // Verificar si la persona existe antes de intentar eliminarla
            await VerificarPersonaExiste(IdPersona);

            // Ejecutar el procedimiento almacenado para eliminar a la persona
            string query = @"EliminarPersona";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                idPersona = IdPersona
            });

            // Si el resultado es Guid.Empty, significa que no se pudo eliminar
            if (resultadoConsulta == Guid.Empty)
                throw new Exception("Error al eliminar la persona");

            // Devolver el IdPersona si la eliminación fue exitosa
            return IdPersona;
        }



        public async Task<IEnumerable<PersonaResponse>> Obtener()
        {
            string query = @"ObtenerPersonas";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PersonaResponse>(query);

            return resultadoConsulta;
        }


        public async Task<PersonaResponse> Obtener(Guid IdPersona)
        {
            string query = @"ObtenerPersonaPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PersonaResponse>(query,
                new { idPersona = IdPersona });
            return resultadoConsulta.FirstOrDefault();
        }


        private async Task VerificarPersonaExiste(Guid IdPersona)
        {
            PersonaResponse? resultadoConsultaPersona = await Obtener(IdPersona);
            if (resultadoConsultaPersona == null)
                throw new Exception("No se encontro la persona");
        }
    }
}
