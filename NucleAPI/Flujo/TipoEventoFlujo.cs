using Abstracciones.Interfaces.AccesoADatos.TipoEvento;
using Abstracciones.Interfaces.Flujo.TipoEventos;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo.TipoEvento
{
    public class TipoEventoFlujo : ITipoEventoFlujo
    {
        ITipoEventoAccesoADatos _tipoEventoAD;

        public TipoEventoFlujo(ITipoEventoAccesoADatos tipoEventoAD)
        {
            _tipoEventoAD = tipoEventoAD;
        }

        public async Task<Guid> Agregar(TipoEventoRequest tipoEvento)
        {
            return await _tipoEventoAD.Agregar(tipoEvento);
        }

        public async Task<Guid> Editar(Guid idTipoEvento, TipoEventoRequest tipoEvento)
        {
            return await _tipoEventoAD.Editar(idTipoEvento, tipoEvento);
        }

        public async Task<Guid> Eliminar(Guid idTipoEvento)
        {
            return await _tipoEventoAD.Eliminar(idTipoEvento);
        }

        public async Task<IEnumerable<TipoEventoResponse>> Obtener()
        {
            return await _tipoEventoAD.Obtener();
        }

        public async Task<TipoEventoResponse?> Obtener(Guid idTipoEvento)
        {
            return await _tipoEventoAD.Obtener(idTipoEvento);
        }
    }
}