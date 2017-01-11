using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Services;
using ClientManagement.Tests.Core;
using ClientManagement.Core.Models;
using System.Linq;

namespace ClientManagement.Tests.Services
{
    [TestClass]
    public class ProjectServiceTest
    {
        private Mock<IProjectRepository> _projectRepoMock;
        private ProjectService _projectService ;

        [TestInitialize]
        public void BeforeEach()
        {
            var projects = Data.Projects;
            _projectRepoMock = new Mock<IProjectRepository>();
            _projectRepoMock.Setup(x => x.GetAllProjects()).Returns(projects);
            _projectRepoMock.Setup(x => x.GetProject(It.IsAny<Guid>()))
                .Returns((Guid input) => 
                {
                    return projects.FirstOrDefault(x => x.Id == input);
                });
            _projectService = new ProjectService(_projectRepoMock.Object);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Get_All_Projects()
        {
            var projects = _projectService.GetAllProjects();

            Assert.AreEqual(3, projects.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_A_Project()
        {
            var project = _projectService.GetProject(Data.Projects[0].Id);

            Assert.IsNotNull(project);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Get_All_Employees_For_A_Project()
        {
            var employees = _projectService.GetEmployeeListForProject(Data.Projects[0].Id);

            Assert.IsNotNull(employees);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Add_A_Project()
        {
            var project = Data.Projects[2];
            _projectService.Save(project);
        }
    }
}
