using Abstracciones.Modelos.Ubicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Ubicacion
{
    public interface IUbicacionAccesoADatos
    {
        Task<IEnumerable<UbicacionResponse>> Obtener();
    }
}
