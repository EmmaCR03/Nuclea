
CREATE PROCEDURE [dbo].[EliminarTipoEvento]
    @idTipoEvento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar el tipo de evento
        DELETE FROM TipoEvento
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