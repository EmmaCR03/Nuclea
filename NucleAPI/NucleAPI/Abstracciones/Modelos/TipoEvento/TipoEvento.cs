using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.TipoEvento
{
    public class TipoEvento
    {
        public string nombre { get; set; }

    }
    public class TipoEventoResponse : TipoEvento
    {
        public Guid idTipoEvento { get; set; }
    }
}
