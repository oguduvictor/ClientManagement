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

            Property(x => x.ProjectStatus).HasColumnName("ProjectStatusId");
            Property(x => x.ClientId).HasColumnName("ClientId");

            this.HasMany(e => e.Employees)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("EmployeeProject").MapLeftKey("ProjectId").MapRightKey("EmployeeId"));
        }
    }
}
