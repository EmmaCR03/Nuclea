using Abstracciones.Interfaces.AccesoADatos.Eventos.Registrar;
using Abstracciones.Interfaces.Flujo.Eventos.Registrar;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class RegistrarUsuarioAEventoFlujo : IRegistrarUsuarioAEventoFlujo
    {
        IRegistrarEventoAccesoADatos _registrarUsuarioAEventosAD;
        IGeneradorQRServicios _generadorQRServicios;  // Dependencia del servicio para generar el QR
        IGeneradorPDFService _generadorPDFService;

        public RegistrarUsuarioAEventoFlujo(
            IRegistrarEventoAccesoADatos registrarUsuarioAEventosAD,
            IGeneradorQRServicios generadorQRServicios,
            IGeneradorPDFService generadorPDFService)
        {
            _registrarUsuarioAEventosAD = registrarUsuarioAEventosAD;
            _generadorQRServicios = generadorQRServicios;
            _generadorPDFService = generadorPDFService;
        }

        public async Task<Guid> Agregar(RegistrarEventoRequest evento)
        {
            // Si no se proporciona un QR en la solicitud, lo generamos
            if (string.IsNullOrEmpty(evento.qr))
            {
                var qrGenerado = await _generadorQRServicios.GenerarQR(evento.idEvento.ToString());
                evento.qr = qrGenerado;
            }

            // Registrar el evento en la base de datos
            return await _registrarUsuarioAEventosAD.Agregar(evento);
        }
        public async Task<IEnumerable<RegistrarEventoResponse>> Obtener()
        {
            return await _registrarUsuarioAEventosAD.Obtener();
        }
        public async Task<RegistrarEventoResponse> Obtener(Guid idEventoRegistrado)
        {
            return await _registrarUsuarioAEventosAD.Obtener(idEventoRegistrado);
        }

        public async Task<byte[]> GenerarPDF(Guid idEventoRegistrado)
        {
            var evento = await _registrarUsuarioAEventosAD.Obtener(idEventoRegistrado);
            if (evento == null)
                throw new KeyNotFoundException("Evento no encontrado");

            return await _generadorPDFService.GenerarPDFAsync(evento);
        }
    }
}
