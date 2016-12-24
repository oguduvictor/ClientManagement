using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Services;
using Moq;
using ClientManagement.Tests.Core;
using ClientManagement.Web.Controllers;
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
            var clients = Data.clients;
            _clientServiceMock = new Mock<IClientService>();
            _clientServiceMock.Setup(x => x.GetAllClients()).Returns(clients);
            _clientServiceMock.Setup(x => x.GetClient(It.IsAny<int>()))
                .Returns((int input) =>
                {
                    return clients.Find(x => x.Id == input);
                });
        }


        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Return_List_Of_Clients_In_Index()
        {
            var controller = new ClientController(_clientServiceMock.Object);

            var clients = controller.Index() as ViewResult;

            Assert.IsNotNull(clients.Model);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Retrieve_A_Client()
        {
            var controller = new ClientController(_clientServiceMock.Object);
            var client = controller.Details(It.IsAny<int>());
            Assert.IsNotNull(client);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_Client()
        {
            var controller = new ClientController(_clientServiceMock.Object);
            controller.Create(Data.clients[0]);
        }
    }
}
