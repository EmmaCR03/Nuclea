using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IConfiguracion
    {
        string ObtenerMetodo(string seccion, string nombre, params object[] args); 
        public string ObtenerValor(string llave);
    }
}
