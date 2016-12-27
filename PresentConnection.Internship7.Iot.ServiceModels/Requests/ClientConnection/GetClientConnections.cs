using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clientconnections", "GET", Summary = "Gets all clientconnections!")]
    public class GetClientConnections
    {
        public string ClientId { get; set; }
    }
}
