using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.ConnectionGroups)]
    public class ConnectionGroup : EntityBase, IEntityWithUniqueName
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
