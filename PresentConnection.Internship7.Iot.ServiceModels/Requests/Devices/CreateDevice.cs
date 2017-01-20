using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices", "POST", Summary = "Create device")]
    public class CreateDevice : IReturn<CreateDeviceResponse>
    {
        public string ModelName { get; set; }
        public byte[] Image { get; set; }
        public string FileName { get; set; }
    }
}
