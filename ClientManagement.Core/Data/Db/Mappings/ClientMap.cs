using ClientManagement.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientManagement.Core.Data.Db.Mappings
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            ToTable("Clients");
            HasKey(x => x.Id);
            
            HasMany(e => e.Projects)
                .WithRequired(e => e.Client).HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}