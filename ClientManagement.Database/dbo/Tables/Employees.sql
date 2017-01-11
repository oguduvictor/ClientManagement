CREATE TABLE [dbo].[Employees] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (MAX)   NULL,
    [LastName]  NVARCHAR (MAX)   NULL,
    [Gender]    INT              NOT NULL,
    [UserId]    NVARCHAR (128)   NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_AspUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [FK_Employees_AspUsers]
    ON [dbo].[Employees]([UserId] ASC);

