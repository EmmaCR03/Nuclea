
create PROCEDURE [dbo].[ActualizarTipoEvento]
    @idTipoEvento UNIQUEIDENTIFIER,
    @nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar el tipo de evento
        UPDATE TipoEvento
        SET nombre = @nombre
        WHERE idTipoEvento = @idTipoEvento;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;