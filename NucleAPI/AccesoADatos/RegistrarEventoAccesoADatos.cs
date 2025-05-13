using Abstracciones.Interfaces.AccesoADatos.Eventos.Registrar;
using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
  public   class RegistrarEventoAccesoADatos : IRegistrarEventoAccesoADatos
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;
        public RegistrarEventoAccesoADatos(IRepositorioDapper repositorio)
        {
            _repositorioDapper = repositorio;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        public async Task<Guid> Agregar(RegistrarEventoRequest evento)
        {
            string query = "RegistrarUsuarioAEvento"; // Nombre del procedimiento almacenado
            var parametros = new
            {
                idEventoRegistrado = Guid.NewGuid(), 
                idEvento = evento.idEvento, 
                qr = evento.qr,
                idUsuario = evento.idUsuario
            };
            // Ejecutar el procedimiento almacenado
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, parametros, commandType: CommandType.StoredProcedure);
            return resultadoQuery;
        }

        public async Task<IEnumerable<RegistrarEventoResponse>> Obtener()
        {
            string query = @"ObtenerTodosLosEventosRegistrados";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RegistrarEventoResponse>(query);

            // No es necesario hacer ninguna conversión, ya que TimeSpan se maneja correctamente en C#
            return resultadoConsulta;
        }

        public async Task<RegistrarEventoResponse> Obtener(Guid idEventoRegistrado)
        {
            string query = @"ObtenerEventoRegistradoPorID";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RegistrarEventoResponse>(query, new { idEventoRegistrado = idEventoRegistrado});
            return resultadoConsulta.FirstOrDefault();
        }

    }
}
