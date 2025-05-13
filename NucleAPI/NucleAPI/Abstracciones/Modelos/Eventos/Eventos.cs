using System;
using System.Collections.Generic;
using System.Linq;
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

    }

    public class EventoRequest : EventosBase
    {
        public Guid fkTipoEvento { get; set; }
        public Guid fkUbicacion { get; set; }
    }

    public class EventoResponse : EventosBase
    {
        public Guid IdEvento { get; set; }
        public string TipoEvento { get; set; }
        public string Ubicacion { get; set; }
    }
}
