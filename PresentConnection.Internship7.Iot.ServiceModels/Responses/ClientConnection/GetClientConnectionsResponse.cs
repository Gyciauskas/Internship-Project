using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;
namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetClientConnectionsResponse
    {
        public GetClientConnectionsResponse()
        {
            clientconnections = new List<ClientConnection>();
        }
        public List<ClientConnection> clientconnections { get; set; }
    }
}
