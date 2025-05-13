Create PROCEDURE [dbo].[EliminarServicio]
    @idServicio UNIQUEIDENTIFIER
AS
BEGIN
    -- Verificamos si el servicio existe
    IF EXISTS (SELECT 1 FROM Servicios WHERE idServicio = @idServicio)
    BEGIN
        -- Eliminamos el servicio
        DELETE FROM Servicios WHERE idServicio = @idServicio;
        
        -- Podemos retornar el Id del servicio eliminado como confirmación
        SELECT @idServicio AS idServicioEliminado;
    END
    ELSE
    BEGIN
        -- Si no existe, lanzamos un error
        RAISERROR('El servicio con el Id especificado no existe.', 16, 1);
    END
END