using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/devices/{Id}", "GET", Summary = "Get client device by Id")]
    public class GetClientDevice : IReturn<GetClientDeviceResponse>
    {
        public string Id { get; set; }
    }
}
