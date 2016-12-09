CREATE TABLE [dbo].[Project] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (100)   NOT NULL,
    [Description]     NVARCHAR (500)   NOT NULL,
    [ProjectStatusId] INT              NOT NULL,
    [ClientId]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKProject1511] FOREIGN KEY ([ProjectStatusId]) REFERENCES [dbo].[ProjectStatus] ([Id]),
    CONSTRAINT [FKProject552251] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

