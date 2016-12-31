using ServiceStack;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections/{Id}", "PUT", Summary = "Update clientconnection")]
    public class UpdateClientConnection
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string ClientId { get; set; }
        public Dictionary <string, object> Configuration { get; set; }
        public bool IsDefault { get; set; }

    }
}
