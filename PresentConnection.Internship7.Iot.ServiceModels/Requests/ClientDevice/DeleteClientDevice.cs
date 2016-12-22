using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientDevice/{Id}", "DELETE", Summary = "Delete client device")]
    public class DeleteClientDevice : IReturn<DeleteClientDeviceResponse>
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
    }
}
