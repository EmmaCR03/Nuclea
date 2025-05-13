-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,Editar los datos de una persona>
-- =============================================
CREATE   PROCEDURE [ActualizarPersona] 
	-- Add the parameters for the stored procedure here
	@idPersona UNIQUEIDENTIFIER,
	@nombre NVARCHAR(100),
	@apellido NVARCHAR(100),
	@fkRol UNIQUEIDENTIFIER,
	@estado BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar si la persona existe
        IF NOT EXISTS (SELECT 1 FROM Persona WHERE idPersona = @idPersona)
        BEGIN
            RAISERROR('La persona con el ID especificado no existe.', 16, 1);
            RETURN;
        END
        
        -- Verificar si el rol existe
        IF NOT EXISTS (SELECT 1 FROM Rol WHERE idRol = @fkRol)
        BEGIN
            RAISERROR('El rol especificado no existe.', 16, 1);
            RETURN;
        END
        
        -- Actualizar los datos de la persona
        UPDATE Persona
        SET 
            nombre = @nombre,
            apellido = @apellido,
            fkRol = @fkRol,
            estado = @estado
        WHERE 
            idPersona = @idPersona;
        
        COMMIT TRANSACTION;
        
        -- Retornar los datos actualizados
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