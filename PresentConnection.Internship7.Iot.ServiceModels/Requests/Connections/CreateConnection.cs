using ServiceStack;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connections", "POST", Summary = "Create connection")]
    public class CreateConnection : IReturn<CreateConnectionResponse>
    {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
