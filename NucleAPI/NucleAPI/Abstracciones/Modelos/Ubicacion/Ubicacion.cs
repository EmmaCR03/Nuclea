using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Ubicacion
{
    public class Ubicacion
    {
        public string nombreUbicacion { get; set; }
        public string direccion { get; set; }
        public string capacidad { get; set; }

    }
    public class UbicacionResponse : Ubicacion
    {
        public Guid IdUbicacion { get; set; }
    }
}
