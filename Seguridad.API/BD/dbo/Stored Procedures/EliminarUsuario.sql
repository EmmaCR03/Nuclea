CREATE PROCEDURE [dbo].[EliminarUsuario]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validar que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @Id)
        BEGIN
            RAISERROR('El usuario no existe.', 16, 1);
            RETURN;
        END

        -- Eliminar referencias (si las hay)
        DELETE FROM PerfilesxUsuario WHERE IdUsuario = @Id;

        -- Eliminar usuario
        DELETE FROM Usuarios WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT;
        SELECT 
            @ErrMsg = ERROR_MESSAGE(), 
            @ErrSeverity = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END