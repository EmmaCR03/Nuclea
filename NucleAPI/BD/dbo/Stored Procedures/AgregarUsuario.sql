
-- =============================================
-- Autor: Jesús
-- Fecha: 10/04/2025
-- Descripción: Agrega un nuevo usuario con nombre y apellido
-- =============================================
CREATE PROCEDURE [dbo].[AgregarUsuario]
	@NombreUsuario     VARCHAR(MAX),
	@PasswordHash      VARCHAR(MAX),
	@CorreoElectronico VARCHAR(MAX),
	@Nombre            VARCHAR(50),
	@Apellido          VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Id AS UNIQUEIDENTIFIER = NEWID();

	BEGIN TRANSACTION;

	BEGIN TRY
		INSERT INTO [dbo].[Usuarios]
			   ([Id],
			    [NombreUsuario],
			    [PasswordHash],
			    [CorreoElectronico],
			    [Nombre],
			    [Apellido])
		VALUES
			   (@Id,
			    @NombreUsuario,
			    @PasswordHash,
			    @CorreoElectronico,
			    @Nombre,
			    @Apellido);

		INSERT INTO [dbo].[PerfilesxUsuario]
			   ([IdUsuario],
			    [IdPerfil])
		VALUES
			   (@Id,
			    1);

		SELECT @Id AS IdUsuario;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
		RAISERROR(@ErrorMessage, 16, 1);
	END CATCH
END