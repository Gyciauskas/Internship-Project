using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientConnectionService
    {
        void CreateClientConnection(ClientConnection clientconnection, string responsibleClientId);
        void UpdateClientConnection(ClientConnection clientconnection, string responsibleClientId);
        List<ClientConnection> GetClientConnections(string clientId, string responsibleClientId);
        ClientConnection GetClientConnection(string id, string responsibleClientId);
        bool DeleteClientConnection(string id, string responsibleClientId);
    }
}
