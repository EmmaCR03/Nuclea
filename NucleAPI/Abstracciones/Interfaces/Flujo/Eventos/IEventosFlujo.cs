using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.Eventos
{
    public interface IEventosFlujo
    {
        Task<Guid> Agregar(EventoRequest evento);
        Task<Guid> Editar(Guid IdEvento, EventoRequest evento);
        Task<Guid> Eliminar(Guid IdEvento);
        Task<EventoResponse> Obtener(Guid IdEvento);
        Task<IEnumerable<EventoResponse>> Obtener();
    }
}
