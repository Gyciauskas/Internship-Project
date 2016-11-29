using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Lookups)]
    public class Lookup : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Type { get; set; } // e.g. Widget, Category, Industry
    }
}
