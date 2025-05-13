using Abstracciones.Interfaces;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            var urlBase = ObtenerUrlBase(seccion);
            if (urlBase == null)
            {
                throw new ArgumentException($"Configuration section '{seccion}' not found or is invalid");
            }

            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();
            if (apiEndPoint?.Metodos == null)
            {
                throw new ArgumentException($"No methods found in configuration section '{seccion}'");
            }

            var metodo = apiEndPoint.Metodos.FirstOrDefault(m => m.Nombre == nombre);
            if (metodo == null)
            {
                throw new ArgumentException($"Method '{nombre}' not found in configuration section '{seccion}'");
            }

            return $"{urlBase}/{metodo.Valor}";
        }

        private string ObtenerUrlBase(string seccion)
        {
            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();
            if (apiEndPoint?.UrlBase == null)
            {
                throw new ArgumentException($"UrlBase not found in configuration section '{seccion}'");
            }
            return apiEndPoint.UrlBase;
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value;
        }
    }
}


