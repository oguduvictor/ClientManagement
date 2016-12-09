using ClientManagement.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Linq;
using ClientManagement.Core.Data.Repositories;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class FileSystemRepositoryTests
    {
        private readonly static string _filepath = ConfigurationManager.AppSettings["ClientEmployeeFilePath"];

        [TestInitialize]
        public void InitTest()
        {
            var employees = Data.Employees;

            File.WriteAllText(_filepath, JsonConvert.SerializeObject(employees, Formatting.Indented));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.WriteAllText(_filepath, string.Empty);
        }

        [TestMethod]
        public void Should_Be_Able_To_Create_FileSystemRepository_Instance()
        {
            var repo = new FileSystemRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void Should_Be_Able_To_Save_Employee()
        {
            var repo = new FileSystemRepository();
            var employee = new Employee();

            employee.FirstName = "Ngozi";
            employee.LastName = "Adekola";
            employee.Salary = 100000.50M;
            employee.SkillLevel = 5;
            employee.Gender = Gender.Female;
            repo.Create(Data.Employees[0]);
            repo.Create(Data.Employees[1]);
        }

        [TestMethod]
        public void Should_Be_Able_To_Read_All_Employees()
        {
            var repo = new FileSystemRepository();
            var employees = repo.GetAllEmployees();

            Assert.AreEqual(3, employees.Count);
            Assert.AreEqual(1, employees.First().Id);
            Assert.AreEqual(0, employees[1].Id);

        }

        
    }
}
