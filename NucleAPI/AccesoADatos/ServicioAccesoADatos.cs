using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Interfaces.AccesoADatos.Servicios;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos
{
    public class ServicioAccesoADatos : IServicioAccesoADatos
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ServicioAccesoADatos(IRepositorioDapper repositorio)
        {
            _repositorioDapper = repositorio;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }


        public async Task<Guid> Agregar(ServicioRequest servicio)
        {
            string query = @"AgregarServicio";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                idServicio = Guid.NewGuid(),
                nombreServicio = servicio.nombreServicio,
                descripcion = servicio.descripcion,
                costo = servicio.costo
              
            });
            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid idServicio, ServicioRequest servicio)
        {
            // Verificar si el evento existe antes de intentar actualizarlo
            await VerificarExistenciaServicio(idServicio);

            // La consulta de actualización puede seguir siendo la misma
            string query = @"ActualizarServicio";

            // Ejecutar la consulta con el IdEvento y el resto de parámetros desde el body
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                 idServicio,
                nombreServicio = servicio.nombreServicio,
                descripcion = servicio.descripcion,
                costo = servicio.costo
            });

            return resultadoQuery;
        }


        public async Task<Guid> Eliminar(Guid idServicio)
        {
            await VerificarExistenciaServicio(idServicio);
            string query = @"EliminarServicio";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                idServicio = idServicio


            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ServicioResponse>> Obtener()
        {
            string query = @"ObtenerTodosServicios";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ServicioResponse>(query);

            // No es necesario hacer ninguna conversión, ya que TimeSpan se maneja correctamente en C#
            return resultadoConsulta;
        }

        public async Task<ServicioResponse> Obtener(Guid idServicio)
        {
            string query = @"ObtenerServicioPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ServicioResponse>(query, new { idServicio = idServicio });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task VerificarExistenciaServicio(Guid idServicio)
        {
            ServicioResponse? resultadoConsultaServicio = await Obtener(idServicio);
            if (resultadoConsultaServicio == null)
                throw new Exception("No se encontro el servicio");
        }
    }
}
