using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Evento
    {
        public string nombreEvento { get; set; }

        public DateTime fecha { get; set; }

        public TimeSpan horaInicio { get; set; }

        public TimeSpan horaFin { get; set; }

        public string descripcion { get; set; }

        public string? ImagenUrl { get; set; }
    }

    public class EventoRequest : Evento
    {
        public Guid FkTipoEvento { get; set; }

        public Guid FkUbicacion { get; set; }

        public Guid FkNegocio { get; set; }
        public Guid FkServicios { get; set; }    
        public IFormFile? ImagenFile { get; set; }


    }

    public class EventoResponse : Evento
    {
        public Guid IdEvento { get; set; }

        public string TipoEvento { get; set; }

        public string Ubicacion { get; set; }

        public string Negocio { get; set; }

        public string Servicios { get; set; }
    }
}
