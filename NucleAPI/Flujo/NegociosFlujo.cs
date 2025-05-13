using Abstracciones.Interfaces.AccesoADatos.Negocios;
using Abstracciones.Interfaces.Flujo.Negocios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo.Negocio
{
    public class NegociosFlujo : INegociosFlujo
    {
        INegocioAccesoADatos _negociosAD;

        public NegociosFlujo(INegocioAccesoADatos negocios) 
        {
            _negociosAD = negocios;
        }

        public async Task<Guid> Agregar(NegocioRequest negocio)
        {
            return await _negociosAD.Agregar(negocio);
        }

        public async Task<Guid> Editar(Guid IdNegocio, NegocioRequest negocio)
        {
            return await _negociosAD.Editar(IdNegocio, negocio);
        }

        public async Task<Guid> Eliminar(Guid IdNegocio)
        {
            return await _negociosAD.Eliminar(IdNegocio);
        }

        public async Task<IEnumerable<NegocioResponse>> Obtener()
        {
            return await _negociosAD.Obtener();
        }

        public async Task<NegocioResponse?> Obtener(Guid IdNegocio)
        {
            return await _negociosAD.Obtener(IdNegocio);


        }
    }
}