CREATE TABLE [dbo].[EmployeeProjects] (
    [EmployeeId] INT NOT NULL,
    [ProjectId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC, [ProjectId] ASC),
    CONSTRAINT [FKEmployeePr150957] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FKEmployeePr686132] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([Id])
);

