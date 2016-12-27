using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public GetClientRecipeResponse Any(GetClientRecipe request)
        {
            var response = new GetClientRecipeResponse
            {
                Result = ClientRecipeService.GetClientRecipe(request.Id, UserSession.UserAuthId)
            };
            return response;
        }
    }
}
