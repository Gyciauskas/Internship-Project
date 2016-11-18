using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Widget
    {
        public string Type; // e.g. pie-chart, gauge, bar-chart
        public string Query; // query as json
        public Dictionary<string, string> Configuration; // config for widget - agenda, width, colors....
    }
}