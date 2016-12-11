using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Models;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_Client_Instance()
        {
            var client = new Client();
        }
    }
}
