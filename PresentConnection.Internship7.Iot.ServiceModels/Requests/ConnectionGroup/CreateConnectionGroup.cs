using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/connection-groups", "POST", Summary = "Create Connection Group")]
    public class CreateConnectionGroup : IReturn<CreateConnectionGroupResponse>
    {
        public CreateConnectionGroup()
        {
            RelatedConnections = new List<string>();
            Images = new List<string>();
        }

        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<string> RelatedConnections { get; set; }
        public List<string> Images { get; set; }
    }
}