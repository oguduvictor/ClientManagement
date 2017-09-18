CREATE TABLE [dbo].[EmployeeProject] (
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.EmployeeProject] PRIMARY KEY CLUSTERED ([EmployeeId] ASC, [ProjectId] ASC),
    CONSTRAINT [FK_dbo.EmployeeProject_dbo.Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_dbo.EmployeeProject_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);
