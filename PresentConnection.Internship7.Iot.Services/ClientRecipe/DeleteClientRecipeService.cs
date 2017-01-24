using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public DeleteClientRecipeResponse Any(DeleteClientRecipe request)
        {
            var clientRecipe = ClientRecipeService.GetClientRecipe(request.Id, request.Id);
            var clientRecipeName = string.Empty;

            if (clientRecipe != null)
            {
                clientRecipeName = clientRecipe.ClientId;
            }

            var response = new DeleteClientRecipeResponse
            {
                Result = ClientRecipeService.DeleteClientRecipe(request.Id, UserSession.UserAuthId)
            };


            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.ClientRecipes.ListWithProvidedName.Fmt(clientRecipeName);
                var cacheKeyForList = CacheKeys.ClientRecipes.List;
                var cacheKeyForItem = CacheKeys.ClientRecipes.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}