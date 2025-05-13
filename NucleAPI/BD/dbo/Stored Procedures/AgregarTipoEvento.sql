
CREATE PROCEDURE [dbo].[AgregarTipoEvento]
    @idTipoEvento UNIQUEIDENTIFIER,
    @nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar el tipo de evento
        INSERT INTO TipoEvento (
            idTipoEvento,
            nombre
        )
        VALUES (
            @idTipoEvento,
            @nombre
        );

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;