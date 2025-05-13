using Abstracciones.Modelos.TipoEvento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.TipoEvento
{
    public interface ITipoEventoFlujo
    {
        Task<IEnumerable<TipoEventoResponse>> Obtener();
    }
}
