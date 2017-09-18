CREATE TABLE [dbo].[Employees] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (255)   NOT NULL,
    [LastName]  NVARCHAR (255)   NOT NULL,
    [Gender]    INT              NOT NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Employees_Gender] FOREIGN KEY ([Gender]) REFERENCES [dbo].[Gender] ([Id]),
    CONSTRAINT [FK_Employees_AspUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[Employees] ([Id])
);
