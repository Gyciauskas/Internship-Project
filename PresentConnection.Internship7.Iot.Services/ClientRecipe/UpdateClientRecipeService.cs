using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public UpdateClientRecipeResponse Any(UpdateClientRecipe request)
        {
            var response = new UpdateClientRecipeResponse();

            var clientRecipe = ClientRecipeService.GetClientRecipe(request.Id, UserSession.UserAuthId);
            clientRecipe = clientRecipe?.PopulateWith(request);
            var clientRecipeName = string.Empty;

            if (clientRecipe != null)
            {
                clientRecipeName = clientRecipe.ClientId;
                clientRecipe = clientRecipe.PopulateWith(request);
            }

            ClientRecipeService.UpdateClientRecipe(clientRecipe, UserSession.UserAuthId);

            var cacheKeyForListWithName = CacheKeys.ClientRecipes.ListWithProvidedName.Fmt(clientRecipeName);
            var cacheKeyForList = CacheKeys.ClientRecipes.List;
            var cacheKeyForItem = CacheKeys.ClientRecipes.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = clientRecipe;
            return response;
        }
    }
}