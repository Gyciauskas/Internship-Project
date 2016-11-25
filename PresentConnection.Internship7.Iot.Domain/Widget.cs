using PresentConnection.Internship7.Iot.Domain.Validators.Enums;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class Widget
    {
        
        public Type Type { get; set; }
        public string Query { get; set; } // query as 
        public int Order { get; set; }
        public Dictionary<string, object> Configuration { get; set; } // config for widget - agenda, width, colors....
        
                
    }

}