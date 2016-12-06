using ClientManagement.Core.Data.Db.Mappings;
using ClientManagement.Core.Models;
using System.Data.Entity;

namespace ClientManagement.Core.Data.Db
{
    public class EmployeeManagementContext: DbContext
    {
        static EmployeeManagementContext()
        {
            Database.SetInitializer<EmployeeManagementContext>(null);
        }
        public EmployeeManagementContext()
            : base("name=DefaultConnection")
        {

        }
        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());
        }
        
    }

}