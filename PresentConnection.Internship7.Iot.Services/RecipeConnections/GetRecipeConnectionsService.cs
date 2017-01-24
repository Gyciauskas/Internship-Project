using System;
using System.Collections.Generic;
using System.Linq;
using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionsService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }
        public IImageService ImageService { get; set; }

        public object Any(GetRecipeConnections request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            var cacheKey = CacheKeys.RecipeConnections.List;

            if (!string.IsNullOrEmpty(request.Name))
            {
                cacheKey = CacheKeys.RecipeConnections.ListWithProvidedName.Fmt(request.Name);
            }

            Request.ToOptimizedResultUsingCache(

                Cache,
                cacheKey,
                expireInTimespan,

                () => 
                {
                    var response = new GetRecipeConnectionsResponse();

                    var recipeConnections = RecipeConnectionService.GetAllRecipeConnections(request.Name);
                    if (recipeConnections != null && recipeConnections.Any())
                    {
                        response.Result = new List<RecipeConnectionDto>();
                        foreach (var recipeConnection in recipeConnections)
                        {
                            var item = RecipeConnectionDto.With(ImageService)
                                                .Map(recipeConnection)
                                                .ApplyImages(recipeConnection.Images)
                                                .Build();

                            response.Result.Add(item);
                        }
                    }

                    return response;
                });

            return Cache.Get<GetRecipeConnectionsResponse>(cacheKey);
        }
    }
}
