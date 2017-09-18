using ClientManagement.Core.Interfaces;
using ClientManagement.Tests.Core;
using ClientManagement.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClientManagement.Tests.Controllers
{
    [TestClass]
    public class ClientControllerTest
    {
        private Mock<IClientService> _clientServiceMock;
        [TestInitialize]
        public void BeforeEach()
        {
            var clients = Data.Clients;
            _clientServiceMock = new Mock<IClientService>();
            _clientServiceMock.Setup(x => x.GetAllClients()).ReturnsAsync(clients);
            _clientServiceMock.Setup(x => x.GetClient(It.IsAny<Guid>()))
                .ReturnsAsync((Guid input) =>
                {
                    return clients.Find(x => x.Id == input);
                });
        }


        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Return_List_Of_Clients_In_Index()
        {
            var controller = new ClientController(_clientServiceMock.Object);

            var clients = await controller.Index() as ViewResult;

            Assert.IsNotNull(clients.Model);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Retrieve_A_Client()
        {
            var controller = new ClientController(_clientServiceMock.Object);
            var client = await controller.Details(It.IsAny<Guid>());
            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public async Task Should_Be_Able_To_Create_Client()
        {
            var controller = new ClientController(_clientServiceMock.Object);
            await controller.Create(Data.Clients[0]);
        }
    }
}
