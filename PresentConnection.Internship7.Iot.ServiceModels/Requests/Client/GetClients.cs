using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/clients", "GET", Summary = "Gets all clients!")]
    public class GetClients
    {
        public string Name { get; set; }
    }
}
