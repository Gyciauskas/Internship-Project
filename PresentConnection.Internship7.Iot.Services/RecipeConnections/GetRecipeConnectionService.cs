using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public GetRecipeConnectionResponse Any(GetRecipeConnection request)
        {
            var response = new GetRecipeConnectionResponse
            {
                Result = RecipeConnectionService.GetRecipeConnection(request.Id.ToString())
            };

            return response;
        }
    }
}