using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Tests.Core
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        [TestMethod]
        public void Should_Be_Able_To_Add_Employee_To_DB()
        {
            using (var context = new DbManagementContext())
            using (var repo = new EmployeeRepository(context))
            //using (var txn = context.Database.BeginTransaction())
            {
                repo.Create(Data.Employees[2]);
                context.SaveChanges();

              //  txn.Rollback();
                
            }
        }
        [TestMethod]
        public void Should_Be_Able_To_Add_Project_To_DB()
        {
            using (var context = new DbManagementContext())
            using (var repo = new ProjectRepository(context))
            //using (var txn = context.Database.BeginTransaction())
            {
                repo.Create(Data.project[0]);
                context.SaveChanges();

                //  txn.Rollback();

            }
        }
        [TestMethod]
        public void Should_Be_Able_To_Get_List_Of_All_Employees_From_Db()
        {
            using (var context = new DbManagementContext())
            using (var repo = new EmployeeRepository(context))
            using (var txn = context.Database.BeginTransaction())
            {
                var employees = repo.GetAllEmployees();
                txn.Rollback();

                Assert.AreEqual(2, employees.Count);
            }
        }

        [TestMethod]
        public void Should_Be_Able_To_Get_An_Employee_From_Db()
        {
            using (var context = new DbManagementContext())
            using (var repo = new EmployeeRepository(context))
            using (var txn = context.Database.BeginTransaction())
            {
                var employee = repo.GetEmployee(3);
                txn.Rollback();

                Assert.IsNotNull(employee);
            }
        }

        [TestMethod]
        public void Should_Be_Able_To_Add_Client_And_Save_To_Db()
        {
            using (var context = new DbManagementContext())
            using (var repo = new ClientRepository(context))
            {
                repo.Create(Data.client[2]);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Should_Get_Employee_For_Project ()
        {
            using (var context = new DbManagementContext())
            using (var repo = new ClientRepository(context))
            {
                repo.Create(Data.client[2]);
                context.SaveChanges();
            }
        }
    }
}
