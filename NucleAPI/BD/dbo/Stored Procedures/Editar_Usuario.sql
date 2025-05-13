CREATE PROCEDURE Editar_Usuario 
    @p_id UNIQUEIDENTIFIER,
    @p_nombre NVARCHAR(255),
    @p_email NVARCHAR(255),
    @p_id_perfil INT
AS
BEGIN
    UPDATE Usuarios
    SET 
        Nombre = @p_nombre,
        CorreoElectronico = @p_email,
        Id_Perfil = @p_id_perfil
    WHERE Id = @p_id;
END