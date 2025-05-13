CREATE TABLE [dbo].[Ubicacion] (
    [idUbicacion]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombreUbicacion] NVARCHAR (100)   NOT NULL,
    [direccion]       NVARCHAR (255)   NOT NULL,
    [capacidad]       INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([idUbicacion] ASC),
    CHECK ([capacidad]>(0)),
    CHECK ([capacidad]>(0)),
    CHECK ([capacidad]>(0)),
    CHECK ([capacidad]>(0))
);

