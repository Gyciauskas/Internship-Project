using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipes", "PUT", Summary = "Update recipe")]
    public class UpdateRecipe : IReturn<UpdateRecipeResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
