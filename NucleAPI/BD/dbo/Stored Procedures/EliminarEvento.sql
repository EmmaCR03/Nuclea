
CREATE PROCEDURE [dbo].[EliminarEvento]
    @idEvento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar el evento
        DELETE FROM Evento
        WHERE idEvento = @idEvento;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;