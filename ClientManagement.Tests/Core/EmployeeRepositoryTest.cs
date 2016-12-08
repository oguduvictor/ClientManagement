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
            using (var txn = context.Database.BeginTransaction())
            {
                repo.Create(Data.Employees[1]);
                context.SaveChanges();
                var dbEmployee = context.Set<Employee>().Find(Data.User1Id);

                txn.Rollback();

                Assert.IsNotNull(dbEmployee);
                
            }
        }
    }
}
