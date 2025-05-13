
CREATE PROCEDURE [dbo].[AgregarEvento]
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
        -- Insertar el evento
        INSERT INTO Evento (
            idEvento,
            nombreEvento,
            fecha,
            horaInicio,
            horaFin,
            descripcion,
            fkTipoEvento,
            fkUbicacion,
            fkNegocio,
            fkServicios,
            ImagenUrl
        )
        VALUES (
            @idEvento,
            @nombreEvento,
            @fecha,
            @horaInicio,
            @horaFin,
            @descripcion,
            @fkTipoEvento,
            @fkUbicacion,
            @fkNegocio,
            @fkServicios,
            @ImagenUrl
        );

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, revertir la transacción
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;