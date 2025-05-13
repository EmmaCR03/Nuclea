CREATE TABLE [dbo].[TipoEvento] (
    [idTipoEvento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombre]       NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([idTipoEvento] ASC)
);

