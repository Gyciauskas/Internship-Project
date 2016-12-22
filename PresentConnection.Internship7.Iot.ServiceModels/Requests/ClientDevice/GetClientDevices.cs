using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/devices", "GET", Summary = "Gets all client devices")]
    public class GetClientDevices : IReturn<GetClientDeviceResponse>
    {
        public string ClientId { get; set; }
    }
}