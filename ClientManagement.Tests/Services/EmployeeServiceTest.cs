using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using Moq;
using ClientManagement.Tests.Core;
using System.Linq;
using ClientManagement.Core.Services;

namespace ClientManagement.Tests.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeRepository> _employeeRepoMock;

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
                    return employees.FirstOrDefault(y => y.Id == input);
                });
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_EmployeeService_Instance()
        {
            var employeeService = new EmployeeService(_employeeRepoMock.Object);

            Assert.IsNotNull(employeeService);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_An_Employee()
        {
            var employeeService = new EmployeeService(_employeeRepoMock.Object);
            var employee = employeeService.GetEmployee(Data.Employees[0].Id);

            Assert.IsNotNull(employee);
            Assert.AreEqual("James", employee.FirstName);
        }
    }
}
