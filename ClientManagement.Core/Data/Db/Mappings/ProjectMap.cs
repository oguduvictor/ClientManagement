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

            Property(x => x.Description).HasColumnName("Description");
            Property(x => x.ProjectStatus).HasColumnName("ProjectStatus");
            Property(x => x.ClientId).HasColumnName("ClientId");
            modelBuilder.Entity<Project>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("EmployeeProjects").MapLeftKey("ProjectId").MapRightKey("EmployeeId"));
        }
    }
}
}
