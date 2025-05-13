
CREATE PROCEDURE [dbo].[RegistrarUsuarioAEvento]
    @idEventoRegistrado UNIQUEIDENTIFIER,
    @idEvento UNIQUEIDENTIFIER,
    @idUsuario UNIQUEIDENTIFIER,
    @qr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar si el usuario ya está registrado en el evento
        IF NOT EXISTS (SELECT 1 FROM EventoRegistrado 
                      WHERE idEvento = @idEvento AND idUsuario = @idUsuario)
        BEGIN
            -- Insertar nuevo registro
            INSERT INTO EventoRegistrado (idEventoRegistrado, idEvento, idUsuario, qr)
            VALUES (@idEventoRegistrado, @idEvento, @idUsuario, @qr);
            
            COMMIT TRANSACTION;
            SELECT @idEventoRegistrado AS idEventoRegistrado;
        END
        ELSE
        BEGIN
            -- Usuario ya registrado, devolver el ID existente
            SELECT idEventoRegistrado FROM EventoRegistrado 
            WHERE idEvento = @idEvento AND idUsuario = @idUsuario;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;