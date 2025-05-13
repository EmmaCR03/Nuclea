
-- =============================================
-- Author:		Jesús
-- Create date: 10/04/2025
-- Description: Obtener un usuario por su Id
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerUsuarioPorId]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		Id, 
		NombreUsuario, 
		PasswordHash, 
		CorreoElectronico, 
		FechaCreacion, 
		FechaModificacion, 
		UsuarioCrea, 
		UsuarioModifica, 
		Nombre, 
		Apellido
	FROM 
		Usuarios
	WHERE 
		Id = @Id
END