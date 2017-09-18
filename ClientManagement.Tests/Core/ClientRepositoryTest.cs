using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task Should_Be_Able_To_Add_Client_And_Save_To_Db()
        {
            await repo.Create(Data.Clients[0]);
            await context.SaveChangesAsync();
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Get_All_Clients()
        {
            context.Set<Client>().AddRange(Data.Clients);
            context.SaveChanges();
            var clients = await repo.GetAllClients();

            Assert.AreEqual(2, clients.Count());
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Get_A_Client()
        {
            context.Set<Client>().AddRange(Data.Clients);
            context.SaveChanges();
            var client = await repo.GetClient(Data.Client1Id);

            Assert.IsNotNull(client);
        }
    }
}
