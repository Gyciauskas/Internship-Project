using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/{Id}", "GET", Summary = "Get clients by Id")]
    public class GetClient
    {
        public string Id { get; set; }
    }
}
