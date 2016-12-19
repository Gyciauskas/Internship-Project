using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipesService : Service
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public GetClientRecipesResponse Any(GetClientRecipes request)
        {
            var response = new GetClientRecipesResponse
            {
                ClientRecipes = ClientRecipeService.GetAllClientRecipes(request.ClientId, request.ClientId)
            };

            return response;
        }
    }
}