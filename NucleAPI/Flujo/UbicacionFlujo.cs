using Abstracciones.Interfaces.AccesoADatos.Ubicacion;
using Abstracciones.Interfaces.Flujo.Ubicacion;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo.Ubicacion
{
    public class UbicacionFlujo : IUbicacionFlujo
    {
        private readonly IUbicacionAccesoADatos _ubicacionAD;

        public UbicacionFlujo(IUbicacionAccesoADatos ubicacionAD)
        {
            _ubicacionAD = ubicacionAD;
        }

        public async Task<Guid> Agregar(UbicacionRequest ubicacion)
        {
            return await _ubicacionAD.Agregar(ubicacion);
        }

        public async Task<Guid> Editar(Guid idUbicacion, UbicacionRequest ubicacion)
        {
            return await _ubicacionAD.Editar(idUbicacion, ubicacion);
        }

        public async Task<Guid> Eliminar(Guid idUbicacion)
        {
            return await _ubicacionAD.Eliminar(idUbicacion);
        }

        public async Task<IEnumerable<UbicacionResponse>> Obtener()
        {
            return await _ubicacionAD.Obtener();
        }

        public async Task<UbicacionResponse?> Obtener(Guid idUbicacion)
        {
            return await _ubicacionAD.Obtener(idUbicacion);
        }
    }
}
