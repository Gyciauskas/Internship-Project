using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipeService : Service
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public GetClientRecipeResponse Any(GetClientRecipe request)
        {
            var response = new GetClientRecipeResponse
            {
                ClientRecipe = ClientRecipeService.GetClientRecipe(request.Id, request.ClientId)
            };
            return response;
        }
    }
}
