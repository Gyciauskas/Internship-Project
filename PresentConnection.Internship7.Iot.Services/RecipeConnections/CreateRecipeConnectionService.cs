using PresentConnection.Internship7.Iot.BusinessContracts;
using PresentConnection.Internship7.Iot.BusinessImplementation;
using PresentConnection.Internship7.Iot.Domain;
using PresentConnection.Internship7.Iot.ServiceModels;
using ServiceStack;
using System.Collections.Generic;
using System.IO;

namespace PresentConnection.Internship7.Iot.Services
{
    public class CreateRecipeConnectionService : ServiceBase
    {
        public IRecipeConnectionService RecipeConnectionService { get; set; }
        public IImageService ImagesService { get; set; }

        public CreateRecipeConnectionResponse Any(CreateRecipeConnnection request)
        {
            var response = new CreateRecipeConnectionResponse();

            var imageIds = ImagesService.GenerateImagesIds(request.FileName, request.Image);

            var recipeConnection = new RecipeConnection
            {
                Name = request.Name,
                Images = imageIds,
                UniqueName = SeoService.GetSeName(request.Name)
            };

            RecipeConnectionService.CreateRecipeConnection(recipeConnection);

            var cacheKey = CacheKeys.RecipeConnections.List;
            Request.RemoveFromCache(Cache, cacheKey);

            // now, return not recipe connection object, but just Id
            response.Result = recipeConnection.Id.ToString();
            return response;
        }
    }
}
