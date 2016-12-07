using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices/{Id}", "GET", Summary = "Get device by Id")]
    public class GetDevice : IReturn<GetDeviceResponse>
    {
        public string Id { get; set; }
    }
}
