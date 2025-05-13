using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Modelos;
using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Autorizacion.AccesoADatos
{
    public class SeguridadAccesoADatos : ISeguridadDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;
        public SeguridadAccesoADatos(IRepositorioDapper repositorio, SqlConnection sqlConection)
        {
            _repositorioDapper = repositorio;
            _sqlConnection = repositorio.ObtenerRepositorioDapper();
        }

        public async Task<IEnumerable<Perfil>> ObtenerPerfilesXUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerPerfilesPorUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Perfil>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.ConvertirLista<Abstracciones.Entidades.Perfil, Abstracciones.Modelos.Perfil>(consulta);
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var resultado = await _sqlConnection.QueryAsync<Usuario>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return resultado.FirstOrDefault();
        }
    }
}