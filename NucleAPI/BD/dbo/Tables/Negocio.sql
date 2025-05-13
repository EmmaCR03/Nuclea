CREATE TABLE [dbo].[Negocio] (
    [idNegocio]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombre]      NVARCHAR (100)   NOT NULL,
    [descripcion] NVARCHAR (255)   NULL,
    [ImagenUrl]   NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([idNegocio] ASC),

);

