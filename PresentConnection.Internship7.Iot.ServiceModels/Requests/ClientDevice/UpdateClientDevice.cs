using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/devices/{Id}", "PUT", Summary = "Update client device")]
    public class UpdateClientDevice : IReturn<UpdateClientDeviceResponse>
    {
        public string Id { get; set; }
    }
}
