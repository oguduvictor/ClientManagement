using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IClientService
    {
        Task<Client> GetClient(Guid ClientId);
        Task<IEnumerable<Client>> GetAllClients();
        Task SaveClient(Client client);
        Task Delete(Guid id);
        Task AddProject(Project project, Guid clientId);
        Task<IEnumerable<Project>> GetClientProjects(Guid ClientId);
    }
}