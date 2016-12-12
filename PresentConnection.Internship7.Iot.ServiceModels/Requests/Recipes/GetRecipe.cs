using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes/{Id}", "GET", Summary = "Get recipe")]
    public class GetRecipe
    {
        public string Id { get; set; }
    }
}
