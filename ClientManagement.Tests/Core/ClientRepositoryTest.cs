using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Models;
using System.Data.Entity;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class ClientRepositoryTest
    {
        private DbManagementContext context;
        private ClientRepository repo;
        private DbContextTransaction txn;

        [TestInitialize]
        public void BeforeEach()
        {
            context = new DbManagementContext();
            repo = new ClientRepository(context);
            txn = context.Database.BeginTransaction();
        }
        [TestCleanup]
        public void AfterEach()
        {
            txn.Rollback();
            txn.Dispose();
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Add_Client_And_Save_To_Db()
        {
            repo.Create(Data.clients[0]);
            context.SaveChanges();
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_All_Clients()
        {
            context.Set<Client>().AddRange(Data.clients);
            context.SaveChanges();
            var clients = repo.GetAllClients();

            Assert.AreEqual(1, clients.Count);
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_A_Client()
        {
            context.Set<Client>().AddRange(Data.clients);
            context.SaveChanges();
            var client = repo.GetClient(1);

            Assert.IsNotNull(client);
        }
    }
}
