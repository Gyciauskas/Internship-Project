using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientDevice/{Id}", "PUT", Summary = "Update client device")]
    public class UpdateClientDevice : IReturn<UpdateClientDeviceResponse>
    {
        public string Id { get; set; }
    }
}
