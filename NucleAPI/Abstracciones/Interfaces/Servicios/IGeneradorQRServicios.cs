﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
  public interface IGeneradorQRServicios
    {
        Task<string> GenerarQR(string texto);
    }
}
