CREATE TABLE [dbo].[Persona] (
    [idPersona] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombre]    NVARCHAR (50)    NOT NULL,
    [apellido]  NVARCHAR (50)    NOT NULL,
    [fkRol]     UNIQUEIDENTIFIER NOT NULL,
    [estado]    BIT              DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([idPersona] ASC),
    FOREIGN KEY ([fkRol]) REFERENCES [dbo].[Rol] ([idRol]),
    FOREIGN KEY ([fkRol]) REFERENCES [dbo].[Rol] ([idRol]),
    FOREIGN KEY ([fkRol]) REFERENCES [dbo].[Rol] ([idRol]),
    FOREIGN KEY ([fkRol]) REFERENCES [dbo].[Rol] ([idRol])
);

