using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class EmployeeMap: EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("Employees");
            HasKey(x => x.Id);
            
            Property(x => x.Gender).HasColumnName("GenderId");

            Property(e => e.Salary).HasColumnName("Salary").HasPrecision(19, 2);

            HasMany(e => e.Projects).WithMany(e => e.Employees)
            .Map(m => m.ToTable("EmployeeProject").MapLeftKey("EmployeeId").MapRightKey("ProjectId"));
        }
    }
}