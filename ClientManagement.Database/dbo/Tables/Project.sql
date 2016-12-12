CREATE TABLE [dbo].[Project] (
    [Id]                 INT            NOT NULL,
    [ProjectTitle]       NVARCHAR (100) NOT NULL,
    [ProjectDescription] NVARCHAR (500) NOT NULL,
    [ProjectStatus]      INT            NOT NULL,
    [ClientId]           INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKProject552251] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

