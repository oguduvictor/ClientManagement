CREATE TABLE [dbo].[Projects] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (256)   NOT NULL,
    [Description] NVARCHAR (256)   NULL,
    [ClientId]           UNIQUEIDENTIFIER NOT NULL,
    [Status]      INT              NOT NULL,
    CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Projects_ProjectStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[ProjectStatus] ([Id]),
    CONSTRAINT [FK_Projects_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);
