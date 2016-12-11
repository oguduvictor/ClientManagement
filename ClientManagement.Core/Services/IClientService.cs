using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IClientService
    {
        Client GetClient(int ClientId);
        List<Client> GetAllClients();
        void SaveClient(Client client);
        void AddProject(Project project, int clientId);
        List<Project> GetClientProjects(int ClientId);
    }
}