using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Lookups")]
    public class Lookup : EntityBase
    {
        public Lookup() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Type { get; set; } // e.g. Widget, Category, Industry
    }
}
