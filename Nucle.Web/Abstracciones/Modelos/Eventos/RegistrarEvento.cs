using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class RegistrarEvento
    {
        public string qr { get; set; }

    }

    public class RegistrarEventoRequest : RegistrarEvento
    {
        public Guid idEvento { get; set; }
        public Guid idUsuario { get; set; }

    }
    public class RegistrarEventoResponse : RegistrarEvento
    {
        public Guid idEventoRegistrado { get; set; }
        public Guid idUsuario { get; set; }
        public Guid idEvento { get; set; }
        public string nombreEvento { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFin { get; set; }
        public string descripcion { get; set; }
        public string nombreUsuario { get; set; } // Nombre del Usuario asociado
        public string nombreUbicacion { get; set; }

    }
}
