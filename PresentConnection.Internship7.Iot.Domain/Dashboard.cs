using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Dashboards)]
    public class Dashboard : EntityBase
    {
        public Dashboard()
        {
            Widgets = new List<Widget>();
        }
        public string UserId { get; set; } // e.g. Raspberry 
        public List<Widget> Widgets { get; set; }
    }
}
