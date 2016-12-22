using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("ClientDevice", "POST", Summary = "Create client device")]
    public class CreateClientDevice : IReturn<CreateClientDeviceResponse>
    {
        public string DeviceId { get; set; }
        public string ClientId { get; set; }
    }
}