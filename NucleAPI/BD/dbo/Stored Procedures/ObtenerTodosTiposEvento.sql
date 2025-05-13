CREATE PROCEDURE [dbo].[ObtenerTodosTiposEvento]
AS
BEGIN
    -- Evita que se devuelvan conteos adicionales de filas afectadas.
    SET NOCOUNT ON;

    -- Selecciona todos los tipos de evento
    SELECT 
        idTipoEvento,
        nombre
    FROM 
        TipoEvento;
END