using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagement.Core.Models;
using System.Data.Entity;
using ClientManagement.Core.Data.Db;

namespace ClientManagement.Core.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbManagementContext _context;
        private readonly bool _externalContext;
        public ClientRepository()
        {
            _context = new DbManagementContext();
        }
        public ClientRepository(DbManagementContext context)
        {
            _context = context;
            _externalContext = true;
        }
         
        public void Create(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClient(Guid id)
        {
            return _context.Clients.Find(id);
        }

        public void Update(Client client)
        {
            if(!_context.Clients.Local.Contains(client))
            {
                _context.Clients.Attach(client);
                _context.Entry(client).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
