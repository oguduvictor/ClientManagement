using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Models;
using System.Data.Entity;

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
        public void Should_Be_Able_To_Add_Employee_To_DB()
        {
            repo.Create(Data.Employees[0]);
        }
        
        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_List_Of_All_Employees_From_Db()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            context.SaveChanges();
            var employees = repo.GetAllEmployees();
            Assert.AreEqual(3, employees.Count);
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Get_An_Employee_From_Db()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            context.SaveChanges();
            var employee = repo.GetEmployee(Data.Employee2Id);
            Assert.IsNotNull(employee);
        }

        [TestMethod, TestCategory("Integration Test")]
        public void Should_Be_Able_To_Assign_Project_To_Employee()
        {
            context.Set<Employee>().AddRange(Data.Employees);
            context.Set<Project>().AddRange(Data.Projects);
            context.SaveChanges();
            repo.AssignProjectToEmployee(Data.Employee2Id, Data.Projects[1].Id);
        }
    }
}