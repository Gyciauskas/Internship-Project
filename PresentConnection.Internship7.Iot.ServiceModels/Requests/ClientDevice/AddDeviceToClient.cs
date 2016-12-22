using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/devices", "POST", Summary = "Create client device")]
    public class AddDeviceToClient : IReturn<AddDeviceToClientResponse>
    {
        public string DeviceId { get; set; }
        public string ClientId { get; set; }
        
        // TODO : add more properties
    }
}