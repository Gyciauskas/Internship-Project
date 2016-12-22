using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionsService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionsService { get; set; }

        public GetRecipeConnectionsResponse Any(GetRecipeConnections request)
        {
            var response = new GetRecipeConnectionsResponse
            {
                Result = RecipeConnectionsService.GetAllRecipeConnections(request.Name)
            };

            return response;
        }
    }
}
