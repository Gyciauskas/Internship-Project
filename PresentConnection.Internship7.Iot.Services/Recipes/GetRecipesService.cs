using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipesService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public object Any(GetRecipes request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.Recipes.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.Recipes.ListWithProvidedName.Fmt(request.Name);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {

                    var response = new GetRecipesResponse
                    {
                        Result = RecipeService.GetAllRecipes(request.Name)
                    };

                    return response;
                });
        }
    }
}
