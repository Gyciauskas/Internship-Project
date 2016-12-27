using System.Collections.Generic;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/recipes/{Id}", "PUT", Summary = "Update client recipe")]
    public class UpdateClientRecipe : IReturn<UpdateClientRecipeResponse>
    {
        public UpdateClientRecipe()
        {
            Configuration = new Dictionary<string, object>();
        }

        public string Id { get; set; }

        public Dictionary<string, object> Configuration { get; set; }
    }
}
