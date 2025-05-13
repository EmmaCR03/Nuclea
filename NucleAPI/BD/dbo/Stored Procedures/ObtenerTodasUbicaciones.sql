CREATE PROCEDURE [dbo].[ObtenerTodasUbicaciones]
AS
BEGIN
    -- Evita que se devuelvan conteos adicionales de filas afectadas.
    SET NOCOUNT ON;

    -- Selecciona todas las ubicaciones
    SELECT 
        idUbicacion,
        nombreUbicacion,
        direccion,
        capacidad
    FROM 
        Ubicacion;
END