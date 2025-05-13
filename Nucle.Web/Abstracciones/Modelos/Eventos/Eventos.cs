using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Eventos
{
    public class EventosBase
    {

        public string nombreEvento { get; set; }

        public DateTime fecha { get; set; }

        public TimeSpan horaInicio { get; set; }

        public TimeSpan horaFin { get; set; }

        public string descripcion { get; set; }

        public string? ImagenUrl { get; set; }

        public IFormFile? ImagenFile { get; set; }
    }

    public class EventoRequest : EventosBase
    {
        public Guid IdEvento { get; set; }

        public Guid fkTipoEvento { get; set; }

        public Guid fkUbicacion { get; set; }

        public Guid fkNegocio { get; set; }

        public Guid fkServicios { get; set; }
    }

    public class EventoResponse : EventosBase
    {
        public Guid IdEvento { get; set; }

        public string TipoEvento { get; set; }

        public string Ubicacion { get; set; }

        public string Negocio { get; set; }

        public string Servicios { get; set; }
    }

}
