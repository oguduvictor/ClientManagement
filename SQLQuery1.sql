
select * from Project;
select * from Employees;
select * from Client;
select * from EmployeeProject;

Select Employees.Id, Employees.FirstName,
 Employees.LastName, Project.ProjectTitle, Project.ProjectStatus
From Employees
Left join EmployeeProject on (Employees.Id = EmployeeProject.EmployeeId)
Left join Project on (Project.Id = EmployeeProject.ProjectId)

delete from EmployeeProject;
delete from Project;
delete from Client
delete from Employees

Select Employees.Id, Project.ProjectTitle, Project.ProjectDescription,
Project.ProjectStatus
from Project
inner join EmployeeProject
On EmployeeProject.ProjectId = Project.Id