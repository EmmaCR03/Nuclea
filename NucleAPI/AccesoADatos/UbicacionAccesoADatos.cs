using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Interfaces.AccesoADatos.Ubicacion;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.Ubicacion
{
    public class UbicacionAccesoADatos : IUbicacionAccesoADatos
    {
        private readonly IRepositorioDapper _repositorio;
        private readonly SqlConnection _conexion;

        public UbicacionAccesoADatos(IRepositorioDapper repositorio)
        {
            _repositorio = repositorio;
            _conexion = _repositorio.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(UbicacionRequest ubicacion)
        {
            string query = @"AgregarUbicacion";
            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                idUbicacion = Guid.NewGuid(),
                nombreUbicacion = ubicacion.nombreUbicacion,
                direccion = ubicacion.direccion,
                capacidad = ubicacion.capacidad
            });

            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid idUbicacion, UbicacionRequest ubicacion)
        {
            await VerificarExistenciaUbicacion(idUbicacion);

            string query = @"ActualizarUbicacion";

            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                idUbicacion = idUbicacion,
                nombreUbicacion = ubicacion.nombreUbicacion,
                direccion = ubicacion.direccion,
                capacidad = ubicacion.capacidad
            });

            return resultadoQuery;
        }

        public async Task<Guid> Eliminar(Guid idUbicacion)
        {
            await VerificarExistenciaUbicacion(idUbicacion);

            string query = @"EliminarUbicacion";
            var resultadoConsulta = await _conexion.ExecuteScalarAsync<Guid>(query, new
            {
                idUbicacion = idUbicacion
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<UbicacionResponse>> Obtener()
        {
            string query = @"ObtenerTodasUbicaciones";
            var resultadoConsulta = await _conexion.QueryAsync<UbicacionResponse>(query);
            return resultadoConsulta;
        }

        public async Task<UbicacionResponse> Obtener(Guid idUbicacion)
        {
            string query = @"ObtenerUbicacionPorId";
            var resultadoConsulta = await _conexion.QueryAsync<UbicacionResponse>(query, new { idUbicacion = idUbicacion });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task VerificarExistenciaUbicacion(Guid idUbicacion)
        {
            UbicacionResponse? resultadoConsultaUbicacion = await Obtener(idUbicacion);
            if (resultadoConsultaUbicacion == null)
                throw new Exception("No se encontró la ubicación");
        }
    }
}


