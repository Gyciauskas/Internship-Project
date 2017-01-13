using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionsService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionsService { get; set; }

        public object Any(GetRecipeConnections request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.RecipeConnections.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.RecipeConnections.ListWithProvidedName.Fmt(request.Name);
            }

            return Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => {
                    var response = new GetRecipeConnectionsResponse
                    {
                        Result = RecipeConnectionsService.GetAllRecipeConnections(request.Name)
                    };

                    return response;
                });
        }
    }
}
