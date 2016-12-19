using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientRecipe", "GET", Summary = "Gets all client recipes")]
    public class GetClientRecipes : IReturn<GetClientRecipeResponse>
    {
        public string ClientId { get; set; }
    }
}