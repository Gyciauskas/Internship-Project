using CodeMash.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Manufacturers)]
    public class Manufacturer : EntityBase, IEntityWithUniqueName
    {
        public Manufacturer()
        {
            Images = new List<string>();
        }

        public string Name { get; set; } // e.g. Raspberry PI
        public string Description { get; set; }
        public string UniqueName { get; set; } // e.g. raspberry-pi
        public List<string> Images { get; set; } // see description for class DisplayImage
        public string Url { get; set; } // manufacturer Url
        public bool IsVisible { get; set; }

    }
}
