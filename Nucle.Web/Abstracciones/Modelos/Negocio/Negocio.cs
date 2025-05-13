using Abstracciones.Modelos.Negocio;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Negocio
{
    public class NegocioBase{

    public string nombre { get; set; }

    public string descripcion { get; set; }

    public string? ImagenUrl { get; set; }
}

public class NegocioRequest : NegocioBase
{
    public Guid idNegocio { get; set; }

    }

    public class NegocioResponse : NegocioBase
{
    public Guid idNegocio { get; set; }
}
}
