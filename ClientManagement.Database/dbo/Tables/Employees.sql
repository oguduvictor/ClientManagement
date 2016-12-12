CREATE TABLE [dbo].[Employees] (
    [Id]         INT             NOT NULL,
    [FirstName]  NVARCHAR (30)   NOT NULL,
    [LastName]   NVARCHAR (30)   NOT NULL,
    [Salary]     DECIMAL (19, 2) NOT NULL,
    [SkillLevel] INT             NOT NULL,
    [Gender]     INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([LastName] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Employees_Id]
    ON [dbo].[Employees]([Id] ASC);

