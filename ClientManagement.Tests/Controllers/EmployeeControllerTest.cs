using ClientManagement.Core.Interfaces;
using ClientManagement.Tests.Core;
using ClientManagement.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ClientManagement.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();
        private Mock<IProjectService> _projectServiceMock = new Mock<IProjectService>() ;

        [TestInitialize]
        public void BeforeEach()
        {
            var employees = Data.Employees;
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            _employeeServiceMock.Setup(x => x.GetEmployee(It.IsAny<Guid>()))
                .ReturnsAsync((Guid input) =>
                {
                    return employees.Find(x => x.Id == input);
                });
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_An_Employee()
        {
            var controller = new EmployeeController(_employeeServiceMock.Object);
            var employee = await controller.Details(It.IsAny<Guid>());
            Assert.IsNotNull(employee);
        }

        //[TestMethod, TestCategory("Unit Test")]
        //public async Task Should_Be_Able_To_Create_Employee()
        //{
        //    var controller = new EmployeeController(_employeeServiceMock.Object);
        //    await controller.Create(Data.Employees[0]);
        //}
    }
}
