using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connections", "GET", Summary = "Gets all connections !")]
    public class GetConnections : IReturn<GetConnectionsResponse>
    {
        public string Name { get; set; }
    }
}