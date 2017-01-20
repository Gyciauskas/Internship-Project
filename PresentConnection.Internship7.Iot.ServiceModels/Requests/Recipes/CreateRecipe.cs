using ServiceStack;
using System.Collections.Generic;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes", "POST", Summary = "Create recipe")]
    public class CreateRecipe : IReturn<CreateRecipeResponse>
    {
        public string Name { get; set; }
        public List<string> Images { get; set; }
    }
}
