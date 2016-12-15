using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes", "POST", Summary = "Create recipe")]
    public class CreateRecipe : IReturn<CreateRecipeResponse>
    {
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
