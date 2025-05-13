
CREATE PROCEDURE [dbo].[ActualizarUbicacion]
    @idUbicacion UNIQUEIDENTIFIER,
    @nombreUbicacion NVARCHAR(100),
    @direccion NVARCHAR(MAX),
    @capacidad INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar la ubicación
        UPDATE Ubicacion
        SET 
            nombreUbicacion = @nombreUbicacion,
            direccion = @direccion,
            capacidad = @capacidad
        WHERE 
            idUbicacion = @idUbicacion;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;