
CREATE PROCEDURE [dbo].[ObtenerUbicacionPorId]
    @idUbicacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener la ubicación por su ID
    SELECT
        idUbicacion,
        nombreUbicacion,
        direccion,
        capacidad
    FROM
        Ubicacion
    WHERE
        idUbicacion = @idUbicacion;
END;