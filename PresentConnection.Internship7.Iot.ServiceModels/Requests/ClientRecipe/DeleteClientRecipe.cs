using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/client/recipes/{Id}", "DELETE", Summary = "Delete client recipe")]
    public class DeleteClientRecipe : IReturn<DeleteClientRecipeResponse>
    {
        public string Id { get; set; }
    }
}
