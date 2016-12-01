using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Widget
    {
        public Widget()
        {
            Configuration = new Dictionary<string, object>();
        }
        public WidgetType WidgetType { get; set; }
        public string Query { get; set; } // query as 
        public int Order { get; set; }

        /// <summary>
        /// config for widget - agenda, width, colors....
        /// </summary>
        public Dictionary<string, object> Configuration { get; set; }
    }
}