using Autorizacion.Abstracciones.DA;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizacion.AccesoADatos.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public RepositorioDapper(IConfiguration configuration, SqlConnection connection) 
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("BDSeguridad"));

        }
        public SqlConnection ObtenerRepositorioDapper()
        {
            return _connection;
        }
    }
}
