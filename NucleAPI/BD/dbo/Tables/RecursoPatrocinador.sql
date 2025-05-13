CREATE TABLE [dbo].[RecursoPatrocinador] (
    [idRecursoPatrocinador] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombreRecurso]         NVARCHAR (100)   NOT NULL,
    [descripcion]           NVARCHAR (255)   NULL,
    [fkPatrocinador]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([idRecursoPatrocinador] ASC),
    FOREIGN KEY ([fkPatrocinador]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkPatrocinador]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkPatrocinador]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkPatrocinador]) REFERENCES [dbo].[Negocio] ([idNegocio])
);

