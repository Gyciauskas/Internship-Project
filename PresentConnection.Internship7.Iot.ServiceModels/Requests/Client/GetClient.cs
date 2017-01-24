using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/{Id}", "GET", Summary = "Get clients by Id")]
    public class GetClient : IReturn<GetClientResponse>
    {
        public string Id { get; set; }
    }
}
