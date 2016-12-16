using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IClientRepository
    {
        void Create(Client client);
        Client GetClient(int id);
        List<Client> GetAllClients();
        void Update(Client client);
        void Delete(int id);
    }
}