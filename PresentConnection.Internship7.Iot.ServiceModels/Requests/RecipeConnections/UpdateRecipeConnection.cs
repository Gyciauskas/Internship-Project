using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipeConnections", "PUT", Summary = "Update recipe connection")]
    public class UpdateRecipeConnection : IReturn<UpdateRecipeConnectionResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
    }
}
