﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class PerfilResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
