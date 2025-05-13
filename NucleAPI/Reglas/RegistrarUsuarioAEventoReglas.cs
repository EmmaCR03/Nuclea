//using Abstracciones.Interfaces.Flujo.Eventos.Registrar;
//using Abstracciones.Modelos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Reglas
//{
//   public class RegistrarUsuarioAEventoReglas : IRegistrarUsuarioAEventoReglas
//    {
//        IRegistrarUsuarioAEventoFlujo _registrarUsuarioAEvento;

//        public RegistrarUsuarioAEventoReglas(IRegistrarUsuarioAEventoFlujo registrarUsuarioAEvento)
//        {
//            _registrarUsuarioAEvento = registrarUsuarioAEvento;
//        }
//        public async Task<Guid> Agregar(RegistrarEventoRequest evento)
//        {
//            // Validar el evento
//            if (evento == null)
//            {
//                throw new ArgumentNullException(nameof(evento), "El evento no puede ser nulo.");
//            }
//            if (evento.idEvento == Guid.Empty)
//            {
//                throw new ArgumentException("El id del evento no puede ser vacío.", nameof(evento.idEvento));
//            }
//            if (evento.idUsuario == Guid.Empty)
//            {
//                throw new ArgumentException("El id del usuario no puede ser vacío.", nameof(evento.idUsuario));
//            }
//            // Llamar al flujo para registrar el evento
//            return await _registrarUsuarioAEvento.Agregar(evento);
//        }
//    }
//}
