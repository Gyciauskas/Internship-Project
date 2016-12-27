using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/recipes", "GET", Summary = "Gets all client recipes")]
    public class GetClientRecipes : IReturn<GetClientRecipeResponse>
    {
        public string Id { get; set; }
    }
}