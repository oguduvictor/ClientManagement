using ClientManagement.Core.Data.Db.Mappings;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Core.Data.Db
{
    public class DbManagementContext: DbContext
    {
        
        public DbManagementContext()
            : base("name=ClientsManagementDB")
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ClientMap());
        }
    }
}
