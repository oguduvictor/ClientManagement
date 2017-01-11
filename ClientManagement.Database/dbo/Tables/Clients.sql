CREATE TABLE [dbo].[Clients] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ClientName]         NVARCHAR (MAX)   NULL,
    [ClientEmailAddress] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

