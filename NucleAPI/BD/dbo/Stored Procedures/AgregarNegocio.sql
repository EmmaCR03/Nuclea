-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarNegocio] 
    @idNegocio UNIQUEIDENTIFIER,
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255),
	@ImagenUrl NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Validar si el rol existe

        -- Insertar el negocio solo si el rol existe
        INSERT INTO Negocio (
            idNegocio,
            nombre,
            descripcion,
			ImagenUrl
        )
        VALUES (
            @idNegocio,
            @nombre,
            @descripcion,
			@ImagenUrl
        );

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;