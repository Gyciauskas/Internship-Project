using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class DeleteRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public DeleteRecipeResponse Any(DeleteRecipe request)
        {
            var recipe = RecipeService.GetRecipe(request.Id);
            var recipeName = string.Empty;

            if (recipe != null)
            {
                recipeName = recipe.Name;
            }

            var response = new DeleteRecipeResponse
            {
                Result = RecipeService.DeleteRecipe(request.Id)
            };

            if (response.Result)
            {
                var cacheKeyForListWithName = CacheKeys.Recipes.ListWithProvidedName.Fmt(recipeName);
                var cacheKeyForList = CacheKeys.Recipes.List;
                var cacheKeyForItem = CacheKeys.Recipes.Item.Fmt(request.Id);

                Request.RemoveFromCache(Cache, cacheKeyForList);
                Request.RemoveFromCache(Cache, cacheKeyForListWithName);
                Request.RemoveFromCache(Cache, cacheKeyForItem);
            }

            return response;
        }
    }
}
