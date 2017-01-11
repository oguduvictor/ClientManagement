using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Web.Controllers;
using System.Linq;
using System.Web.Mvc;
using ClientManagement.Core.Models;
using Moq;
using ClientManagement.Core.Services;
using ClientManagement.Tests.Core;
using System;

namespace ClientManagement.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        private Mock<IProjectService> _projectServiceMock = new Mock<IProjectService>();
        private Mock<IClientService> _clientServiceMock = new Mock<IClientService>();
        [TestInitialize]
        public void BeforeEach()
        {
            var projects = Data.Projects;
            _projectServiceMock = new Mock<IProjectService>();
            _projectServiceMock.Setup(x => x.GetAllProjects()).Returns(projects);
            _projectServiceMock.Setup(x => x.GetProject(It.IsAny<Guid>()))
                .Returns((Guid input) =>
                {
                    return projects.FirstOrDefault(x => x.Id == input);
                });
        }


        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Return_List_Of_Projects_In_Index()
        {
            var controller = new ProjectController(_projectServiceMock.Object);

            var projects = controller.Index() as ViewResult;

            Assert.IsNotNull(projects.Model);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_A_Project()
        {
            var controller = new ProjectController(_projectServiceMock.Object);
            var project = controller.Details(It.IsAny<Guid>());
            Assert.IsNotNull(project);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_And_Assign_Project_To_Client()
        {
            var controller = new ProjectController(_projectServiceMock.Object);
            var project = Data.Projects[0];
            controller.Create(project);
        }
    }
}