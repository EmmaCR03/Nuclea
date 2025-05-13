using Abstracciones.Modelos.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.Ubicacion
{
    public interface IUbicacionFlujo
    {
        Task<IEnumerable<UbicacionResponse>> Obtener();
    }
}
