CREATE TABLE [dbo].[Employees] (
    [Id]         INT             NOT NULL,
    [FirstName]  NVARCHAR (30)   NOT NULL,
    [LastName]   NVARCHAR (30)   NOT NULL,
    [Salary]     DECIMAL (19, 2) NOT NULL,
    [SkillLevel] INT             NOT NULL,
    [Gender]     INT             NOT NULL,
    [UserId]     NVARCHAR (128)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKEmployees105251] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([LastName] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employees]
    ON [dbo].[Employees]([Id] ASC);

