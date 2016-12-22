using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientRecipe", "POST", Summary = "Create client recipe")]
    public class CreateClientRecipe : IReturn<CreateClientRecipeResponse>
    {
        public string RecipeId { get; set; }
        public string ClientId { get; set; }
    }
}