using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Ubicacion
{
    public interface IUbicacionAccesoADatos
    {
        Task<Guid> Agregar(UbicacionRequest ubicacion);
        Task<Guid> Editar(Guid IdUbicacion, UbicacionRequest ubicacion);
        Task<Guid> Eliminar(Guid IdUbicacion);
        Task<UbicacionResponse> Obtener(Guid IdUbicacion);
        Task<IEnumerable<UbicacionResponse>> Obtener();
    }
}
