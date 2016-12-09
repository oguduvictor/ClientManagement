CREATE TABLE [dbo].[EmployeeProject] (
    [ProjectId]  INT              NOT NULL,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ProjectId] ASC, [EmployeeId] ASC),
    CONSTRAINT [FKEmployeePr350606] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    CONSTRAINT [FKEmployeePr510305] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

