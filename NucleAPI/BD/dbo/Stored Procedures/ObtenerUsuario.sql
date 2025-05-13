-- =============================================
-- Author:		Jesús
-- Create date: 10/04/2025
-- Description:	Obtiene un usuario por nombre de usuario o correo electrónico, incluyendo nombre y apellido
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerUsuario]
	@NombreUsuario VARCHAR(MAX),
	@CorreoElectronico VARCHAR(MAX)
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
	WHERE NombreUsuario = @NombreUsuario
	   OR CorreoElectronico = @CorreoElectronico
END