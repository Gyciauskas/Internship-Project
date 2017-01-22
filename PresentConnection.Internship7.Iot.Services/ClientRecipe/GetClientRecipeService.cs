using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetClientRecipeService : ServiceBase
    {
        public IClientRecipeService ClientRecipeService { get; set; }

        public object Any(GetClientRecipe request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.ClientRecipes.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetClientRecipeResponse
                    {
                        Result = ClientRecipeService.GetClientRecipe(request.Id, UserSession.UserAuthId)
                    };
                    return response;
                });
        }
    }
}