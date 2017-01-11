using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IClientService
    {
        Client GetClient(Guid ClientId);
        List<Client> GetAllClients();
        void SaveClient(Client client);
        void Delete(Guid id);
        void AddProject(Project project, Guid clientId);
        List<Project> GetClientProjects(Guid ClientId);
    }
}