using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task Create(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }
        public async Task<Client> GetClient(Guid clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
        }
        public async Task Update(Client client)
        {
            var dbClient = await GetClient(client.Id);

            dbClient.Name = client.Name;
            dbClient.EmailAddress = client.EmailAddress;

            foreach (var project in client.Projects)
            {
                if (project.Id == null)
                {
                    dbClient.Projects.Add(project);
                    continue;
                }

                var dbProject = dbClient.Projects.FirstOrDefault(x => x.Id == project.Id);

                if (dbProject != null)
                {
                    dbProject.Title = project.Title;
                    dbProject.Description = project.Description;
                    dbProject.Status = project.Status;
                }
            }

            _context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var client = await GetClient(id);
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