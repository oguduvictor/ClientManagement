using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class EmployeeMap: EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            HasKey(x => x.Id).ToTable("Employees");
            HasMany(x => x.Projects)
                .WithMany(x => x.Employees)
                .Map(m => m.ToTable("EmployeeProject").MapLeftKey("EmployeeId").MapRightKey("ProjectId"));
        }
    }
}