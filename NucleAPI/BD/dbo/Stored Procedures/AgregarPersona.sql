
Create PROCEDURE [dbo].[AgregarPersona]
    @idPersona UNIQUEIDENTIFIER OUTPUT,
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @fkRol UNIQUEIDENTIFIER,
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET @idPersona = NEWID(); 

    BEGIN TRANSACTION;

    BEGIN TRY
       
        INSERT INTO Persona (idPersona, nombre, apellido, fkRol, estado)
        VALUES (@idPersona, @nombre, @apellido, @fkRol, @estado);

        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;