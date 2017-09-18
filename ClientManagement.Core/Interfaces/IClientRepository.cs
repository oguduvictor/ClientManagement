using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IClientRepository
    {
        Task Create(Client client);
        Task<Client> GetClient(Guid id);
        Task<IEnumerable<Client>> GetAllClients();
        Task Update(Client client);
        Task Delete(Guid id);
    }
}