using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using Moq;
using ClientManagement.Tests.Core;
using System.Linq;
using ClientManagement.Core.Services;
using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Tests.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private EmployeeService _employeeService;

        [TestInitialize]
        public void BeforeEach()
        {
            var employees = Data.Employees;

            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _employeeRepoMock.Setup(x => x.GetAllEmployees()).Returns(employees);
            _employeeRepoMock
                .Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns((int input) =>
                {
                    return employees.FirstOrDefault(x => x.Id == input);
                });
            _employeeRepoMock.Setup(x => x.Create(It.IsAny<Employee>()));
            _employeeRepoMock.Setup(x => x.Update(It.IsAny<Employee>()));
            _employeeService = new EmployeeService(_employeeRepoMock.Object);
        }
        
        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_An_Employee()
        {
            var employee = _employeeService.GetEmployee(Data.Employees[0].Id);

            Assert.IsNotNull(employee);
            Assert.AreEqual("James", employee.FirstName);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_All_Employees()
        {
            var employees = _employeeService.GetAllEmployees();

            Assert.AreEqual(3, employees.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_All_Projects_For_An_Employee()
        {
            var projects = _employeeService.GetProjectListForEmployee(Data.Employees[1].Id);

            Assert.AreEqual(2, projects.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Assign_Project_To_Employee()
        {
            var projectId = Data.projects[0].Id;
            var employeeId = Data.Employees[2].Id;
            _employeeService.AssignProjectToEmployee(employeeId, projectId);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Remove_Project_From_Employee()
        {
            var employee = _employeeService.GetEmployee(Data.Employees[2].Id);
            employee.Projects.Add(Data.projects[0]);
            var projectId = Data.projects[0].Id;
            Assert.AreEqual(1, employee.Projects.Count);
            _employeeService.RemoveProjectFromEmployee(employee.Id, projectId);
            Assert.AreEqual(0, employee.Projects.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Save_Employee()
        {
            _employeeService.Save(Data.Employees[0]);
            var employee = _employeeService.GetEmployee(30);
            Assert.IsNotNull(employee);
        }
    }
}
