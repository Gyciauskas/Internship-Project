using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components", "GET", Summary = "Gets all components !")]
    public class GetComponents : IReturn<GetComponentsResponse>
    {
        public string ModelName { get; set; }
    }
}
