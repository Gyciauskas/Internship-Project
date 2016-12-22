using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipesService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public GetClientRecipesResponse Any(GetClientRecipes request)
        {
            var response = new GetClientRecipesResponse
            {
                Result = ClientRecipeService.GetAllClientRecipes(request.Id, UserSession.UserAuthId)
            };

            return response;
        }
    }
}