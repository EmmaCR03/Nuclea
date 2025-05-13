CREATE TABLE [dbo].[TipoContacto] (
    [idTipoContacto] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [descripcion]    NVARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([idTipoContacto] ASC)
);

