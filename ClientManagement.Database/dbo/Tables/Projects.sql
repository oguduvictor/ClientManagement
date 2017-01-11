CREATE TABLE [dbo].[Projects] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ProjectTitle]       NVARCHAR (256)   NOT NULL,
    [ProjectDescription] NVARCHAR (256)   NOT NULL,
    [ClientId]           UNIQUEIDENTIFIER NOT NULL,
    [ProjectStatus]      INT              NOT NULL,
    CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Projects_dbo.Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[Projects]([ClientId] ASC);

