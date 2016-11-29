using System;
using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName("ClientRecipes")]
    public class ClientRecipe : EntityBase
    {
        public string RecipeId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, object> configuration { get; set; }
        public ClientRecipe()
        {
            configuration = new Dictionary<string, object>();
        }
    }
}
