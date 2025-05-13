using Abstracciones.Interfaces.AccesoADatos.Eventos;
using Abstracciones.Interfaces.AccesoADatos.Repositorio;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AccesoADatos
{
    public class EventosAccesoADatos : IEventosAccesoADatos
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public EventosAccesoADatos(IRepositorioDapper repositorio) 
        {
            _repositorioDapper = repositorio;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio(); 
        }


        public async Task<Guid> Agregar(EventoRequest evento) 
        {
            string query = "AgregarEvento";
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new { 
            IdEvento = Guid.NewGuid(),
            nombreEvento = evento.nombreEvento,
            descripcion = evento.descripcion,
            fecha = evento.fecha,
            horaInicio = evento.horaInicio,
            horaFin = evento.horaFin,
            fkTipoEvento = evento.FkTipoEvento,
            fkUbicacion = evento.FkUbicacion,
            fkNegocio = evento.FkNegocio,
            fkServicios = evento.FkServicios,
            ImagenUrl = evento.ImagenUrl
            });
            return resultadoQuery;
        }

        public async Task<Guid> Editar(Guid IdEvento, EventoRequest evento)
        {
            // Verificar si el evento existe antes de intentar actualizarlo
            await VerificarExistenciaEvento(IdEvento);

            // La consulta de actualización puede seguir siendo la misma
            string query = @"ActualizarEvento";

            // Ejecutar la consulta con el IdEvento y el resto de parámetros desde el body
            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdEvento = IdEvento,  // Solo se pasa el IdEvento aquí, no necesitas pasarlo dos veces
                fkTipoEvento = evento.FkTipoEvento,
                fkUbicacion = evento.FkUbicacion,
                
                nombreEvento = evento.nombreEvento,
                descripcion = evento.descripcion,
                fecha = evento.fecha,
                horaInicio = evento.horaInicio,
                horaFin = evento.horaFin,
                fkNegocio = evento.FkNegocio,
                fkServicios = evento.FkServicios,
                ImagenUrl = evento.ImagenUrl
            });

            return resultadoQuery;
        }


        public async Task<Guid> Eliminar(Guid IdEvento) 
        {
            await VerificarExistenciaEvento(IdEvento);
            string query = @"EliminarEvento";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdEvento = IdEvento


            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<EventoResponse>> Obtener()
        {
            string query = @"ObtenerTodosEventos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EventoResponse>(query);

            // No es necesario hacer ninguna conversión, ya que TimeSpan se maneja correctamente en C#
            return resultadoConsulta;
        }

        public async Task<EventoResponse> Obtener(Guid IdEvento)
        {
            string query = @"ObtenerEventoPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EventoResponse>(query, new { IdEvento = IdEvento });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task VerificarExistenciaEvento(Guid IdEvento)
        {
            EventoResponse? resultadoConsultaEvento = await Obtener(IdEvento);
            if (resultadoConsultaEvento == null)
                throw new Exception("No se encontro el evento");
        }
    }
}
