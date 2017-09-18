using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        private DbManagementContext context;
        private EmployeeRepository repo;
        private DbContextTransaction txn;

        [TestInitialize]
        public void BeforeEach()
        {
            context = new DbManagementContext();
            repo = new EmployeeRepository(context);
            txn = context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void AfterEach()
        {
            txn.Rollback();
            txn.Dispose();
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Add_Employee_To_DB()
        {
            await repo.Create(Data.Employees[0]);
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Get_List_Of_All_Employees_From_Db()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            await context.SaveChangesAsync();
            var employees = await repo.GetAllEmployees();
            Assert.AreEqual(3, employees.Count());
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Get_An_Employee_From_Db()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            context.SaveChanges();
            var employee = await repo.GetEmployee(Data.Employee2Id);
            Assert.IsNotNull(employee);
        }

        [TestMethod, TestCategory("Integration Test")]
        public async Task Should_Be_Able_To_Assign_Project_To_Employee()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            context.Set<Project>().AddRange(Data.Projects);
            await context.SaveChangesAsync();
            await repo.AssignProjectToEmployee(Data.Employee2Id, Data.Projects[1].Id);
        }
    }
}