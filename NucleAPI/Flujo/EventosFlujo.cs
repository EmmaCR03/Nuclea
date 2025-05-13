using Abstracciones.Interfaces.AccesoADatos.Eventos;
using Abstracciones.Interfaces.Flujo.Eventos;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo.Eventos
{
    public class EventosFlujo : IEventosFlujo
    {
        IEventosAccesoADatos _eventosAD;

        public EventosFlujo(IEventosAccesoADatos eventosAD) 
        {
            _eventosAD = eventosAD;
        }
        public async Task<Guid> Agregar(EventoRequest evento)
        {
            return await _eventosAD.Agregar(evento);
        }

        public async Task<Guid> Editar(Guid IdEvento, EventoRequest evento)
        {
            return await _eventosAD.Editar(IdEvento, evento);
        }

        public async Task<Guid> Eliminar(Guid IdEvento)
        {
            return await _eventosAD.Eliminar(IdEvento);
        }

        public async Task<IEnumerable<EventoResponse>> Obtener()
        {
            return await _eventosAD.Obtener();
        }

        public async Task<EventoResponse?> Obtener(Guid IdEvento)
        {
            return await _eventosAD.Obtener(IdEvento);

           
        }
    }
}