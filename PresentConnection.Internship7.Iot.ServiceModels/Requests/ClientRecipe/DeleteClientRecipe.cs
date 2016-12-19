using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientRecipe/{Id}", "DELETE", Summary = "Delete client recipe")]
    public class DeleteClientRecipe : IReturn<DeleteClientRecipeResponse>
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
    }
}
