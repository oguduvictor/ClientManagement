CREATE TABLE [dbo].[Client] (
    [Id]                 INT            NOT NULL,
    [ClientName]         NVARCHAR (150) NOT NULL,
    [ClientEmailAddress] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([ClientEmailAddress] ASC),
    UNIQUE NONCLUSTERED ([ClientName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Client_Id]
    ON [dbo].[Client]([Id] ASC);

