using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients", "GET", Summary = "Gets all clients!")]
    public class GetClients : IReturn<GetClientsResponse>
    {
        public string Name { get; set; }
    }
}
