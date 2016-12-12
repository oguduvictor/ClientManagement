using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Tests.Core;
using ClientManagement.Core.Models;
using ClientManagement.Core.Services;

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
            var clients = Data.clients;
            _clientRepoMock = new Mock<IClientRepository>();
            _clientRepoMock.Setup(x => x.GetAllClients()).Returns(clients);
            _clientRepoMock
                .Setup(x => x.GetClient(It.IsAny<int>()))
                .Returns((int input) =>
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
            
            Assert.AreEqual(1, clients.Count);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Get_A_Client()
        {
            var client = _clientService.GetClient(1);

            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Save_Client()
        {
            var client = Data.clients[0];
            _clientService.SaveClient(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Add_Project_To_Client()
        {
            var project = Data.project[0];
            _clientService.AddProject(project, 1);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_Projects_For_Client()
        {
            var projects = _clientService.GetClientProjects(1);
            
            Assert.AreEqual(2, projects.Count);
        }
    }
}
