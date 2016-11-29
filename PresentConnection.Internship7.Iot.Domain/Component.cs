using CodeMash.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Components")]
    public class Component : EntityBase
    {
     

        public Component()
        {
            Images = new List<DisplayImage>();
            ArticlesUrls = new List<DisplayUrl>();
            DefaultRules = new List<string>();
            DefaultCommands = new List<string>();

        }

        public string ModelName { get; set; }
        public string UniqueName { get; set; }
        public string ArticlesUrl { get; set; }
        public string ShopUrl { get; set; }
        public string ManufacturerId { get; set; }
        public string HowToConnectDescription { get; set; }
        public string TechnicalInstructionsHowToConnect { get; set; }
        public List<DisplayImage> Images { get; set; }
        public List<DisplayUrl> ArticlesUrls { get; set; }
        public List<string> DefaultRules { get; set; }
        public List<string> DefaultCommands { get; set; }
        public string BuyUrl { get; set; }


    }
}

