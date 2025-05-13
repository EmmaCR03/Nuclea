
-- =============================================
-- Author:		TuNombre
-- Create date: 2025-04-07
-- Description:	Obtiene todos los servicios
-- =============================================
CREATE PROCEDURE ObtenerTodosServicios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idServicio,
        nombreServicio,
        descripcion,
        costo
    FROM Servicios;
END