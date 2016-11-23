using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Widget
    {
        public enum Type { NotSet, Gauge, BatChart } // e.g. pie-chart, gauge, bar-chart
        public string Query { get; set; } // query as json
        public Dictionary<string, object> Configuration { get; set; } // config for widget - agenda, width, colors....
    }

}