using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientService
    {
        string CreateClient(Client client);
        void UpdateClient(Client client);
        bool DeleteClient(string id);
        List<Client> GetAllClients();
        Client GetClient(string id);
    }
}
