using ClientManagement.Core.Interfaces;
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
    public class ProjectServiceTest
    {
        private Mock<IProjectRepository> _projectRepoMock;
        private ProjectService _projectService ;

        [TestInitialize]
        public void BeforeEach()
        {
            var projects = Data.Projects;
            _projectRepoMock = new Mock<IProjectRepository>();
            _projectRepoMock.Setup(x => x.GetAllProjects()).ReturnsAsync(projects);
            _projectRepoMock.Setup(x => x.GetProject(It.IsAny<Guid>()))
                .ReturnsAsync((Guid input) => 
                {
                    return projects.FirstOrDefault(x => x.Id == input);
                });
            _projectService = new ProjectService(_projectRepoMock.Object);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Get_All_Projects()
        {
            var projects = await _projectService.GetAllProjects();

            Assert.AreEqual(3, projects.Count());
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_A_Project()
        {
            var project = await _projectService.GetProject(Data.Projects[0].Id);

            Assert.IsNotNull(project);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Get_All_Employees_For_A_Project()
        {
            var employees = await _projectService.GetEmployeeListForProject(Data.Projects[0].Id);

            Assert.IsNotNull(employees);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Add_A_Project()
        {
            var project = Data.Projects[2];
            await _projectService.Save(project);
        }
    }
}
