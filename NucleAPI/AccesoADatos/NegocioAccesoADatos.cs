using Abstracciones.Interfaces.AccesoADatos.Negocios;
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

namespace AccesoADatos.Negocio
{
    public class NegocioAccesoADatos : INegocioAccesoADatos
    { 
    private readonly IRepositorioDapper _repositorioDapper;
    private readonly SqlConnection _sqlConnection;

    public NegocioAccesoADatos(IRepositorioDapper repositorio)
    {
        _repositorioDapper = repositorio;
        _sqlConnection = _repositorioDapper.ObtenerRepositorio();
    }


        public async Task<Guid> Agregar(NegocioRequest negocio)
        {
            string query = @"AgregarNegocio";
            var parametros = new
            {
                idNegocio = Guid.NewGuid(),
                nombre = negocio.nombre,
                descripcion = negocio.descripcion,
                ImagenUrl = negocio.ImagenUrl // Agregar imagen
            };

            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, parametros, commandType: CommandType.StoredProcedure);
            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid IdNegocio, NegocioRequest negocio)
        {
            await VerificarExistenciaNegocio(IdNegocio);

            string query = @"ActualizarNegocio";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdNegocio,
                nombre = negocio.nombre,
                descripcion = negocio.descripcion,
                ImagenUrl = negocio.ImagenUrl // Agregar imagen
            });

            return resultadoQuery;
        }


        public async Task<Guid> Eliminar(Guid IdNegocio)
    {
        await VerificarExistenciaNegocio(IdNegocio);
        string query = @"EliminarNegocioPorId";
        var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
        {
            IdNegocio = IdNegocio


        });
        return resultadoConsulta;
    }

    public async Task<IEnumerable<NegocioResponse>> Obtener()
    {
        string query = @"ObtenerTodosNegocios";
        var resultadoConsulta = await _sqlConnection.QueryAsync<NegocioResponse>(query);

        // No es necesario hacer ninguna conversión, ya que TimeSpan se maneja correctamente en C#
        return resultadoConsulta;
    }

    public async Task<NegocioResponse> Obtener(Guid IdNegocio)
    {
        string query = @"ObtenerNegocioPorId";
        var resultadoConsulta = await _sqlConnection.QueryAsync<NegocioResponse>(query, new { IdNegocio = IdNegocio });
        return resultadoConsulta.FirstOrDefault();
    }

    private async Task VerificarExistenciaNegocio(Guid IdNegocio)
    {
            NegocioResponse? resultadoConsultaEvento = await Obtener(IdNegocio);
        if (resultadoConsultaEvento == null)
            throw new Exception("No se encontro el evento");
    }
}
}

