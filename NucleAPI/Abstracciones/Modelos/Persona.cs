using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Persona
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Estado { get; set; }
    }

    public class PersonaRequest : Persona
    {
        public Guid FkRol { get; set; }
    }

    public class PersonaResponse : Persona
    {
        public Guid IdPersona { get; set; }
    }
}
