using Abstracciones.Modelos.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Eventos
{
    public interface IEventosAccesoADatos
    {
        Task<Guid> Agregar(EventoRequest evento);
        Task<Guid> Editar(Guid IdEvento, EventoRequest evento);
        Task<Guid> Eliminar(Guid IdEvento);
        Task<IEnumerable<EventoResponse>> Obtener();
        Task<EventoResponse> Obtener(Guid IdEvento);

    }
}
