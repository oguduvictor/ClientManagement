using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            ToTable("Project");
            HasKey(x => x.Id);
            
            
            Property(x => x.Title).HasColumnName("ProjectTitle");
            Property(x => x.Description).HasColumnName("ProjectDescription");
            HasMany(e => e.Employees)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("EmployeeProject").MapLeftKey("EmployeeId").MapRightKey("ProjectId"));
        }
    }
}
