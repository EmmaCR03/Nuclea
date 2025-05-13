using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Ubicacion
{
    namespace Abstracciones.Modelos.Ubicacion
    {
        public class Ubicacion
        {
            [Required(ErrorMessage = "El nombre de la ubicación es obligatorio.")]
            [StringLength(100, ErrorMessage = "El nombre de la ubicación no puede tener más de 100 caracteres.")]
            public string nombreUbicacion { get; set; }

            [Required(ErrorMessage = "La dirección es obligatoria.")]
            [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
            public string direccion { get; set; }

            [Required(ErrorMessage = "La capacidad es obligatoria.")]
            [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser un número positivo.")]
            public int capacidad { get; set; }
        }

        public class UbicacionRequest : Ubicacion
        {
            public Guid idUbicacion { get; set; }
        }

        public class UbicacionResponse : Ubicacion
        {
            public Guid idUbicacion { get; set; }
        }
    }
}
