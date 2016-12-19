using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientRecipe/{Id}", "GET", Summary = "Get client recipe by Id")]
    public class GetClientRecipe : IReturn<GetClientRecipeResponse>
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
    }
}
