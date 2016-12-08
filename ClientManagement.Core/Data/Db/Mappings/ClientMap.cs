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

            this.HasMany(e => e.Projects)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);
        }
    }
}