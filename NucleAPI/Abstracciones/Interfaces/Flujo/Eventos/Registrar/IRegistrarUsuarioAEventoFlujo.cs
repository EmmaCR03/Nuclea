using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo.Eventos.Registrar
{
    public interface IRegistrarUsuarioAEventoFlujo
    {
        Task<Guid> Agregar(RegistrarEventoRequest evento);
        Task<IEnumerable<RegistrarEventoResponse>> Obtener();
        Task<RegistrarEventoResponse> Obtener(Guid idEventoRegistrado);
        Task<byte[]> GenerarPDF(Guid idEventoRegistrado);

    }
}
