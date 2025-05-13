CREATE TABLE [dbo].[Rol] (
    [idRol]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [descripcion] NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([idRol] ASC)
);

