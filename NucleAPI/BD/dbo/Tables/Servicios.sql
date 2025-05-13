CREATE TABLE [dbo].[Servicios] (
    [idServicio]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [nombreServicio] NVARCHAR (100)   NOT NULL,
    [descripcion]    NVARCHAR (255)   NOT NULL,
    [costo]          DECIMAL (10, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([idServicio] ASC),
    CHECK ([costo]>=(0)),
    CHECK ([costo]>=(0)),
    CHECK ([costo]>=(0)),
    CHECK ([costo]>=(0))
);

