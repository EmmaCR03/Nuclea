using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.TipoEvento
{
    public interface ITipoEventoAccesoADatos
    {
        Task<Guid> Agregar(TipoEventoRequest tipoEvento);
        Task<Guid> Editar(Guid idTipoEvento, TipoEventoRequest tipoEvento);
        Task<Guid> Eliminar(Guid idTipoEvento);
        Task<TipoEventoResponse> Obtener(Guid idTipoEvento);
        Task<IEnumerable<TipoEventoResponse>> Obtener();
    }
}
