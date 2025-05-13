using Abstracciones.Interfaces.AccesoADatos.Servicios;
using Abstracciones.Interfaces.Flujo.Servicios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class ServicioFlujo : IServicioFlujo
    {
        IServicioAccesoADatos _servicioAccesoADatos;

        public ServicioFlujo(IServicioAccesoADatos servicio)
        {
            _servicioAccesoADatos = servicio;
        }

        public async Task<Guid> Agregar(ServicioRequest servicio)
        {
            return await _servicioAccesoADatos.Agregar(servicio);
        }
        public async Task<Guid> Editar(Guid IdServicio, ServicioRequest servicio)
        {
            return await _servicioAccesoADatos.Editar(IdServicio, servicio);
        }
        public async Task<Guid> Eliminar(Guid IdServicio)
        {
            return await _servicioAccesoADatos.Eliminar(IdServicio);
        }
        public async Task<IEnumerable<ServicioResponse>> Obtener()
        {
            return await _servicioAccesoADatos.Obtener();
        }
        public async Task<ServicioResponse> Obtener(Guid IdServicio)
        {
            return await _servicioAccesoADatos.Obtener(IdServicio);
        }
    }
}