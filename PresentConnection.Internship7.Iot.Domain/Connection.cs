using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Connections)]
    public class Connection : EntityBase, IEntityWithUniqueName
    {
        public Connection()
        {
            Images = new List<string>();
            JavascriptLibs = new List<string>();
        }

        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Url { get; set; }
        public string DocumentationUrl { get; set; }
        public string Description { get; set; }
        public List<string> JavascriptLibs { get; set; }
        public bool IsVisible { get; set; }
        public string GroupId { get; set; }
    }
}
