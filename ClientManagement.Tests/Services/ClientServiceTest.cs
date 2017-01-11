using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Tests.Core;
using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using System;

namespace ClientManagement.Tests.Services
{
    [TestClass]
    public class ClientServiceTest
    {
        private Mock<IClientRepository> _clientRepoMock;
        private ClientService _clientService;

        [TestInitialize]
        public void BeforeEach()
        {
            var clients = Data.Clients;
            _clientRepoMock = new Mock<IClientRepository>();
            _clientRepoMock.Setup(x => x.GetAllClients()).Returns(clients);
            _clientRepoMock
                .Setup(x => x.GetClient(It.IsAny<Guid>()))
                .Returns((Guid input) =>
            {
                return clients.FirstOrDefault(x => x.Id == input);
            });
            _clientRepoMock.Setup(x => x.Create(It.IsAny<Client>()));
            _clientRepoMock.Setup(x => x.Update(It.IsAny<Client>()));
            _clientService = new ClientService(_clientRepoMock.Object);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Get_All_Clients()
        {
            var clients = _clientService.GetAllClients();
            
            Assert.AreEqual(2, clients.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Get_A_Client()
        {
            var client = _clientService.GetClient(Data.Client2Id);

            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Save_Client()
        {
            var client = Data.Clients[0];
            _clientService.SaveClient(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Add_Project_To_Client()
        {
            var project = Data.Projects[0];
            _clientService.AddProject(project, Data.Client1Id);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_Projects_For_Client()
        {
            var project = Data.Projects[0];
            _clientService.AddProject(project, Data.Client1Id);

            project = Data.Projects[1];
            _clientService.AddProject(project, Data.Client1Id);

            var projects = _clientService.GetClientProjects(Data.Client1Id);
            
            Assert.AreEqual(2, projects.Count);
        }
    }
}
