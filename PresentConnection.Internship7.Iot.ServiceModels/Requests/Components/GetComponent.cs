using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/components/{Id}", "GET", Summary = "Get component by Id")]
    public class GetComponent : IReturn<GetComponentResponse>
    {
        public string Id { get; set; }
    }
}
