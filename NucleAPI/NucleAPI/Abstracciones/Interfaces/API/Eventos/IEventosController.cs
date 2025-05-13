using Abstracciones.Modelos.Eventos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API.Eventos
{
    public interface IEventosController
    {
        Task<IActionResult> Agregar(EventoRequest eventos);
        Task<IActionResult> Editar( Guid IdEventos, EventoRequest evento);
        Task<IActionResult> Eliminar(Guid IdEvento);
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener( Guid IdVehiculo);
    }
}
