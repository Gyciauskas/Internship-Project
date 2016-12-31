using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections", "POST", Summary = "Create clientconnection")]
    public class CreateClientConnection : IReturn<CreateClientConnectionResponse>
    {
        public CreateClientConnection()
        {
            Configuration = new Dictionary<string, object>();
        }

        public string ConnectionId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
    }
}
