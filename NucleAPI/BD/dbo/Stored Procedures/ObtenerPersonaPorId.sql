-- =================================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,Obtener una persona por su ID>
-- =================================================
CREATE   PROCEDURE ObtenerPersonaPorId
    -- Add the parameters for the stored procedure here
    @idPersona UNIQUEIDENTIFIER
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    BEGIN TRY
        -- Obtener la persona por ID
        SELECT 
            idPersona,
            nombre,
            apellido,
            fkRol,
            estado
        FROM 
            Persona
        WHERE 
            idPersona = @idPersona;
        
        -- Verificar si se encontró el registro
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No se encontró la persona con el ID especificado.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END