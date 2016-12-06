CREATE TABLE [dbo].[Employee] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (30)   NOT NULL,
    [LastName]      NVARCHAR (30)   NOT NULL,
    [Salary]        DECIMAL (19, 2) NOT NULL,
    [SkillLevel]    INT             NOT NULL,
    [GenderId]      INT             NOT NULL,
    [AccountTypeId] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FKEmployee191756] FOREIGN KEY ([GenderId]) REFERENCES [dbo].[Gender] ([Id]),
    CONSTRAINT [FKEmployee497673] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountType] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Employee_Id]
    ON [dbo].[Employee]([Id] ASC);

