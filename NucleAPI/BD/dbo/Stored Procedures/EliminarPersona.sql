-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,Eliminar una persona de manera lógica o física>
-- =============================================
CREATE   PROCEDURE [EliminarPersona]
    -- Add the parameters for the stored procedure here
    @idPersona UNIQUEIDENTIFIER,
    @eliminacionLogica BIT = 1 -- 1 para eliminación lógica (cambiar estado), 0 para física
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar si la persona existe
        IF NOT EXISTS (SELECT 1 FROM Persona WHERE idPersona = @idPersona)
        BEGIN
            RAISERROR('La persona con el ID especificado no existe.', 16, 1);
            RETURN;
        END
        
        -- Eliminación lógica (cambiar estado a 0/falso)
        IF @eliminacionLogica = 1
        BEGIN
            UPDATE Persona
            SET estado = 0
            WHERE idPersona = @idPersona;
            
            SELECT 'Eliminación lógica realizada correctamente.' AS Resultado;
        END
        ELSE
        BEGIN
            -- Eliminación física (borrar registro)
            DELETE FROM Persona
            WHERE idPersona = @idPersona;
            
            SELECT 'Eliminación física realizada correctamente.' AS Resultado;
        END
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END