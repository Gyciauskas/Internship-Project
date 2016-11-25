using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("ConnectionGroups")]
    public class ConnectionGroup : EntityBase
    {
        public ConnectionGroup()
        {
            RelatedConnections = new List<string>();
            Images = new List<DisplayImage>();
        }

        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<string> RelatedConnections { get; set; }
        public List<DisplayImage> Images { get; set; }
    }
}
