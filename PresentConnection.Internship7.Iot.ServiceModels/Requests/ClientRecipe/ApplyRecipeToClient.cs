using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/recipes", "POST", Summary = "Apply recipe to the client")]
    public class ApplyRecipeToClient : IReturn<ApplyRecipeToClientResponse>
    {
        public ApplyRecipeToClient()
        {
            Configuration = new Dictionary<string, object>();
        }

        public string RecipeId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
    }
}