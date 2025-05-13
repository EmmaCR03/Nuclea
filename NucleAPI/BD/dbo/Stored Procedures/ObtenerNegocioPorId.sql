-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Obtiene un negocio por su ID.
-- =============================================
CREATE PROCEDURE  [dbo].[ObtenerNegocioPorId]
    @idNegocio UNIQUEIDENTIFIER
AS
BEGIN
    -- Evita que se devuelvan conteos adicionales de filas afectadas.
    SET NOCOUNT ON;

    -- Selecciona los datos del negocio junto con la descripción del rol.
    SELECT 
        n.idNegocio,
        n.nombre,
        n.descripcion AS descripcionNegocio ,
		n.ImagenUrl-- Descripción del negocio
    FROM 
        Negocio n
    WHERE
        n.idNegocio = @idNegocio; -- Filtra por el ID del negocio
END