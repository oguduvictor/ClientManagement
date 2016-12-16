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
    public class ClientRepository : IClientRepository, IDisposable
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
        public Client GetClient(int clientId)
        {
            return _context.Clients.Find(clientId);
        }
        public void Update(Client client)
        {
            var dbClient = GetClient(client.Id);

            dbClient.Name = client.Name;
            dbClient.EmailAddress = client.EmailAddress;
            
            foreach (var project in client.Projects)
            {
                if (project.Id == 0)
                {
                    dbClient.Projects.Add(project);
                    continue;
                }

                var dbProject = dbClient.Projects.FirstOrDefault(x => x.Id == project.Id);

                if (dbProject != null)
                {
                    dbProject.Title = project.Title;
                    dbProject.Description = project.Description;
                    dbProject.ProjectStatus = project.ProjectStatus;
                }
            }
            
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            if (_externalContext || _context == null)
                return;

            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}