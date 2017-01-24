using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }

        public object Any(GetRecipeConnection request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.RecipeConnections.Item.Fmt(request.Id),
                expireInTimespan,

                () => {
                    var response = new GetRecipeConnectionResponse
                    {
                        Result = RecipeConnectionService.GetRecipeConnection(request.Id)
                    };
                    return response;
                });
        }
    }
}