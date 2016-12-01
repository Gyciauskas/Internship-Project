using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientService
    {
        void CreateClient(Client client);
        void UpdateClient(Client client);
        bool DeleteClient(string id);
        List<Client> GetAllClients();
        Client GetClient(string id);
    }
}
