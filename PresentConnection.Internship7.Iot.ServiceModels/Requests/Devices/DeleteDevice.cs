using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices/{Id}", "DELETE", Summary = "Delete device")]
    public class DeleteDevice : IReturn<DeleteDeviceResponse>
    {
        public string Id { get; set; }
    }
}
