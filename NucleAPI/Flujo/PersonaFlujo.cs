using Abstracciones.Interfaces.AccesoADatos.Persona;
using Abstracciones.Interfaces.Flujo.Persona;
using Abstracciones.Modelos.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    namespace Flujo.Persona
    {
        public class PersonaFlujo : IPersonaFlujo
        {
            private readonly IPersonaAccesoADatos _personaAD;

            public PersonaFlujo(IPersonaAccesoADatos personaAD)
            {
                _personaAD = personaAD;
            }

            public async Task<Guid> Agregar(PersonaRequest persona)
            {
                return await _personaAD.Agregar(persona);
            }

            public async Task<Guid> Editar(Guid idPersona, PersonaRequest persona)
            {
                return await _personaAD.Editar(idPersona, persona);
            }

            public async Task<Guid> Eliminar(Guid idPersona)
            {
                return await _personaAD.Eliminar(idPersona);
            }

            public async Task<IEnumerable<PersonaResponse>> Obtener()
            {
                return await _personaAD.Obtener();
            }

            public async Task<PersonaResponse?> Obtener(Guid idPersona)
            {
                return await _personaAD.Obtener(idPersona);
            }
        }
    }
}
