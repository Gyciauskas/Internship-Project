using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public UpdateRecipeConnectionResponse Any(UpdateRecipeConnection request)
        {
            var response = new UpdateRecipeConnectionResponse();

            var recipeConnection = RecipeConnectionService.GetRecipeConnection(request.Id);
            var recipeConnectionName = string.Empty;

            if (recipeConnection != null)
            {
                recipeConnectionName = recipeConnection.Name;
                recipeConnection = recipeConnection.PopulateWith(request);
                recipeConnection.UniqueName = SeoService.GetSeName(request.UniqueName);
            }

            RecipeConnectionService.UpdateRecipeConnection(recipeConnection);

            var cacheKeyForListWithName = CacheKeys.RecipeConnections.ListWithProvidedName.Fmt(recipeConnectionName);
            var cacheKeyForList = CacheKeys.RecipeConnections.List;
            var cacheKeyForItem = CacheKeys.RecipeConnections.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = true;
            return response;
        }
    }
}
