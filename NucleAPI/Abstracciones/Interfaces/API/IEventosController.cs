using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IEventosController
    {
        Task<IActionResult> Agregar(EventoRequest evento);
        Task<IActionResult> Eliminar(Guid IdEvento);
        Task<IActionResult> Obtener(Guid IdEvento);
        Task<IActionResult> Obtener();
        Task<IActionResult> EditarEvento(Guid idEvento, EventoRequest eventoRequest);
    }
}
