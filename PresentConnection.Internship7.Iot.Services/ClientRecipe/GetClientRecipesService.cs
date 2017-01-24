using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipesService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public object Any(GetClientRecipes request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.ClientRecipes.List;

            if (!string.IsNullOrEmpty(request.Id))
            {
                cacheKey = CacheKeys.ClientRecipes.ListWithProvidedName.Fmt(request.Id);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {
                    var response = new GetClientRecipesResponse
                    {
                        Result = ClientRecipeService.GetAllClientRecipes(request.Id, UserSession.UserAuthId)
                    };

                    return response;
                });
        }
    }
}