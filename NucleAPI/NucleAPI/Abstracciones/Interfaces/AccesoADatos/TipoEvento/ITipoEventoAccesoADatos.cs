using Abstracciones.Modelos.TipoEvento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.TipoEvento
{
    public interface ITipoEventoAccesoADatos
    {
        Task<IEnumerable<TipoEventoResponse>> Obtener();
    }
}
