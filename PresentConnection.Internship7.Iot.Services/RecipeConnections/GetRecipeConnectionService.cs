using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System;

namespace PresentConnection.Internship7.Iot.Services
{
    public class GetRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }
        public IImageService ImageService { get; set; }

        public object Any(GetRecipeConnection request)
        {
            var expireInTimespan = new TimeSpan(1, 0, 0);

            return Request.ToOptimizedResultUsingCache(

                Cache,
                CacheKeys.RecipeConnections.Item.Fmt(request.Id),
                expireInTimespan,

                () => 
                {
                    var recipeConnection = RecipeConnectionService.GetRecipeConnection(request.Id);

                    var response = new GetRecipeConnectionResponse();

                    if (recipeConnection != null)
                    {
                        response.Result = RecipeConnectionDto.With(ImageService)
                                                .Map(recipeConnection)
                                                .ApplyImages(recipeConnection.Images)
                                                .Build();
                    }

                    return response;
                });
        }
    }
}