CREATE TABLE AccountType (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(12) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Clients (
  Id           int NOT NULL, 
  Name         nvarchar(150) NOT NULL, 
  Description  text NOT NULL, 
  EmailAddress nvarchar(80) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Clients_Projects (
  ClientsId  int NOT NULL, 
  ProjectsId int NOT NULL, 
  PRIMARY KEY (ClientsId, 
  ProjectsId));
CREATE TABLE Employees (
  Id            int NOT NULL, 
  FirstName     nvarchar(30) NOT NULL, 
  LastName      nvarchar(30) NOT NULL, 
  Salary        money NOT NULL, 
  SkillLevel    int NOT NULL, 
  GendersId     int NOT NULL, 
  AccountTypeId int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Employees_Clients_Projects (
  EmployeesId                int NOT NULL, 
  Clients_ProjectsClientsId  int NOT NULL, 
  Clients_ProjectsProjectsId int NOT NULL, 
  PRIMARY KEY (EmployeesId, 
  Clients_ProjectsClientsId, 
  Clients_ProjectsProjectsId));
CREATE TABLE Genders (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(10) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Projects (
  Id              int IDENTITY NOT NULL, 
  Title           nvarchar(40) NOT NULL, 
  Details         text NOT NULL, 
  ProjectStatusId int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE ProjectStatus (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(20) NOT NULL, 
  PRIMARY KEY (Id));
CREATE UNIQUE INDEX Clients_Id 
  ON Clients (Id);
CREATE UNIQUE INDEX Employees_Id 
  ON Employees (Id);
ALTER TABLE Clients_Projects ADD CONSTRAINT FKClients_Pr537645 FOREIGN KEY (ClientsId) REFERENCES Clients (Id);
ALTER TABLE Employees ADD CONSTRAINT FKEmployees791873 FOREIGN KEY (AccountTypeId) REFERENCES AccountType (Id);
ALTER TABLE Projects ADD CONSTRAINT FKProjects396073 FOREIGN KEY (ProjectStatusId) REFERENCES ProjectStatus (Id);
ALTER TABLE Employees_Clients_Projects ADD CONSTRAINT FKEmployees_541819 FOREIGN KEY (Clients_ProjectsClientsId, Clients_ProjectsProjectsId) REFERENCES Clients_Projects (ClientsId, ProjectsId);
ALTER TABLE Employees_Clients_Projects ADD CONSTRAINT FKEmployees_746876 FOREIGN KEY (EmployeesId) REFERENCES Employees (Id);
ALTER TABLE Clients_Projects ADD CONSTRAINT FKClients_Pr246802 FOREIGN KEY (ProjectsId) REFERENCES Projects (Id);
ALTER TABLE Employees ADD CONSTRAINT FKEmployees887729 FOREIGN KEY (GendersId) REFERENCES Genders (Id);
