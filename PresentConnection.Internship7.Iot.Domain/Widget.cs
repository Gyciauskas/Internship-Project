using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Widget
    {
        public string Type { get; set; } // e.g. pie-chart, gauge, bar-chart
        public string Query { get; set; } // query as json
        public Dictionary<string, string> Configuration { get; set; } // config for widget - agenda, width, colors....
    }
}