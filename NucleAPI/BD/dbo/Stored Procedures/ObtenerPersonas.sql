-- =================================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,Obtener todas las personas activas>
-- =================================================
CREATE   PROCEDURE ObtenerPersonas
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    BEGIN TRY
        -- Obtener todas las personas con estado activo (1)
        SELECT 
            idPersona,
            nombre,
            apellido,
            fkRol,
            estado
        FROM 
            Persona
        WHERE 
            estado = 1
        ORDER BY 
            apellido, nombre;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END