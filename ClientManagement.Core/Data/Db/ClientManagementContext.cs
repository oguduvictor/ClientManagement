using ClientManagement.Core.Data.Db.Mappings;
using ClientManagement.Core.Models;
using System.Data.Entity;

namespace ClientManagement.Data.Db
{
    public class ClientManagementContext : DbContext
    {
        static ClientManagementContext()
        {
            Database.SetInitializer<ClientManagementContext>(null);
        }
        public ClientManagementContext()
            : base("name=DefaultConnection")
        {

        }
        public virtual DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientMap());
        }
    }
}
