CREATE TABLE [dbo].[EventoServicio] (
    [idEventoServicio] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [fkEvento]         UNIQUEIDENTIFIER NOT NULL,
    [fkServicio]       UNIQUEIDENTIFIER NOT NULL,
    [cantidad]         INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([idEventoServicio] ASC),
    CHECK ([cantidad]>(0)),
    CHECK ([cantidad]>(0)),
    CHECK ([cantidad]>(0)),
    CHECK ([cantidad]>(0)),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkServicio]) REFERENCES [dbo].[Servicios] ([idServicio]),
    FOREIGN KEY ([fkServicio]) REFERENCES [dbo].[Servicios] ([idServicio]),
    FOREIGN KEY ([fkServicio]) REFERENCES [dbo].[Servicios] ([idServicio]),
    FOREIGN KEY ([fkServicio]) REFERENCES [dbo].[Servicios] ([idServicio])
);

