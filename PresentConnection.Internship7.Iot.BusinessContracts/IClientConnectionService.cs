﻿using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.BusinessContracts
{
    public interface IClientConnectionService
    {
        void CreateClientConnection(ClientConnection clientconnection);
        void UpdateClientConnection(ClientConnection clientconnection);
        List<ClientConnection> GetClientConnections(string clientId);
        ClientConnection GetClientConnection(string id);
        bool DeleteClientConnection(string id);
    }
}
