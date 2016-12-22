using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/ClientRecipe/{Id}", "PUT", Summary = "Update client recipe")]
    public class UpdateClientRecipe : IReturn<UpdateClientRecipeResponse>
    {
        public string Id { get; set; }
    }
}
