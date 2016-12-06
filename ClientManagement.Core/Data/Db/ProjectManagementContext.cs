using ClientManagement.Core.Data.Db.Mappings;
using ClientManagement.Core.Models;
using System.Data.Entity;

namespace ClientManagement.Core.Data.Db
{
    public class ProjectManagementContext: DbContext
    {
        static ProjectManagementContext()
        {
            Database.SetInitializer<EmployeeManagementContext>(null);
        }
        public ProjectManagementContext()
            : base("name=DefaultConnection")
        {

        }
        public virtual DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectMap());
        }
    }
}
