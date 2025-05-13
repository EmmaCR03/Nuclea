-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarServicio]
	-- Add the parameters for the stored procedure here
	@idServicio UNIQUEIDENTIFIER,
	@nombreServicio nvarchar(100),
	@descripcion nvarchar(255),
	@costo decimal(10,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Servicios(idServicio, nombreServicio, descripcion, costo) values(@idServicio, @nombreServicio, @descripcion, @costo)
END