CREATE TABLE [dbo].[PersonaEvento] (
    [idPersonaEvento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [fkPersona]       UNIQUEIDENTIFIER NOT NULL,
    [fkEvento]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([idPersonaEvento] ASC),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkEvento]) REFERENCES [dbo].[Evento] ([idEvento]),
    FOREIGN KEY ([fkPersona]) REFERENCES [dbo].[Persona] ([idPersona]),
    FOREIGN KEY ([fkPersona]) REFERENCES [dbo].[Persona] ([idPersona]),
    FOREIGN KEY ([fkPersona]) REFERENCES [dbo].[Persona] ([idPersona]),
    FOREIGN KEY ([fkPersona]) REFERENCES [dbo].[Persona] ([idPersona])
);

