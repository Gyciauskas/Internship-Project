using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers", "POST", Summary = "Create manufacturer")]
    public class CreateManufacturer : IReturn<CreateManufacturerResponse>
    {
        public CreateManufacturer()
        {
            Images = new List<string>();
        }

        public string Name { get; set; }
        public string UniqueName { get; set; }
        public List<string> Images { get; set; }
    }
}