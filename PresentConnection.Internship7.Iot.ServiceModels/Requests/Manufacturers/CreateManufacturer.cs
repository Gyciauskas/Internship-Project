using System.Collections.Generic;
using PresentConnection.Internship7.Iot.Domain;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/manufacturers", "POST", Summary = "Create manufacturer")]
    public class CreateManufacturer : IReturn<CreateManufacturerResponse>
    {

        public string Name { get; set; }
        public string UniqueName { get; set; }

        public byte[] ImageBytes { get; set; }
        public string SeoFileName { get; set; }
        public string MimeType { get; set; }
    }
}