CREATE PROCEDURE [dbo].[ObtenerServicioPorId]
    @idServicio UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Consultar los datos del servicio con el ID proporcionado
        SELECT 
            idServicio, 
            nombreServicio, 
            descripcion, 
            costo
        FROM 
            Servicios
        WHERE 
            idServicio = @idServicio;

        -- Si no se encuentra el servicio, puedes lanzar un error
        IF NOT EXISTS (SELECT 1 FROM Servicios WHERE idServicio = @idServicio)
        BEGIN
            RAISERROR('El servicio con el ID especificado no existe.', 16, 1);
            RETURN;
        END
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, lo manejamos aquí
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END