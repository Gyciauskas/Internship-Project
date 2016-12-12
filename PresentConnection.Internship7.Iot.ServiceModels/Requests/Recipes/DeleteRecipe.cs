using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes", "DELETE", Summary = "Delete recipe")]
    public class DeleteRecipe : IReturn<DeleteRecipeResponse>
    {
        public string Id { get; set; }
    }
}
