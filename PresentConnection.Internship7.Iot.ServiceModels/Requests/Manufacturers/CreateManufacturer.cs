using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers", "POST", Summary = "Create manufacturer")]
    public class CreateManufacturer : IReturn<CreateManufacturerResponse>
    {
        public CreateManufacturer()
        {
            Images = new List<byte[]>();
        }

        public string Name { get; set; }
        public string UniqueName { get; set; }
        public List<byte[]> Images { get; set; }
    }
}