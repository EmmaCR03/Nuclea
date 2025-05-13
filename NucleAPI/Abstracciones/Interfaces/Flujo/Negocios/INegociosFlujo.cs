using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.Negocios
{
    public interface INegociosFlujo
    {
        Task<Guid> Agregar(NegocioRequest negocio);
        Task<Guid> Editar(Guid IdNegocio, NegocioRequest negocio);
        Task<Guid> Eliminar(Guid IdNegocio);
        Task<IEnumerable<NegocioResponse>> Obtener();
        Task<NegocioResponse?> Obtener(Guid IdNegocio);

    }
}
