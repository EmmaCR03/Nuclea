using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Negocio
    {

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public string? ImagenUrl { get; set; }
    }

    public class NegocioRequest : Negocio
    {
        public IFormFile? ImagenFile { get; set; } // Para recibir el archivo
    }

    public class NegocioResponse : Negocio
    {
        public Guid idNegocio { get; set; }
    }
}
