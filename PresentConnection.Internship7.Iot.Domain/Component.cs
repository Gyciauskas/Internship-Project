using CodeMash.Net;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.Components)]
    public class Component : EntityBase, IEntityWithUniqueName
    {
        public Component()
        {
            Images = new List<string>();
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
        public List<string> Images { get; set; }
        public List<DisplayUrl> ArticlesUrls { get; set; }
        public List<string> DefaultRules { get; set; }
        public List<string> DefaultCommands { get; set; }
        public string BuyUrl { get; set; }


    }
}

