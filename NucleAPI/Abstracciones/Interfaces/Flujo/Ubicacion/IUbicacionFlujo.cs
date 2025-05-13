using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.Ubicacion
{
    public interface IUbicacionFlujo
    {
        Task<Guid> Agregar(UbicacionRequest ubicacion);
        Task<Guid> Editar(Guid idUbicacion, UbicacionRequest ubicacion);
        Task<Guid> Eliminar(Guid idUbicacion);
        Task<UbicacionResponse> Obtener(Guid idUbicacion);
        Task<IEnumerable<UbicacionResponse>> Obtener();
    }
}
