-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarNegocio]
	-- Add the parameters for the stored procedure here
	@idNegocio UNIQUEIDENTIFIER,
	@nombre nvarchar(100),
	@descripcion nvarchar(255),
	    @ImagenUrl NVARCHAR(MAX) = NULL

AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar el evento
        UPDATE Negocio
        SET
            nombre = @nombre,
            descripcion = @descripcion,
        ImagenUrl = @ImagenUrl

        WHERE
            idNegocio = @idNegocio;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;