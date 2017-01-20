using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class UpdateRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public UpdateRecipeResponse Any(UpdateRecipe request)
        {
            var response = new UpdateRecipeResponse();

            var recipe = RecipeService.GetRecipe(request.Id);
            var recipeName = string.Empty;

            if (recipe != null)
            {
                recipeName = recipe.Name;
                recipe = recipe.PopulateWith(request);
                recipe.UniqueName = SeoService.GetSeName(request.UniqueName);
            }

            RecipeService.UpdateRecipe(recipe);

            var cacheKeyForListWithName = CacheKeys.Recipes.ListWithProvidedName.Fmt(recipeName);
            var cacheKeyForList = CacheKeys.Recipes.List;
            var cacheKeyForItem = CacheKeys.Recipes.Item.Fmt(request.Id);

            Request.RemoveFromCache(Cache, cacheKeyForList);
            Request.RemoveFromCache(Cache, cacheKeyForListWithName);
            Request.RemoveFromCache(Cache, cacheKeyForItem);

            response.Result = true;
            return response;
        }
    }
}
