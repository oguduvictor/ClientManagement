using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            ToTable("Client");
            HasKey(x => x.Id);
            
            Property(x => x.Name).HasColumnName("ClientName");
            Property(x => x.EmailAddress).HasColumnName("ClientEmailAddress");
            HasMany(e => e.Projects)
                .WithRequired(e => e.Client).HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}