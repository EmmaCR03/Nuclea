using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class Configuracion : IConfiguracion 
    {
        private IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre, params object[] args)
        {
            // Obtener la URL base
            string urlBase = _configuration.GetSection(seccion).GetValue<string>("UrlBase");
            if (string.IsNullOrEmpty(urlBase))
            {
                throw new ArgumentException($"La sección '{seccion}' no contiene una URL base válida.");
            }

            // Obtener el valor del método
            var metodo = _configuration.GetSection($"{seccion}:Metodos")
                                       .GetChildren()
                                       .FirstOrDefault(m => m["Nombre"] == nombre)?["Valor"];
            if (string.IsNullOrEmpty(metodo))
            {
                throw new KeyNotFoundException($"No se encontró el método '{nombre}' en la sección '{seccion}'.");
            }

            // Reemplazar placeholders con argumentos
            if (args != null && args.Length > 0)
            {
                metodo = string.Format(metodo, args);
            }

            // Retornar la URL completa
            return $"{urlBase}/{metodo}";
        }

        private string? ObtenerUrlBase(string seccion)
        {
            return _configuration.GetSection(seccion).Get<APIEndPoint>
                            ().UrlBase;
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value;
        }
    }
}

  