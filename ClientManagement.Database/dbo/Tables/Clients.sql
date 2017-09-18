CREATE TABLE [dbo].[Clients] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (255)   NOT NULL,
    [EmailAddress] NVARCHAR (MAX)   NULL,
    [PhoneNumber] NVARCHAR(20) NULL, 
    CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

