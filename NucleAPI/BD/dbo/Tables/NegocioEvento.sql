CREATE TABLE [dbo].[NegocioEvento] (
    [idNegocioEvento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [fkNegocio]       UNIQUEIDENTIFIER NOT NULL,
    [fkEvento]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([idNegocioEvento] ASC),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkNegocio]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkNegocio]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkNegocio]) REFERENCES [dbo].[Negocio] ([idNegocio]),
    FOREIGN KEY ([fkNegocio]) REFERENCES [dbo].[Negocio] ([idNegocio])
);

