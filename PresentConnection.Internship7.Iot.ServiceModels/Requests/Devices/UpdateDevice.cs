using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices/{Id}", "PUT", Summary = "Update device")]
    public class UpdateDevice : IReturn<UpdateDeviceResponse>
    {
        public string Id { get; set; }
        public string ModelName { get; set; }
        public string UniqueName { get; set; }
    }
}
