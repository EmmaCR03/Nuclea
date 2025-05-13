
CREATE PROCEDURE [dbo].[ActualizarEvento]
    @idEvento UNIQUEIDENTIFIER,
    @nombreEvento NVARCHAR(100),
    @fecha DATE,
    @horaInicio TIME,
    @horaFin TIME,
    @descripcion NVARCHAR(MAX),
    @fkTipoEvento UNIQUEIDENTIFIER,
    @fkUbicacion UNIQUEIDENTIFIER,
    @fkNegocio UNIQUEIDENTIFIER,
    @fkServicios UNIQUEIDENTIFIER,
    @ImagenUrl NVARCHAR(MAX) = NULL


AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar el evento
        UPDATE Evento
        SET
            nombreEvento = @nombreEvento,
            fecha = @fecha,
            horaInicio = @horaInicio,
            horaFin = @horaFin,
            descripcion = @descripcion,
            fkTipoEvento = @fkTipoEvento,
            fkUbicacion = @fkUbicacion,
            fkNegocio = @fkNegocio,
            fkServicios = @fkServicios,
            ImagenUrl = @ImagenUrl
        WHERE
            idEvento = @idEvento;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;