using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipeConnections", "DELETE", Summary = "Delete recipe connection")]
    public class DeleteRecipeConnection : IReturn<DeleteRecipeConnectionResponse>
    {
        public string Id { get; set; }
    }
}
