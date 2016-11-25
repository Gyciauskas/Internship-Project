using PresentConnection.Internship7.Iot.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    public class RecipeConnection
    {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DisplayImage Image { get; set; }
        public string ConnectionId { get; set; }
        public string DeviceId { get; set; }
        public string ComponentId { get; set; }
    }
}
