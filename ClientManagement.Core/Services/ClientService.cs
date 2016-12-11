using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientManagement.Core.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client GetClient(int ClientId)
        {
            return _clientRepository.GetClient(ClientId);
        }
        public List<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }
        public void SaveClient(Client client)
        {
            var dbClient = GetClient(client.Id);
            if (dbClient == null)
                _clientRepository.Create(client);
            else
                _clientRepository.Update(client);
        }

        public void AddProject(Project project, int clientId)
        {
            var client = GetClient(clientId);
            client.Projects.Add(project);
        }

        public List<Project> GetClientProjects(int ClientId)
        {
            var client = GetClient(ClientId);
            return client.Projects.ToList();
        }
    }
}
