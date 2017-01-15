using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes/{Id}", "GET", Summary = "Get recipe")]
    public class GetRecipe : IReturn<GetRecipeResponse>
    {
        public string Id { get; set; }
    }
}
