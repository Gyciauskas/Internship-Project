using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/devices/{Id}", "DELETE", Summary = "Delete client device")]
    public class DeleteClientDevice : IReturn<DeleteClientDeviceResponse>
    {
        public string Id { get; set; }
    }
}
