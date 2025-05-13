CREATE TABLE [dbo].[EventoRegistrado] (
    [idEventoRegistrado] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [idEvento]           UNIQUEIDENTIFIER NOT NULL,
    [idUsuario]          UNIQUEIDENTIFIER NOT NULL,
    [qr]                 NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([idEventoRegistrado] ASC),
    FOREIGN KEY ([idEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([idUsuario]) REFERENCES [dbo].[Usuarios] ([Id]),
    CONSTRAINT [UQ_EventoUsuario] UNIQUE NONCLUSTERED ([idEvento] ASC, [idUsuario] ASC)
);

