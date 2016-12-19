using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipeConnections/{Id}", "GET", Summary = "Get recipe connection")]
    public class GetRecipeConnection : IReturn<GetRecipeConnectionResponse>
    {
        public string Id { get; set; }
    }
}
