CREATE TABLE [dbo].[Usuarios] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [NombreUsuario]     VARCHAR (MAX)    NOT NULL,
    [PasswordHash]      VARCHAR (MAX)    NOT NULL,
    [CorreoElectronico] VARCHAR (MAX)    NOT NULL,
    [FechaCreacion]     DATETIME         NULL,
    [FechaModificacion] DATETIME         NULL,
    [UsuarioCrea]       UNIQUEIDENTIFIER NULL,
    [UsuarioModifica]   UNIQUEIDENTIFIER NULL,
    [Nombre]            VARCHAR (50)     NULL,
    [Apellido]          VARCHAR (50)     NULL,
    [Id_Perfil]         INT              NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC)
);

