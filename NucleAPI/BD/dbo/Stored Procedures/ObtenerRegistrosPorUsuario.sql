CREATE PROCEDURE [dbo].[ObtenerRegistrosPorUsuario]
    @idUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        er.idEventoRegistrado,
        e.nombreEvento,
        e.fecha,
        e.horaInicio,
        e.horaFin,
        ub.nombreUbicacion AS ubicacion,
        er.qr
    FROM 
        EventoRegistrado er
    INNER JOIN Evento e ON er.idEvento = e.idEvento
    INNER JOIN Ubicacion ub ON e.fkUbicacion = ub.idUbicacion
    WHERE 
        er.idUsuario = @idUsuario;
END;