using ServiceStack;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connections", "PUT", Summary = "Update connection")]
    public class UpdateConnection : IReturn<UpdateConnectionResponse>
    {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
}
