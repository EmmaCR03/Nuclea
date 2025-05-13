CREATE PROCEDURE [dbo].[ObtenerTodosLosEventosRegistrados]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        er.idEventoRegistrado,
        er.idUsuario,
        er.idEvento,
        er.qr,
        e.nombreEvento,
        e.fecha,
        e.horaInicio,
        e.horaFin,
        e.descripcion,
        u.Nombre + ' ' + u.Apellido AS nombreUsuario,
        ub.nombreUbicacion AS nombreUbicacion
    FROM 
        EventoRegistrado er
    INNER JOIN 
        Evento e ON er.idEvento = e.idEvento
    INNER JOIN 
        Usuarios u ON er.idUsuario = u.Id
    INNER JOIN
        Ubicacion ub ON e.fkUbicacion = ub.idUbicacion;
END;