using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public CreateRecipeResponse Any(CreateRecipe request)
        {
            var response = new CreateRecipeResponse();

            var recipe = new Recipe
            {
                Name = request.Name,
                UniqueName = request.UniqueName,
                Images = request.Images
            };

            RecipeService.CreateRecipe(recipe);

            var cacheKey = CacheKeys.Recipes.List;
            Request.RemoveFromCache(Cache, cacheKey);

            response.Result = recipe.Id.ToString();
            return response;
        }
    }
}
