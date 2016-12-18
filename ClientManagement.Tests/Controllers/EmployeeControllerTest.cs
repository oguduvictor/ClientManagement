using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Services;
using Moq;
using ClientManagement.Tests.Core;
using ClientManagement.Web.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ClientManagement.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeService> _employeeServiceMock;
        [TestInitialize]
        public void BeforeEach()
        {
            var employees = Data.Employees;
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).Returns(employees);
            _employeeServiceMock.Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns((int input) =>
                {
                    return employees.Find(x => x.Id == input);
                });
        }


        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Return_All_Employees_In_Index()
        {
            var controller = new EmployeeController(_employeeServiceMock.Object);

            var employees = controller.Index();

            Assert.IsNotNull(employees);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_An_Employee()
        {
            var controller = new EmployeeController(_employeeServiceMock.Object);
            var employee = controller.Details(It.IsAny<int>());
            Assert.IsNotNull(employee);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_Employee()
        {
            var controller = new EmployeeController(_employeeServiceMock.Object);
            controller.Create(Data.Employees[0]);
        }
    }
}
