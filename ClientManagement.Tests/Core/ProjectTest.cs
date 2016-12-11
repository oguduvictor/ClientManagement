using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Models;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod, TestCategory("Unit Test")]
        public void Should_Be_Able_To_Create_Project_Instance()
        {
            var project = new Project();
        }
    }
}
