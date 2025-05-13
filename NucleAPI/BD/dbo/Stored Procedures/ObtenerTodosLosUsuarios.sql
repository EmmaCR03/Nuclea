-- =============================================
-- Author:		Jesús
-- Create date: 10/04/2025
-- Description:	Obtiene todos los datos de los usuarios
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerTodosLosUsuarios]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreUsuario,
        PasswordHash,
        CorreoElectronico,
        Nombre,
        Apellido,
        FechaCreacion,
        FechaModificacion,
        UsuarioCrea,
        UsuarioModifica
    FROM Usuarios
END