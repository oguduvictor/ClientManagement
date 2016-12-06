CREATE TABLE AccountType (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(12) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Clients (
  Id           int IDENTITY(1, 1) NOT NULL, 
  Name         nvarchar(150) NOT NULL, 
  Description  text NOT NULL, 
  EmailAddress nvarchar(80) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Clients_Projects (
  Id              int IDENTITY(1, 1) NOT NULL, 
  ClientsId       int NOT NULL, 
  ProjectsId      int NOT NULL, 
  ProjectStatusId int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Clients_Projects_Employees (
  Id                 int IDENTITY(1, 1) NOT NULL, 
  Clients_Projectsid int NOT NULL, 
  EmployeesId        int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Employees (
  Id            int IDENTITY(1, 1) NOT NULL, 
  FirstName     nvarchar(30) NOT NULL, 
  LastName      nvarchar(30) NOT NULL, 
  Salary        money NOT NULL, 
  SkillLevel    int NOT NULL, 
  GendersId     int NOT NULL, 
  AccountTypeId int NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Genders (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(10) NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE Projects (
  Id      int IDENTITY(1, 1) NOT NULL, 
  Title   nvarchar(40) NOT NULL, 
  Details text NOT NULL, 
  PRIMARY KEY (Id));
CREATE TABLE ProjectStatus (
  Id          int IDENTITY NOT NULL, 
  Description nvarchar(20) DEFAULT 'Not Started' NOT NULL, 
  PRIMARY KEY (Id));
CREATE UNIQUE INDEX Clients_Id 
  ON Clients (Id);
CREATE UNIQUE INDEX Clients_Projects_Id 
  ON Clients_Projects (Id);
CREATE UNIQUE INDEX Clients_Projects_Employees_Id 
  ON Clients_Projects_Employees (Id);
CREATE UNIQUE INDEX Employees_Id 
  ON Employees (Id);
CREATE UNIQUE INDEX Projects_Id 
  ON Projects (Id);
ALTER TABLE Clients_Projects_Employees ADD CONSTRAINT FKClients_Pr424306 FOREIGN KEY (EmployeesId) REFERENCES Employees (Id);
ALTER TABLE Clients_Projects_Employees ADD CONSTRAINT FKClients_Pr911787 FOREIGN KEY (Clients_Projectsid) REFERENCES Clients_Projects (Id);
ALTER TABLE Employees ADD CONSTRAINT FKEmployees791873 FOREIGN KEY (AccountTypeId) REFERENCES AccountType (Id);
ALTER TABLE Clients_Projects ADD CONSTRAINT FKClients_Pr50891 FOREIGN KEY (ProjectStatusId) REFERENCES ProjectStatus (Id);
ALTER TABLE Clients_Projects ADD CONSTRAINT FKClients_Pr246802 FOREIGN KEY (ProjectsId) REFERENCES Projects (Id);
ALTER TABLE Clients_Projects ADD CONSTRAINT FKClients_Pr537645 FOREIGN KEY (ClientsId) REFERENCES Clients (Id);
ALTER TABLE Employees ADD CONSTRAINT FKEmployees887729 FOREIGN KEY (GendersId) REFERENCES Genders (Id);
