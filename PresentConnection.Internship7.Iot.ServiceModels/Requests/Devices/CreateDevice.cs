using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/devices", "POST", Summary = "Create device")]
    public class CreateDevice : IReturn<CreateDeviceResponse>
    {
        public CreateDevice()
        {
            Images = new List<string>();
        }

        public string ModelName { get; set; }
        public List<string> Images { get; set; }
    }
}
