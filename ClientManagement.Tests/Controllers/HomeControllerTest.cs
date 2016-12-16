using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Web.Controllers;

namespace ClientManagement.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod, TestCategory("Unit Test")]
        public void Index()
        {
            
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("Client Management System", result.ViewBag.Title);
        }
    }
}
