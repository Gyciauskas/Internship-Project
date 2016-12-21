using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections", "POST", Summary = "Create clientconnection")]
    public class CreateClientConnection : IReturn<CreateClientConnectionResponse>
    {
        public string ConnectionId { get; set; }
        public string ClientId { get; set; }
    }
}
