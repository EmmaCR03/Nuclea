using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ServicioBase
    {
        public string nombreServicio { get; set; }
        public string descripcion { get; set; }
        public decimal costo { get; set; }

    }

    public class ServicioRequest : ServicioBase
    {
    }

    public class ServicioResponse : ServicioBase
    {
        public Guid idServicio { get; set; }

    }
}
