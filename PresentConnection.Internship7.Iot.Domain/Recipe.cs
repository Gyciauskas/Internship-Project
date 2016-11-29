using CodeMash.Net;
using System;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("Recipes")]
    public class Recipe : EntityBase
    {
        public Recipe()
        {
            Images = new List<DisplayImage>();
            Connections = new List<RecipeConnection>();
        }

        public string UniqueName { get; set; } // e.g. Raspberry PI
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeConnection> Connections { get; set; }
        public List<DisplayImage> Images { get; set; } // see description for class DisplayImage
        public string Url { get; set; } // manufacturer Url
        public bool IsVisible { get; set; }
    }
}
