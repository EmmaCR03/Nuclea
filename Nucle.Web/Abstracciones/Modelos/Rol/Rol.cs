using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Rol
{
    public class Rol
    {
        public string descripcion { get; set; }

    }
    public class RolRequest : Rol
    {

    }
    public class RolResponse : Rol
    {
        public Guid idRol { get; set; }
    }
}
