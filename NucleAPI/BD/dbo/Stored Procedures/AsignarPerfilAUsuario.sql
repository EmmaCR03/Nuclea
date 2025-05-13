
CREATE PROCEDURE [dbo].[AsignarPerfilAUsuario]
(
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPerfil INT
)
AS
BEGIN
    -- Verificar si el usuario ya tiene un perfil asignado
    IF EXISTS (SELECT 1 FROM PerfilesxUsuario WHERE IdUsuario = @IdUsuario)
    BEGIN
        -- Actualizar el perfil existente
        UPDATE PerfilesxUsuario
        SET IdPerfil = @IdPerfil
        WHERE IdUsuario = @IdUsuario;
    END
    ELSE
    BEGIN
        -- Insertar nuevo perfil si no tiene ninguno
        INSERT INTO PerfilesxUsuario (IdUsuario, IdPerfil)
        VALUES (@IdUsuario, @IdPerfil);
    END
END