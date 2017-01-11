using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using System.Data.Entity;
using ClientManagement.Core.Data.Db;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class ProjectRepositoryTest
    {
        private DbManagementContext context;
        private ProjectRepository repo;
        private DbContextTransaction txn;

        [TestInitialize]
        public void BeforeEach()
        {
            context = new DbManagementContext();
            repo = new ProjectRepository(context);
            txn = context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void AfterEach()
        {
            txn.Rollback();
            txn.Dispose();
        }
        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Add_Project_To_DB()
        {
            repo.Create(Data.Projects[0]);
            context.SaveChanges();
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_All_Projects_From_DB()
        {
            context.Projects.AddRange(Data.Projects);
            context.SaveChanges();
            var projects = repo.GetAllProjects();

            Assert.AreEqual(3, projects.Count);
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_A_Project_From_DB()
        {
            context.Projects.AddRange(Data.Projects);
            context.SaveChanges();
            var project = repo.GetProject(Data.Projects[1].Id);

            Assert.IsNotNull(project);
        }
    }
}