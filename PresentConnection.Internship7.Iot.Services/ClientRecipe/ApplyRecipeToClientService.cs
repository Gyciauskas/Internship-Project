using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class ApplyRecipeToClientService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public ApplyRecipeToClientResponse Any(ApplyRecipeToClient request)
        {
            var response = new ApplyRecipeToClientResponse();

            var clientRecipe = new ClientRecipe
            {
                RecipeId = request.RecipeId,
                ClientId = UserSession.UserAuthId,
                Configuration = request.Configuration
            };

            ClientRecipeService.CreateClientRecipe(clientRecipe, UserSession.UserAuthId);

            var cacheKey = CacheKeys.ClientRecipes.List;
            Request.RemoveFromCache(Cache, cacheKey);

            response.Result = clientRecipe;
            return response;
        }
    }
}