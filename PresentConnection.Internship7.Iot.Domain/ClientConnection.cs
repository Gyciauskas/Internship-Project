using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.ClientConnections)]
    public class ClientConnection : EntityBase
    {
        public ClientConnection()
        {
            Configuration = new Dictionary<string, object>();
        }

        public string ConnectionId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
        public bool IsDefault { get; set; }
    }
}

