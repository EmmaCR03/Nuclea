
CREATE PROCEDURE [dbo].[ObtenerTipoEventoPorId]
    @idTipoEvento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el tipo de evento por su ID
    SELECT
        idTipoEvento,
        nombre
    FROM
        TipoEvento
    WHERE
        idTipoEvento = @idTipoEvento;
END;