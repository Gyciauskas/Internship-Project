using CodeMash.Net.DataContracts;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.ServiceModels
{
    [Route("/recipeConnections", "GET", Summary = "Get all recipe connections")]
    public class GetRecipeConnections : ListRequestBase, IReturn<GetRecipeConnectionsResponse>
    {
        public string Name { get; set; }
    }
}