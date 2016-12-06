CREATE TABLE [dbo].[Client] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (150) NOT NULL,
    [EmailAddress] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Client_Id]
    ON [dbo].[Client]([Id] ASC);

