using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Core.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClient(Guid ClientId)
        {
            return await _clientRepository.GetClient(ClientId);
        }
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clientRepository.GetAllClients();
        }
        public async Task Delete(Guid id)
        {
            await _clientRepository.Delete(id);
        }
        public async Task SaveClient(Client client)
        {
            var dbClient = await GetClient(client.Id);
            if (dbClient == null)
                await _clientRepository.Create(client);
            else
                await _clientRepository.Update(client);
        }
        public async Task AddProject(Project project, Guid clientId)
        {
            var client = await GetClient(clientId);
            client.Projects.Add(project);
        }
        public async Task<IEnumerable<Project>> GetClientProjects(Guid ClientId)
        {
            var client = await GetClient(ClientId);
            return client.Projects.ToList();
        }
    }
}
