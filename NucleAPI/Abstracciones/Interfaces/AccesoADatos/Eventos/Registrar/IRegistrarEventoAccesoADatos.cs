using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Eventos.Registrar
{
   public interface IRegistrarEventoAccesoADatos
    {
        Task<Guid> Agregar(RegistrarEventoRequest evento);
        Task<IEnumerable<RegistrarEventoResponse>> Obtener();
        Task<RegistrarEventoResponse> Obtener(Guid idEventoRegistrado);

    }
}
