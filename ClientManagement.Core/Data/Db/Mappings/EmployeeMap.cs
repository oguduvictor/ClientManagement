using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class EmployeeMap: EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("Employee");
            HasKey(x => x.Id);
            
            Property(x => x.Gender).HasColumnName("GenderId");
            
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 2);
        }
    }
}