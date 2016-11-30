using System.Collections.Generic;
using CodeMash.Net;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.ClientRecipes)]
    public class ClientRecipe : EntityBase, IEntityWithSensitiveData
    {
        public ClientRecipe()
        {
            Configuration = new Dictionary<string, object>();
        }

        public string RecipeId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
        
    }
}
