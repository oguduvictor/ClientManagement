using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IClientRepository
    {
        Client GetClient(Guid id);

        List<Client> GetAllClients();

        void Create(Client client);

        void Update(Client client);
    }
}