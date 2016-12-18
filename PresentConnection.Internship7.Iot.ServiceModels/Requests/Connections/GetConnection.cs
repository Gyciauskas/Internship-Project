using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connections/{Id}", "GET", Summary = "Gets connection by Id !")]
    public class GetConnection : IReturn<GetConnectionResponse>
    {
        public string Id { get; set; }
    }
}