using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Servicios
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
