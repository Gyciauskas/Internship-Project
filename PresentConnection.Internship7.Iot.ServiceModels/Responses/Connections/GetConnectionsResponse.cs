using PresentConnection.Internship7.Iot.Domain;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    public class GetConnectionsResponse
    {
        public GetConnectionsResponse()
        {
            Connections = new List<Connection>();
        }
        public List<Connection> Connections { get; set; }

    }
}