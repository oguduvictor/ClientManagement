using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using ClientManagement.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Tests.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private Mock<IProjectRepository> _projectRepoMock;
        private EmployeeService _employeeService;

        [TestInitialize]
        public void BeforeEach()
        {
            var employees = Data.Employees;

            _projectRepoMock = new Mock<IProjectRepository>();
            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _employeeRepoMock.Setup(x => x.GetAllEmployees(false)).ReturnsAsync(employees);
            _employeeRepoMock
                .Setup(x => x.GetEmployee(It.IsAny<Guid>()))
                .ReturnsAsync((Guid input) =>
                {
                    return employees.FirstOrDefault(x => x.Id == input);
                });
            _employeeRepoMock.Setup(x => x.Create(It.IsAny<Employee>()));
            _employeeRepoMock.Setup(x => x.Update(It.IsAny<Employee>()));
            _employeeService = new EmployeeService(_employeeRepoMock.Object, _projectRepoMock.Object);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_An_Employee()
        {
            var employee = await _employeeService.GetEmployee(Data.Employee1Id);

            Assert.IsNotNull(employee);
            Assert.AreEqual("James", employee.FirstName);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_All_Employees()
        {
            var employees = await _employeeService.GetAllEmployees();

            Assert.AreEqual(3, employees.Count());
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_All_Projects_For_An_Employee()
        {
            var employee = await _employeeService.GetEmployee(Data.Employee1Id);
            employee.Projects.Add(Data.Projects[0]);
            employee.Projects.Add(Data.Projects[1]);
            var projects = await _employeeService.GetProjectListForEmployee(Data.Employee1Id);

            Assert.AreEqual(2, projects.Count());
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Assign_Project_To_Employee()
        {
            var projectId = Data.Projects[0].Id;
            var employeeId = Data.Employee3Id;
            await _employeeService.AssignProjectToEmployee(employeeId, projectId);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Remove_Project_From_Employee()
        {
            var employee = await _employeeService.GetEmployee(Data.Employees[2].Id);
            employee.Projects.Add(Data.Projects[0]);
            var projectId = Data.Projects[0].Id;
            Assert.AreEqual(1, employee.Projects.Count);
            await _employeeService.RemoveProjectFromEmployee(employee.Id, projectId);
            Assert.AreEqual(0, employee.Projects.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Save_Employee()
        {
            await _employeeService.Save(Data.Employees[0]);
            var employee = await _employeeService.GetEmployee(Data.Employee1Id);
            Assert.IsNotNull(employee);
        }
    }
}
