
CREATE PROCEDURE [dbo].[EliminarUbicacion]
    @idUbicacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar la ubicación
        DELETE FROM Ubicacion
        WHERE idUbicacion = @idUbicacion;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;