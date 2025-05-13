using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.TipoEvento
{
    public class TipoEvento
    {
        [Required(ErrorMessage = "El nombre del tipo de evento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del tipo de evento no puede tener más de 100 caracteres.")]
        public string nombre { get; set; }
    }

    public class TipoEventoRequest : TipoEvento
    {
        public Guid idTipoEvento { get; set; }
    }

    public class TipoEventoResponse : TipoEvento
    {
        public Guid idTipoEvento { get; set; }
    }
}