using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes", "GET", Summary = "Gets all recipes or find all with same names!")]
    public class GetRecipes : IReturn<GetRecipesResponse>
    {
        public string Name { get; set; }
    }
}
