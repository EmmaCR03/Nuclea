Create PROCEDURE [dbo].[ActualizarServicio]
    @idServicio UNIQUEIDENTIFIER,
    @nombreServicio NVARCHAR(255),
    @descripcion NVARCHAR(500),
    @costo DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar el servicio
        UPDATE Servicios
        SET 
            nombreServicio = @nombreServicio,
            descripcion = @descripcion,
            costo = @costo
        WHERE 
            idServicio = @idServicio;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;