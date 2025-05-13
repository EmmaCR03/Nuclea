using Abstracciones.Modelos.Persona;

namespace Abstracciones.Interfaces.Flujo.Persona
{
    public interface IPersonaFlujo
    {
        Task<Guid> Agregar(PersonaRequest persona);
        Task<Guid> Editar(Guid IdPersona, PersonaRequest persona);
        Task<Guid> Eliminar(Guid IdPersona);
        Task<IEnumerable<PersonaResponse>> Obtener();
        Task<PersonaResponse> Obtener(Guid IdPersona);
    }
}

