using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.RecipeConnections)]
    public class RecipeConnection : EntityBase, IEntityWithUniqueName
    {
        public RecipeConnection()
        {
            Images = new List<DisplayImage>();
        }

        public string UniqueName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DisplayImage> Images { get; set; } // see description for class DisplayImage
        public string ConnectionId { get; set; }
        public string DeviceId { get; set; }
        public string ComponentId { get; set; }
    }
}
