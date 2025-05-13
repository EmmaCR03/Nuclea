-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Elimina un negocio por su ID.
-- =============================================
CREATE PROCEDURE [dbo].[EliminarNegocioPorId]
    @idNegocio UNIQUEIDENTIFIER
AS
BEGIN
    -- Evita que se devuelvan conteos adicionales de filas afectadas.
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia una transacción para asegurar la integridad de los datos.
        BEGIN TRANSACTION;

        -- Elimina el negocio con el ID proporcionado.
        DELETE FROM Negocio
        WHERE idNegocio = @idNegocio;

        -- Confirma la transacción.
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hay un error, revierte la transacción.
        ROLLBACK TRANSACTION;

        -- Lanza el error capturado.
        THROW;
    END CATCH
END