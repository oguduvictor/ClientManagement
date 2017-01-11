using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            ToTable("Projects");
            HasKey(x => x.Id);
            
            Property(x => x.Title).HasColumnName("ProjectTitle");
            Property(x => x.Description).HasColumnName("ProjectDescription");
            
        }
    }
}
