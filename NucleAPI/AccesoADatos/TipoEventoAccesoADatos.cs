using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Interfaces.AccesoADatos.TipoEvento;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.TipoEvento
{
    public class TipoEventoAccesoADatos : ITipoEventoAccesoADatos
    {
        IRepositorioDapper _repositorio;
        SqlConnection _conexion;

        public TipoEventoAccesoADatos(IRepositorioDapper repositorio)
        {
            _repositorio = repositorio;
            _conexion = _repositorio.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(TipoEventoRequest tipoEvento)
        {
            string query = @"AgregarTipoEvento";
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                IdTipoEvento = Guid.NewGuid(),
                nombre = tipoEvento.nombre
            });
            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid idTipoEvento, TipoEventoRequest tipoEvento)
        {
            await VerificarExistenciaTipoEvento(idTipoEvento);

            string query = @"ActualizarTipoEvento";
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                IdTipoEvento = idTipoEvento,
                nombre = tipoEvento.nombre
            });

            return resultadoQuery;

        }

        public async Task<Guid> Eliminar(Guid idTipoEvento)
        {
            await VerificarExistenciaTipoEvento(idTipoEvento);
            string query = @"EliminarTipoEvento";
            var resultadoConsulta = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                IdTipoEvento = idTipoEvento
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<TipoEventoResponse>> Obtener()
        {
            string query = @"ObtenerTodosTiposEvento";
            var resultadoConsulta = await _conexion.QueryAsync<TipoEventoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<TipoEventoResponse> Obtener(Guid idTipoEvento)
        {
            string query = @"ObtenerTipoEventoPorId";
            var resultadoConsulta = await _conexion.QueryAsync<TipoEventoResponse>(query, new { idTipoEvento = idTipoEvento });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task VerificarExistenciaTipoEvento(Guid idTipoEvento)
        {
            TipoEventoResponse? resultadoConsultaTipoEvento = await Obtener(idTipoEvento);
            if (resultadoConsultaTipoEvento == null)
                throw new Exception("No se encontró el tipo de evento");
        }
    }
}


