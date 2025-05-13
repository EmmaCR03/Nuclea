
CREATE PROCEDURE [dbo].[AgregarUbicacion]
    @idUbicacion UNIQUEIDENTIFIER,
    @nombreUbicacion NVARCHAR(100),
    @direccion NVARCHAR(MAX),
    @capacidad INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar la ubicación
        INSERT INTO Ubicacion (
            idUbicacion,
            nombreUbicacion,
            direccion,
            capacidad
        )
        VALUES (
            @idUbicacion,
            @nombreUbicacion,
            @direccion,
            @capacidad
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