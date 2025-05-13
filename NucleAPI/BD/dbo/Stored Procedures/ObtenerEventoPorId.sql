CREATE PROCEDURE [dbo].[ObtenerEventoPorId]
    @idEvento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el evento con detalles de TipoEvento y Ubicacion
    SELECT
        e.idEvento,
        e.nombreEvento,
        e.fecha,
        e.horaInicio,
        e.horaFin,
        e.descripcion,
		e.ImagenUrl,
        te.nombre AS TipoEvento,
        u.nombreUbicacion AS Ubicacion,
				n.nombre AS Negocio,
        s.nombreServicio AS Servicios

    FROM
        Evento e
        INNER JOIN TipoEvento te ON e.fkTipoEvento = te.idTipoEvento
        INNER JOIN Ubicacion u ON e.fkUbicacion = u.idUbicacion
		INNER JOIN Negocio n ON e.fkNegocio = n.idNegocio
        INNER JOIN Servicios s ON e.fkServicios = s.idServicio

    WHERE
        e.idEvento = @idEvento;
END;