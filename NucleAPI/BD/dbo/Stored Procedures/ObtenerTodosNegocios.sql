CREATE PROCEDURE  [dbo].[ObtenerTodosNegocios]
AS
BEGIN
    -- Evita que se devuelvan conteos adicionales de filas afectadas.
    SET NOCOUNT ON;

    -- Selecciona los datos de los negocios junto con la descripción del rol.
    SELECT 
        n.idNegocio,
        n.nombre,
        n.descripcion,
		n.ImagenUrl-- Descripción del negocio
    FROM 
        Negocio n
END