using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Servicios
{
    public interface IServicioAccesoADatos
    {
        Task<Guid> Agregar(ServicioRequest servicio);
        Task<Guid> Editar(Guid IdServicio, ServicioRequest servicio);
        Task<Guid> Eliminar(Guid IdServicio);
        Task<IEnumerable<ServicioResponse>> Obtener();
        Task<ServicioResponse> Obtener(Guid IdServicio);
    }
}
