using Abstracciones.Modelos.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.AccesoADatos.Persona
{
    public interface IPersonaAccesoADatos
    {
        Task<Guid> Agregar(PersonaRequest persona);
        Task<Guid> Editar(Guid IdPersona, PersonaRequest persona);
        Task<Guid> Eliminar(Guid IdPersona);
        Task<IEnumerable<PersonaResponse>> Obtener();
        Task<PersonaResponse> Obtener(Guid IdPersona);
    }
}

