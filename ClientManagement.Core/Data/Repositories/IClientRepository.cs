using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IClientRepository
    {
        Client GetClient(int id);

        List<Client> GetAllClients();

        void Create(Client client);

        void Update(Client client);
    }
}