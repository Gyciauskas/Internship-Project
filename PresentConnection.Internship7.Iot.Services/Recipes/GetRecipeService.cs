using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeService : ServiceBase
    {
        public IRecipeService RecipeService { get; set; }

        public object Any(GetRecipe request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.Recipes.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetRecipeResponse
                    {
                        Result = RecipeService.GetRecipe(request.Id)
                    };

                    return response;
                });
        }
    }
}
