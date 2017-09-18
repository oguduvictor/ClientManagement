/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
ALTER TABLE dbo.Projects
NOCHECK CONSTRAINT FK_Projects_ProjectStatus;

ALTER TABLE dbo.Employees
NOCHECK CONSTRAINT FK_Employees_Gender;

ALTER TABLE dbo.AspNetUserRoles
NOCHECK CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId;

DELETE ProjectStatus;
DELETE Gender;
DELETE AspNetRoles;

GO
INSERT INTO ProjectStatus(Id, Name)
VALUES
	(100, 'Not Started'),
	(200, 'In Progress'),
	(300, 'Completed'),
	(400, 'Cancelled')

GO
INSERT INTO Gender(Id, Name)
VALUES
	(10, 'Male'),
	(20, 'Gender')

GO
INSERT INTO AspNetRoles(Id, Name)
VALUES
	(10000, 'Employee'),
	(20000, 'Manager')

GO
ALTER TABLE dbo.Projects
CHECK CONSTRAINT FK_Projects_ProjectStatus;

ALTER TABLE dbo.Employees
CHECK CONSTRAINT FK_Employees_Gender;

ALTER TABLE dbo.AspNetUserRoles
CHECK CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId;
