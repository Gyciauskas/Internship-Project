using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Connections")]
    public class Connection : EntityBase
    {
        public Connection()
        {
            Images = new List<DisplayImage>();
            JavascriptLibs = new List<string>();
        }

        public string UniqueName { get; set; }
        public string Name { get; set; }
        public List<DisplayImage> Images { get; set; }
        public string Url { get; set; }
        public string DocumentationUrl { get; set; }
        public string Description { get; set; }
        public List<string> JavascriptLibs { get; set; }
        public bool IsVisible { get; set; }
        public string GroupId { get; set; }
    }
}
